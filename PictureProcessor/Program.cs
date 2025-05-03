using System.Diagnostics;
using PictureHandler;

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
        static bool _useRootName;
        static DirectoryInfo _rootPath = new DirectoryInfo(Directory.GetCurrentDirectory());
        static bool _recurse;
        static bool _dryRun;
        static bool _delete;
        static bool _verbose;
        static int _similarity=99;
        static int _timeoutSeconds=30;
        static int _maxTasks=4;
        static string _command;
        static DirectoryInfo _target;

        public static int Main(string[] args)
        {
            var parser = new Parser("PictureProcessor", "Command line picture processor")
                .AddHelpSwitch()
                .WithErrorWriter(Console.Error.WriteLine)
                .WithHelpWriter(Console.WriteLine)
                .AddStringParameter("Command", a => _command = a, "Command to be run. Available commands are `gui` and `deduplicate`.")
                .AddStringParameter("RootPath", RootPath, "The path(s) from which to explore pictures. Can be a list of folders, delimited by a pipe character `|`.", ".")
                .AddSwitch("Recurse", () => _recurse = true, "Explore subfolders")
                .AddSwitch("DryRun", () => _dryRun = true, "Only display work at hand")
                .AddSwitch("Delete", () => _delete = true, "Permanently delete duplicate pictures (if --Deduplicate is specified")
                .AddSwitch("Verbose", () => _verbose = true, "Produce verbose console output")
                .AddOptionalStringParameter("Target", a => _target = new DirectoryInfo(a), "An alternate directory root where to move duplicate files")
                .AddSwitch("UseRootName", () => _useRootName = true, "If the `Target` is provided, use the last folder name of the provided roots for 1st target level.")
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
                    if (_recurse || _dryRun || _delete || _useRootName || _target != null)
                    {
                        MessageBox.Show(
                            "Can't use any of '--Recurse', '--DryRun', '--Delete', '--UseAltName' or '--Target' options on the command line with the --gui  switch",
                            "Invalid command line options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                    ShowGui(_rootPath, _recurse, _similarity);
                    break;
                case "deduplicate":
                    if (_target != null)
                    {
                        if (_delete)
                        {
                            Console.Error.Write("Can't use the --delete option with a target directory.");
                            return -1;
                        }
                    }
                    else if (_useRootName)
                    {
                        Console.Error.Write("Can't use the --UseRootName option without a target directory.");
                        return -1;
                    }

                    var deleteMode = _dryRun ? DeleteModeEnum.DryRun
                        : _delete ? DeleteModeEnum.Delete
                        : _target != null ? (_useRootName ? DeleteModeEnum.UseRootName : DeleteModeEnum.UseTarget)
                        : DeleteModeEnum.Recycle;
                    DeduplicatePictures(_rootPath, _recurse, deleteMode, _target, _verbose, _similarity, _timeoutSeconds, _maxTasks);
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

        static void DeduplicatePictures(DirectoryInfo rootPath, bool recurse, DeleteModeEnum deleteMode,
            DirectoryInfo targetDirectory, bool verbose, int similarity, int timeoutSeconds, int maxTasks)
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
                DeleteMode = deleteMode,
                TargetDirectory = targetDirectory,
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
