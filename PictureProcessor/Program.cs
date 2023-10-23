namespace PictureProcessor
{
    using System.Globalization;
    using SimpleCommandlineParser;
    using System;

    internal class Program
    {
        static string _rootPath = ".";
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
                .AddOptionalStringParameter("RootPath", a => _rootPath = a, "The path from which to explore pictures", ".")
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

        static void ShowGui(string rootPath, bool recurse, int similarity)
        {
            throw new NotImplementedException();
        }

        static void DeduplicatePictures(string rootPath, bool recurse, bool noRecycle, bool dryrun, bool verbose, int similarity)
        {
            throw new NotImplementedException();
        }
    }
}
