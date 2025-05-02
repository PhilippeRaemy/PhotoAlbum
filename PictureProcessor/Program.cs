using System.Diagnostics;

namespace PictureProcessor
{
    using SimpleCommandlineParser;
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using PicturesSorter;

    public static class Program
    {
        static DirectoryInfo _rootPath = new DirectoryInfo(Directory.GetCurrentDirectory());
        static bool _recurse;
        static bool _dryRun;
        static bool _delete;
        static bool _verbose;
        static int _similarity=99;
        static int _timeoutSeconds=30;
        static int _maxTasks=4;
        static string _command;

        public static int Main(string[] args)
        {
            var parser = new Parser("PictureProcessor", "Command line picture processor")
                .AddHelpSwitch()
                .WithErrorWriter(Console.Error.WriteLine)
                .WithHelpWriter(Console.WriteLine)
                .AddStringParameter("Command", a => _command = a, "Command to be run. Available commands are `gui` and `deduplicate`.")
                .AddStringParameter("RootPath", RootPath, "The path from which to explore pictures", ".")
                .AddSwitch("Recurse", () => _recurse = true, "Explore subfolders")
                .AddSwitch("DryRun", () => _dryRun = true, "Only display work at hand")
                .AddSwitch("Delete", () => _delete = true, "Permanently delete duplicate pictures (if --Deduplicate is specified")
                .AddSwitch("Verbose", () => _verbose = true, "Produce verbose console output")
                .AddSwitch("Debug", () => Debugger.Launch(), "Produce verbose console output")
                .AddOptionalIntegerParameter("Timeout", a => _timeoutSeconds = a,
                    "Timeout for loading a picture", "30")
                .AddOptionalIntegerParameter("MaxTasks", a => _maxTasks = a,
                    "Maximum of parallel tasks", "4")
                .AddOptionalIntegerParameter("Similarity", a => _similarity = a,
                    "similarity factor for deduplicate", "99")
                .Run(args);
            if(_verbose) parser.EchoParameters();
            switch (_command?.ToLowerInvariant())
            {
                case "gui":
                    ShowGui(_rootPath, _recurse, _similarity);
                    break;
                case "deduplicate":
                    DeduplicatePictures(_rootPath, _recurse, _delete, _dryRun, _verbose, _similarity, _timeoutSeconds, _maxTasks);
                    break;
                default:
                    Console.WriteLine("Unknown command: " + _command);
                    Console.WriteLine("Available commands are `gui` and `deduplicate`.");
                    return -1;
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

        static void DeduplicatePictures(DirectoryInfo rootPath, bool recurse, bool delete, bool dryRun, bool verbose, int similarity, int timeoutSeconds, int maxTasks)
        {
            var similarPicturesHandler = new SimilarPicturesHandler
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
                DryRun = dryRun,
                Verbose = verbose,
                Deduplicate = true
            };
            similarPicturesHandler.LoadPictures(recurse).Wait();

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
