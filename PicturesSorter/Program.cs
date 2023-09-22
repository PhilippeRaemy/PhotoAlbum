namespace PicturesSorter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ThreadException += Application_ThreadException;
                Application.Run(new PictureSorterForm());
            }

            if (args.Any(a => string.Equals(a, "--deduplicate", StringComparison.InvariantCultureIgnoreCase)))
            {
                var folderName =
                    args.SkipWhile(a => !string.Equals(a, "--folder", StringComparison.InvariantCultureIgnoreCase))
                        .Skip(1).FirstOrDefault() ?? throw new InvalidOperationException("Missing folder name");
                var folder = new DirectoryInfo(folderName);
                if (!folder.Exists) throw new DirectoryNotFoundException($"Folder {folder.FullName} not found!");
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}