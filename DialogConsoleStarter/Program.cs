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
            var fileNameMaker = new FileNameHandler(
                userPrefs.IncludeFiles,
                userPrefs.ExcludeFolders,
                @"\.small\.((jpeg)|(jpg))$",
                s => new Regex(@"\.(jpg|jpeg)$", RegexOptions.IgnoreCase).Replace(s, ".small.$1"),
                s => new Regex(@"(.*\.)small\.(jpg|jpeg)$", RegexOptions.IgnoreCase).Replace(s, "$1.$2")
            );

            var folderWalker = new FolderWalker(
                userPrefs.FolderImportStart,
                userPrefs.FolderImportEnd,
                fileNameMaker,
                null
            );
            folderWalker.StartingFolder += FolderWalker_StartingFolder;
            folderWalker.EndingFolder += FolderWalker_EndingFolder;
            folderWalker.FoundAFile += FolderWalker_FoundAFile;

            folderWalker.Run();
        }

        static void FolderWalker_FoundAFile(object sender, FileEventArgs e)
        {
            Console.WriteLine($"**** Found file {e.FileInfo.FullName}.");
        }

        static void FolderWalker_EndingFolder(object sender, FolderEventArgs e)
        {
            Console.WriteLine($"Ending Folder {e.DirectoryInfo.FullName}.");
        }

        static void FolderWalker_StartingFolder(object sender, FolderEventArgs e)
        {
            Console.WriteLine($"Starting Folder {e.DirectoryInfo.FullName}.");
        }
    }
}
