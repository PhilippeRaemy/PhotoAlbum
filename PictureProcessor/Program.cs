namespace PictureProcessor
{
    using System.Globalization;
    using SimpleCommandlineParser;
    using System;

    internal class Program
    {
        static string _rootPath;
        static bool _recurse;
        static string _command;
        static bool _verbose;

        static int Main(string[] args)
        {
            var parser = new Parser()
                .AddHelpSwitch()
                .WithErrorWriter(Console.Error.WriteLine)
                .WithHelpWriter(Console.WriteLine)
                .AddStringParameter("Command", a => _command = a, "The command to execute")
                .AddOptionalStringParameter("RootPath", a => _rootPath = a, "The path from which to explore pictures", ".")
                .AddSwitch("Recurse", () => _recurse = true, "Explore subfolders")
                .AddOptionalIntegerParameter("Siblings", a => int.Parse(a, NumberStyles.Integer, CultureInfo.InvariantCulture),
                    "Explore sibling folders up to this level", "0")
                .AddSwitch("Verbose", () => _verbose = true, "Produce verbose output")
                .Run(args);
            if(_verbose) parser.EchoParameters();
            switch (_command.ToLowerInvariant())
            {
                case "map": break;
                default: 
                    Console.Error.WriteLine("Fatal error: unsupported command `{_command}`.");
                    return -1; 
            }
            return 0;
        }
    }
}
