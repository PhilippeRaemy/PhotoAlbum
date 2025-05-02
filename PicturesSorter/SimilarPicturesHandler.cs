using System.Diagnostics;

namespace PicturesSorter
{
    using Microsoft.VisualBasic.FileIO;
    using PictureHandler;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Tracer;

    public enum DeduplicateResultsEnum
    {
        KeepNone,
        KeepOld,
        KeepNew,
        KeepBoth
    }

    public class SimilarPicturesHandler
    {
        public DirectoryInfo Directory { get; set; }
        public double SimilarityFactor { get; set; }
        public TimeSpan LoadPictureTimeout { get; set; }
        public int MaxTasks { get; set; }
        public Action CloseAction { get; set; }
        public Action<int> SetProgressMaxAction { get; set; }
        public Action IncrementProgressAction { get; set; }
        public Func<bool> KeepGoingFunc { get; set; }
        public bool Verbose { get; set; }
        public bool ByFolder { get; set; }
        public bool Delete { get; set; }
        public bool DryRun { get; set; }
        public bool Deduplicate { get; set; } = false;

        readonly List<PictureSignature> _signatures = new();

        public int SignatureCount
        {
            get
            {
                lock (_signatures)
                {
                    return _signatures.Count;
                }
            }
        }


        readonly HashSet<PictureSignature> _distinctSignatures = new();

        readonly Dictionary<PictureSignature, List<PictureSignature>> _similarSignatures =
            new(new PictureSignatureComparer());

        int _countDone;


