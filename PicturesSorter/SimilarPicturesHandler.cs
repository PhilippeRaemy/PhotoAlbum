namespace PicturesSorter
{
    using PictureHandler;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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

        readonly List<PictureSignature> _signatures = new List<PictureSignature>();
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

        readonly HashSet<PictureSignature> _distinctSignatures = new HashSet<PictureSignature>();

        readonly Dictionary<PictureSignature, List<PictureSignature>> _similarSignatures =
            new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        int _countDone;


        public async Task<Dictionary<PictureSignature, List<PictureSignature>>> LoadPictures()
        {
            if (Directory is null)
            {
                CloseAction?.Invoke();
                return new Dictionary<PictureSignature, List<PictureSignature>>();
            }

            var files = new Queue<FileInfo>(
                Directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories)
                    .Concat(Directory.EnumerateFiles("*.jpeg", SearchOption.AllDirectories))
                    .Concat(Directory.EnumerateFiles("*.png", SearchOption.AllDirectories))
                    .OrderByDescending(fi => fi.Length)); // better (and heavier) images first

            SetProgressMaxAction?.Invoke(files.Count);
            var taskNum = 0;
            var startTime = DateTime.Now;

            async Task LoadPictureThread()
            {
                int myTaskNum;
                int filesDone = 0;
                lock (this) myTaskNum = taskNum++;
                while (true)
                {
                    FileInfo file;
                    lock (files)
                    {
                        if (files.Count == 0)
                        {
                            Trace.WriteLine(
                                $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {filesDone} Done!");
                            return; // we're done!
                        }

                        file = files.Dequeue();
                    }

                    var signature = new PictureSignature(file, 16, 4, false);
                    Trace.WriteLine($"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {file.FullName}");
                    var signatureTask = signature.GetSignatureAsync(LoadPictureTimeout, ReceiveSignatureNew);
                    await signatureTask.ConfigureAwait(false);
                    var signatureList = signatureTask.Result;
                    Console.WriteLine(
                        $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : {file.FullName} Done. Signature is {string.Join(",", signatureList)}");
                    lock (_signatures) _signatures.Add(signature);
                    try
                    {
                        Trace.WriteLine(
                            $"LoadPictureThread {myTaskNum:d2} : {++filesDone} : {_countDone / (DateTime.Now - startTime).TotalSeconds:f2}[#/s] : {file.FullName}");
                    }
                    catch
                    {
                        // ignored division by zero
                    }
                    var formIsAlive = KeepGoingFunc?.Invoke() ?? true;
                    Trace.WriteLine(
                        $"LoadPictureThread {myTaskNum:D2} : {DateTime.Now - startTime:g} : Form is alive : {formIsAlive}.");
                    if (!formIsAlive) break;
                }
            }

            var tasks = Enumerable.Range(0, MaxTasks)
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
                foreach (var signature in _signatures.TakeWhile(signature => KeepGoingFunc?.Invoke()??true))
                {
                    if (signature.PictureBox != null)
                    {
                        signature.PictureBox.Dispose();
                        signature.PictureBox = null;
                    }

                    signature.FileInfo.Refresh();
                    if (signature.FileInfo.Exists) ReceiveSignatureNew(signature);
                }
            }
            return _similarSignatures;
        }

        void ReceiveSignatureNew(PictureSignature newSignature)
        {
            Console.WriteLine($"Got {newSignature.FileInfo.FullName}");
            _countDone += 1;
            IncrementProgressAction?.Invoke();
            var handled = false;
            lock (_similarSignatures)
            {
                // look for 2 or more similar pictures already displayed: adding 1
                foreach (var s in _similarSignatures.Keys
                             .Where(s => s.GetSimilarityWith(newSignature) > SimilarityFactor))
                {
                    Console.WriteLine($"    Found similar with {s.FileInfo.FullName}. {_similarSignatures[s].Count} pre-existing.");
                    _similarSignatures[s].Add(newSignature);
                    handled = true;
                }

                if (!handled) // look for one similar yet to be displayed picture: adding 2
                    foreach (var previous in _distinctSignatures
                                 .Where(p => p.GetSimilarityWith(newSignature) > SimilarityFactor)
                                 .ToArray() // necessary to close the linq query before to modify the collection
                            )
                    {
                        Console.WriteLine($"    Found similar with {previous.FileInfo.FullName}. New.");
                        _similarSignatures.Add(previous, new[] { previous, newSignature }.ToList());
                        Console.WriteLine($"    {previous.FileInfo.FullName} removed from distincts.");
                        _distinctSignatures.Remove(previous);
                        handled = true;
                    }

                if (handled) return;

                Console.WriteLine($"    {newSignature.FileInfo.FullName} added to distincts.");
                _distinctSignatures.Add(newSignature);
            }
        }

    }
}