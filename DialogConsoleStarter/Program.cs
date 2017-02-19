namespace DialogConsoleStarter
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using AlbumWordAddin;
    using AlbumWordAddin.UserPreferences;

    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var form=new FormImportPictures();
            if (form.ShowDialog() != DialogResult.OK)
            {
                Console.WriteLine("User has canceled");
                return;
            }
            Console.WriteLine("Processing settings...");
            var userPrefs=new PersistedUserPreferences();
            var folderWalker = new FolderWalker(
                userPrefs.FolderImportStart,
                userPrefs.FolderImportEnd,
                @"\.jpg$",
                @"\*",
                @"small\.jpg$",
                s=> new Regex(@"\.jpg$", RegexOptions.IgnoreCase).Replace(s, ".small.jpg"),
                null
            );
            folderWalker.StartingFolder += FolderWalker_StartingFolder;
            folderWalker.EndingFolder += FolderWalker_EndingFolder;
            folderWalker.FoundAFile += FolderWalker_FoundAFile;

            folderWalker.Run();
        }

        private static void FolderWalker_FoundAFile(object sender, FileEventArgs e)
        {
            Console.WriteLine($"**** Found file {e.FileInfo.FullName}.");
        }

        private static void FolderWalker_EndingFolder(object sender, FolderEventArgs e)
        {
            Console.WriteLine($"Ending Folder {e.DirectoryInfo.FullName}.");
        }

        private static void FolderWalker_StartingFolder(object sender, FolderEventArgs e)
        {
            Console.WriteLine($"Starting Folder {e.DirectoryInfo.FullName}.");
        }
    }
}
