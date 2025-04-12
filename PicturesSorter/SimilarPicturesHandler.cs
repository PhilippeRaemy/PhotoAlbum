using Shell32;

namespace PicturesSorter
{
    using PictureHandler;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Tracer;

    public enum FilePreferenceEnum
    {
        Larger,
        Older
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
        public FilePreferenceEnum FilePreference { get; set; }
        public bool ByFolder { get; set; }
        public bool Delete { get; set; }
        public bool DryRun { get; set; }
        static Folder _recyclingBin;

        readonly List<PictureSignature> _signatures = new();

        public SimilarPicturesHandler() => _recyclingBin = new Shell().NameSpace(10);

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
            var extensions = new[] { ".jpg", ".jpeg", ".png" };
            if (Directory is null)
            {
                CloseAction?.Invoke();
                return new Dictionary<PictureSignature, List<PictureSignature>>();
            }

            var searchOption = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
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
                if (Verbose)
                    Tracer.WriteLine(() =>
                        $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} :  Starting...");
                while (true)
                {
                    FileInfo file;
                    lock (files)
                    {
                        if (files.Count == 0)
                        {
                            if (Verbose)
                                Tracer.WriteLine(() =>
                                    $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {filesDone} Done!");
                            return; // we're done!
                        }

                        file = files.Dequeue();
                    }

                    var signature = new PictureSignature(file, 16, 4, false);
                    if (Verbose)
                        Tracer.WriteLine(() =>
                            $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {file.FullName}");
                    var signatureList = await signature.GetSignatureAsync(LoadPictureTimeout, ReceiveSignature)
                        .ConfigureAwait(false);
                    if (Verbose)
                        Tracer.WriteLine(() =>
                            $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {file?.FullName} Done. Signature is {(signatureList is null ? string.Empty : string.Join(",", signatureList))}");
                    lock (_signatures) _signatures.Add(signature);
                    if (Verbose)
                        try
                        {
                            Tracer.WriteLine(() =>
                                $"LoadPictureThread {myTaskNum:d2} : {++filesDone} : {_countDone / (DateTime.Now - startTime).TotalSeconds:f2}[#/s] : {file.FullName}");
                        }
                        catch
                        {
                            // ignored division by zero
                        }

                    var formIsAlive = KeepGoingFunc?.Invoke() ?? true;
                    if (Verbose)
                        Tracer.WriteLine(() =>
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
            if (Verbose) Tracer.WriteLine(() => $"Got {newSignature.FileInfo.FullName}");
            _countDone += 1;
            IncrementProgressAction?.Invoke();
            var handled = false;
            var logger = new Action<string>(s => Tracer.WriteLine(() => s));
            lock (_similarSignatures)
            {
                // look for 2 or more similar pictures already displayed: adding 1
                foreach (var s in _similarSignatures.Keys
                             .Where(s => s.GetSimilarityWith(newSignature) > SimilarityFactor))
                {
                    if (Verbose)
                        Tracer.WriteLine(() =>
                            $"    Found similar with {s.FileInfo.FullName}. {_similarSignatures[s].Count} pre-existing.");
                    var (nSign, pSign) = RemoveDuplicate(newSignature, s, Delete, FilePreference, ByFolder, logger);
                    if (nSign is null)
                    {
                        // do nothing, means we've discarded the new coming signature
                    }
                    else if (pSign is null) //nSign is better than pSign, and we've discarded pSign
                    {
                        _similarSignatures[nSign] = _similarSignatures[s];
                        _similarSignatures.Remove(s);
                    }
                    else
                        _similarSignatures[s].Add(nSign);

                    handled = true;
                }

                if (!handled) // look for one similar yet to be displayed picture: adding 2
                    foreach (var previous in _distinctSignatures
                                 .Where(p => p.GetSimilarityWith(newSignature) > SimilarityFactor)
                                 .ToArray() // necessary to close the linq query before to modify the collection
                            )
                    {
                        var (nSign, pSign) = RemoveDuplicate(newSignature, previous, Delete, FilePreference, ByFolder, logger);
                        if (Verbose)
                            Tracer.WriteLine(() => $"    Found similar with {previous.FileInfo.FullName}. New.");
                        if (nSign is null) break; // do nothing: that new picture has been discarded
                        _distinctSignatures.Remove(previous);
                        if (pSign is not null)
                        {
                            _similarSignatures.Add(previous, [previous, newSignature]);
                            if (Verbose)
                                Tracer.WriteLine(() => $"    {previous.FileInfo.FullName} removed from the list of distinct pictures.");
                        }

                        handled = true;
                    }

                if (handled) return;

                _distinctSignatures.Add(newSignature);
                if (Verbose) Tracer.WriteLine($"    {newSignature.FileInfo.FullName} added to the list of distinct pictures.");
            }
        }

        (PictureSignature, PictureSignature) RemoveDuplicate(
            PictureSignature nSign, PictureSignature pSign,
            bool delete, FilePreferenceEnum filePreference, bool byFolder, Action<string> logger)
        {
            if (byFolder && pSign.FileInfo.DirectoryName != nSign.FileInfo.DirectoryName)
                return (nSign, pSign);
            (nSign, pSign) = filePreference switch
            {
                FilePreferenceEnum.Larger
                    => nSign.FileInfo.Length > pSign.FileInfo.Length
                        ? (nSign, default(PictureSignature))
                        : (default(PictureSignature), pSign),
                FilePreferenceEnum.Older
                    => nSign.FileInfo.CreationTimeUtc < pSign.FileInfo.CreationTimeUtc
                        ? (nSign, default)
                        : (default, pSign),
                _ => throw new ArgumentOutOfRangeException(nameof(filePreference), filePreference, null)
            };
            // one of them has to be deleted
            var toBeDeleted = (nSign ?? pSign).FileInfo;
            if (DryRun)
                logger?.Invoke($"{toBeDeleted.FullName} would be {(delete ? "deleted" : "recycled")}");
            else if (delete)
            {
                toBeDeleted.Delete();
                logger?.Invoke($"{toBeDeleted.FullName} has been deleted");
            }
            else
            {
                _recyclingBin.MoveHere(toBeDeleted.FullName);
                logger?.Invoke($"{toBeDeleted.FullName} has been recycled");
            }

            return (nSign, pSign);
        }
    }
}