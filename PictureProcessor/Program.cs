namespace PictureProcessor
{
    using System.Globalization;
    using SimpleCommandlineParser;
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using PicturesSorter;
    using System.Diagnostics;

    public static class Program
    {
        static DirectoryInfo _rootPath = new DirectoryInfo(Directory.GetCurrentDirectory());
        static bool _recurse;
        static bool _deduplicate;
        static bool _dryrun;
        static bool _delete;
        static bool _verbose;
        static bool _gui;
        static int _similarity=99;
        static int _timeoutSeconds=30;
        static int _maxTasks=4;

        public static int Main(string[] args)
        {
            var parser = new Parser()
                .AddHelpSwitch()
                .WithErrorWriter(Console.Error.WriteLine)
                .WithHelpWriter(Console.WriteLine)
                .AddStringParameter("RootPath", RootPath, "The path from which to explore pictures", ".")
                .AddSwitch("Recurse", () => _recurse = true, "Explore subfolders")
                .AddSwitch("DryRun", () => _dryrun = true, "Only display work at hand")
                .AddSwitch("Deduplicate", () => _deduplicate = true, "Deduplicate pictures")
                .AddSwitch("Delete", () => _delete = true, "Permanently delete duplicate pictures (if --Deduplicate is specified")
                .AddSwitch("Verbose", () => _verbose = true, "Produce verbose console output")
                .AddSwitch("GUI", () => _gui = true, "Show graphical use interface")
                .AddSwitch("Debug", () => Debugger.Break(), "Start interactive debugging")
                .AddOptionalIntegerParameter("Timeout", a => _timeoutSeconds = int.Parse(a, NumberStyles.Integer, CultureInfo.InvariantCulture),
                    "Timeout for loading a picture", "30")
                .AddOptionalIntegerParameter("MaxTasks", a => _maxTasks = int.Parse(a, NumberStyles.Integer, CultureInfo.InvariantCulture),
                    "Maximum of parallel tasks", "4")
                .AddOptionalIntegerParameter("Similarity", a => _similarity = int.Parse(a, NumberStyles.Integer, CultureInfo.InvariantCulture),
                    "similarity factor for deduplicate", "99")
                .Run(args);
            if(_verbose) parser.EchoParameters();
            if (_gui)
                ShowGui(_rootPath, _recurse, _similarity);
            else if (_deduplicate)
            {
                DeduplicatePictures(_rootPath, _recurse, _delete, _dryrun, _verbose, _similarity, _timeoutSeconds, _maxTasks);
            }

            return 0;
        }

        static void RootPath(string a)
        {
            _rootPath = new DirectoryInfo(a);
            if(!_rootPath.Exists) throw new DirectoryNotFoundException(a);
        }

        static void ShowGui(DirectoryInfo rootPath, bool recurse, int similarity)
        {
            var sims = new SimilarPicturesForm(); 
            var t = sims.LoadPictures(rootPath);
            t.ConfigureAwait(false);
            t.Wait();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(sims);
        }

        static void DeduplicatePictures(DirectoryInfo rootPath, bool recurse, bool delete, bool dryrun, bool verbose, int similarity, int timeoutSeconds, int maxTasks)
        {
            var _similarPicturesHandler = new SimilarPicturesHandler
            {
                Directory = rootPath,
                SimilarityFactor = (double)similarity / 100,
                LoadPictureTimeout = TimeSpan.FromSeconds(timeoutSeconds),
                MaxTasks = maxTasks,
                CloseAction = null,
                SetProgressMaxAction = null,
                IncrementProgressAction = null,
                KeepGoingFunc = null,
                Delete = delete,
                DryRun = dryrun,
                Verbose = verbose
            };
            _similarPicturesHandler.LoadPictures(recurse).Wait();

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
