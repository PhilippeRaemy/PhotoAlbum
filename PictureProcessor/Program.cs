namespace PictureProcessor
{
    using System.Globalization;
    using SimpleCommandlineParser;
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using PicturesSorter;

    internal class Program
    {
        static DirectoryInfo _rootPath = new DirectoryInfo(Directory.GetCurrentDirectory());
        static bool _recurse;
        static bool _deduplicate;
        static bool _dryrun;
        static bool _noRecycle;
        static bool _verbose;
        static bool _gui;
        static int _similarity=99;

        static int Main(string[] args)
        {
            var parser = new Parser()
                .AddHelpSwitch()
                .WithErrorWriter(Console.Error.WriteLine)
                .WithHelpWriter(Console.WriteLine)
                .AddOptionalStringParameter("RootPath", RootPath, "The path from which to explore pictures", ".")
                .AddSwitch("Recurse", () => _recurse = true, "Explore subfolders")
                .AddSwitch("DryRun", () => _dryrun = true, "Only display work at hand")
                .AddSwitch("Deduplicate", () => _deduplicate = true, "Deduplicate pictures")
                .AddSwitch("NoRecycleBin", () => _noRecycle = true, "Deduplicate pictures")
                .AddSwitch("Verbose", () => _verbose = true, "Produce verbose console output")
                .AddSwitch("GUI", () => _gui= true, "Show graphical use interface")
                .AddOptionalIntegerParameter("Similarity", a => _similarity = int.Parse(a, NumberStyles.Integer, CultureInfo.InvariantCulture),
                    "similarity factor for deduplicate", "99")
                .AddSwitch("Verbose", () => _verbose = true, "Produce verbose output")
                .Run(args);
            if(_verbose) parser.EchoParameters();
            if (_gui)
                ShowGui(_rootPath, _recurse, _similarity);
            else if (_deduplicate)
                DeduplicatePictures(_rootPath, _recurse, _noRecycle, _dryrun, _verbose, _similarity);
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
            sims.LoadPictures(rootPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(sims);
        }

        static void DeduplicatePictures(DirectoryInfo rootPath, bool recurse, bool noRecycle, bool dryrun, bool verbose, int similarity)
        {
            throw new NotImplementedException();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