        public async Task<Dictionary<PictureSignature, List<PictureSignature>>> LoadPictures(bool recurse = true)
        {
            if (!Verbose) Tracer.DisableTracing();
            var extensions = new[] { ".jpg", ".jpeg", ".png" };
            if (Directory is null)
            {
                CloseAction?.Invoke();
                return new Dictionary<PictureSignature, List<PictureSignature>>();
            }

            var searchOption =
                recurse ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly;
            var files = new Queue<FileInfo>(
                Directory.EnumerateFiles("*", searchOption)
                    .Where(fi => extensions.Contains(fi.Extension, StringComparer.InvariantCultureIgnoreCase))
                    .OrderByDescending(fi => fi.Length)); // better (and heavier) images first

            SetProgressMaxAction?.Invoke(files.Count);
            var taskNum = 0;
            var startTime = DateTime.Now;

            async Task LoadPictureThread()
            {
                int myTaskNum;
                int filesDone = 0;
                lock (this) myTaskNum = taskNum++;
                Tracer.WriteDebug(() =>
                    $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} :  Starting...");
                while (true)
                {
                    FileInfo file;
                    lock (files)
                    {
                        if (files.Count == 0)
                        {
                            Tracer.WriteDebug(() =>
                                $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {filesDone} Done!");
                            return; // we're done!
                        }

                        file = files.Dequeue();
                    }

                    var signature = new PictureSignature(file, 16, 4, false);
                    Tracer.WriteDebug(() =>
                        $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {file.FullName}");
                    var signatureList = await signature.GetSignatureAsync(LoadPictureTimeout, ReceiveSignature)
                        .ConfigureAwait(false);
                    Tracer.WriteDebug(() =>
                        $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {file?.FullName} Done. Signature is {(signatureList is null ? string.Empty : string.Join(",", signatureList))}");
                    lock (_signatures) _signatures.Add(signature);
                    try
                    {
                        var message = $"LoadPictureThread {myTaskNum:d2} : {++filesDone} : {_countDone / (DateTime.Now - startTime).TotalSeconds:f2}[#/s] : {file.FullName}";
                        ConsoleTitle.Set(message);
                        Tracer.WriteDebug(() => message);
                    }
                    catch
                    {
                        // ignored division by zero
                    }

                    var formIsAlive = KeepGoingFunc?.Invoke() ?? true;
                    Tracer.WriteDebug(() =>
                        $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : Form is alive : {formIsAlive}.");
                    if (!formIsAlive) break;
                }
            }

            var tasks = Enumerable.Range(0, new[] { MaxTasks, files.Count }.Min())
                .Select(_ => LoadPictureThread())
                // .Pipe(s => s.Start())
                .ToArray();
            await Task.WhenAll(tasks);

            Task.WaitAll(tasks);
            // var tasks = Enumerable.Range(0, MAX_TASKS)
            //    .Select(_ => new Thread(LoadPictureThread))
            //    .Pipe(s => s.Start())
            //    .ToArray();
            // while (tasks.Any(t => t.IsAlive))
            // {
            //     Task.Delay(500).Wait();
            // }
            // 
            return _similarSignatures;
        }

        public Dictionary<PictureSignature, List<PictureSignature>> ReCompare()
        {
            _similarSignatures.Clear();
            lock (_signatures)
            {
                foreach (var signature in _signatures.TakeWhile(signature => KeepGoingFunc?.Invoke() ?? true))
                {
                    if (signature.PictureBox != null)
                    {
                        signature.PictureBox.Dispose();
                        signature.PictureBox = null;
                    }

                    signature.FileInfo.Refresh();
                    if (signature.FileInfo.Exists) ReceiveSignature(signature);
                }
            }

            return _similarSignatures;
        }

        void ReceiveSignature(PictureSignature newSignature)
        {
            Tracer.WriteDebug($"Processing {newSignature.FileInfo.FullName}");
            _countDone += 1;
            IncrementProgressAction?.Invoke();
            var handled = false;
            var logger = new Action<string>(s => Tracer.WriteInfo(() => s));
            lock (_similarSignatures)
            {
                // look for 2 or more similar pictures already displayed: adding 1
                foreach (var s in _similarSignatures.Keys
                             .Where(s => s.GetSimilarityWith(newSignature) > SimilarityFactor))
                {
                    Tracer.WriteInfo(() =>
                        $"    Found similar with {s.FileInfo.FullName}. {_similarSignatures[s].Count} pre-existing.");
                    if (Deduplicate)
                    {
                        var (key, value) =
                            RemoveDuplicate(newSignature, s, _similarSignatures[s], Delete, ByFolder, logger);
                        if (key.Equals(s))
                        {
                            _similarSignatures[s] = value;
                            Tracer.WriteDebug(
                                $"    The list of similar to {s.FileInfo.FullName} was updated after deduplication of {newSignature.FileInfo.FullName}.");
                        }
                        else
                        {
                            _similarSignatures.Remove(s);
                            _similarSignatures[key] = value;
                            Tracer.WriteDebug(
                                $"    The list of similar to {s.FileInfo.FullName} was replaced by {key.FileInfo.FullName} after deduplication of {newSignature.FileInfo.FullName}.");
                        }
                    }
                    else
                    {
                        _similarSignatures[s].Add(newSignature);
                        Tracer.WriteDebug(
                            $"    {newSignature.FileInfo.FullName} was added as similar to {s.FileInfo.FullName}.");
                    }

                    handled = true;
                }

                if (!handled) // look for one similar yet to be displayed picture: adding 2
                    foreach (var previous in _distinctSignatures
                                 .Where(p => p.GetSimilarityWith(newSignature) > SimilarityFactor)
                                 .ToArray() // necessary to close the linq query before to modify the collection
                            )
                    {
                        switch (Deduplicate
                                    ? RemoveDuplicate(newSignature, previous, Delete, ByFolder, logger)
                                    : DeduplicateResultsEnum.KeepBoth)
                        {
                            case DeduplicateResultsEnum.KeepNone: // shouldn't happen :(
                                _distinctSignatures.Remove(previous);
                                Tracer.WriteDebug(() =>
                                    $"    {previous.FileInfo.FullName} removed from the list of distinct pictures.");
                                break;
                            case DeduplicateResultsEnum.KeepOld:
                                // do nothing: that new picture has been discarded
                                break;
                            case DeduplicateResultsEnum.KeepNew:
                                _distinctSignatures.Remove(previous);
                                _distinctSignatures.Add(newSignature);
                                Tracer.WriteDebug(() =>
                                    $"    {previous.FileInfo.FullName} removed from the list of distinct pictures.\n" +
                                    $"    {newSignature.FileInfo.FullName} was added to the list of distinct pictures.");
                                break;
                            case DeduplicateResultsEnum.KeepBoth:
                                Tracer.WriteDebug(() =>
                                    $"    {previous.FileInfo.FullName} removed from the list of distinct pictures and added to similar signatures dictionary with {newSignature.FileInfo.FullName}.");
                                _distinctSignatures.Remove(previous);
                                _similarSignatures.Add(previous, [previous, newSignature]);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        handled = true;
                    }

                if (handled) return;

                _distinctSignatures.Add(newSignature);
                Tracer.WriteDebug($"    {newSignature.FileInfo.FullName} added to the list of distinct pictures.");
            }
        }

        (PictureSignature, List<PictureSignature>) RemoveDuplicate(PictureSignature nSign, PictureSignature keySign,
            List<PictureSignature> pSigns,
            bool delete, bool byFolder, Action<string> logger)
        {
            if (!ByFolder) throw new ApplicationException("Impossible code branch when not deduplicating by folder!");
            pSigns = pSigns.Append(nSign).ToList();
            var best = pSigns.Append(nSign).Max(); // keep the best as the new key
            var exceptions = new List<ApplicationException>();

            IEnumerable<PictureSignature> TrackExceptions(ApplicationException ex)
            {
                exceptions.Add(ex);
                return [];
            }

            var newSignatures = pSigns.SelectMany(s =>
                s.FileInfo.DirectoryName != nSign.FileInfo.DirectoryName
                    ? [s]
                    : RemoveDuplicate(nSign, s, Delete, ByFolder, logger) switch
                    {
                        DeduplicateResultsEnum.KeepNone =>
                            TrackExceptions(new ApplicationException(
                                $"Both files {nSign.FileInfo?.FullName} and  {s.FileInfo?.FullName} are bad? Impossible case!")),
                        DeduplicateResultsEnum.KeepOld => [s],
                        DeduplicateResultsEnum.KeepNew => [nSign],
                        DeduplicateResultsEnum.KeepBoth => [s, nSign], // possible if on dry run
                        _ => throw new ArgumentOutOfRangeException()
                    }).ToList();
            if (exceptions.Any()) throw new AggregateException("Impossible code case detected at runtime", exceptions);
            return (keySign, newSignatures);
        }

        /// <summary>
        /// RemoveDuplicate returns a tuple with the two pictures that need to be kept. If one is null, then it means that it's been discarded
        /// </summary>
        /// <param name="nSign"></param>
        /// <param name="pSign"></param>
        /// <param name="delete"></param>
        /// <param name="byFolder"></param>
        /// <param name="logger"></param>
        /// <returns>(nSign, pSign), in order, possibly </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        ///  TODO: separate the comparison logic and handle the previously existing signatures as a list: we might have several similar pictures but in different folders!
        DeduplicateResultsEnum RemoveDuplicate(PictureSignature nSign, PictureSignature pSign,
            bool delete, bool byFolder, Action<string> logger)
        {
            if (!Deduplicate)
                throw new ApplicationException("Cannot call RemoveDuplicate when Deduplicate flag not set!");
            nSign.FileInfo.Refresh();
            pSign.FileInfo.Refresh();
            if (!nSign.FileInfo.Exists || !pSign.FileInfo.Exists)
            {
#if DEBUG
                logger?.Invoke("One of the pictures file does not exist anymore!?!");
                Debugger.Break();
#endif
                return nSign.FileInfo.Exists ? DeduplicateResultsEnum.KeepNew
                    : pSign.FileInfo.Exists ? DeduplicateResultsEnum.KeepOld
                    : DeduplicateResultsEnum.KeepNone;
            }

            if (byFolder && pSign.FileInfo.DirectoryName != nSign.FileInfo.DirectoryName)
                return DeduplicateResultsEnum.KeepBoth;
            var results = nSign > pSign
                ? DeduplicateResultsEnum.KeepNew
                : DeduplicateResultsEnum.KeepOld;
            // one of them has to be deleted
            var toBeDeleted = (results == DeduplicateResultsEnum.KeepNew ? nSign : pSign).FileInfo;
            toBeDeleted.Refresh();
            if (!toBeDeleted.Exists)
            {
                logger?.Invoke($"{toBeDeleted.FullName} does not exist anymore");
                return results;
            }

            if (DryRun)
            {
                logger?.Invoke($"{toBeDeleted.FullName} would be {(delete ? "deleted" : "recycled")}");
                return DeduplicateResultsEnum.KeepBoth;
            }

            if (delete)
            {
                toBeDeleted.Delete();
                logger?.Invoke($"{toBeDeleted.FullName} has been deleted");
            }
            else
            {
                FileSystem.DeleteFile(toBeDeleted.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                logger?.Invoke($"{toBeDeleted.FullName} has been recycled");
            }

            return results;
        }
    }
}