namespace AlbumWordAddin
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using FolderExtensions;
    using VstoEx.Progress;

    public class FolderNavigator
    {
        readonly FileNameHandler _fileNameHandler;
        readonly IProgress       _progressIndicator;
        readonly DirectoryInfo   _diFolderFrom;
        readonly DirectoryInfo   _diFolderTo;

        public FolderNavigator(
            string folderFrom, 
            string folderTo,
            FileNameHandler fileNameHandler,
            IProgress progressIndicator)
        {
            _fileNameHandler    = fileNameHandler;
            _progressIndicator  = progressIndicator;
            progressIndicator.CancelEvent += ProgressIndicator_CancelEvent;
            if (folderFrom == null) throw new ArgumentNullException(nameof(folderFrom));
            if (folderTo   == null) throw new ArgumentNullException(nameof(folderTo));
            _diFolderFrom   = new DirectoryInfo(folderFrom);
            _diFolderTo     = new DirectoryInfo(folderTo);
            if (!_diFolderFrom.Exists) throw new DirectoryNotFoundException(folderFrom);
            //if (!_diFolderTo.Exists  ) throw new DirectoryNotFoundException(folderTo  );
            if (!FolderInScope(_diFolderFrom)) throw new InvalidOperationException("Please pick an upper bound folder alphabetically after the lower bound folder");
        }

        bool FolderInScope(DirectoryInfo folderFrom)
        {
            return string.Compare(folderFrom.FullName, _diFolderTo.FullName, StringComparison.InvariantCultureIgnoreCase) <= 0;
        }

        public event EventHandler<FolderEventArgs> StartingFolder;
        public event EventHandler<FolderEventArgs> EndingFolder;
        public event EventHandler<FileEventArgs  > FoundAFile;

        public void Run()
        {
            _cancel = false;
            var runningfolder = _diFolderFrom;
            while (!_cancel && FolderInScope(runningfolder))
            {
                Run(runningfolder);
                runningfolder = runningfolder.WalkNextFolder(FolderDirection.Forward);
            }
        }

        void Run(DirectoryInfo folderFrom)
        {
            if (_fileNameHandler.FolderExcludeMatch(folderFrom.Name)) return;
            var allMatchingFiles = folderFrom
                .EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                .Where(fi => _fileNameHandler.FileMatch(fi.Name, includeSmalls: true))
                .Select(fi => new
                {
                    fileInfo              = fi,
                    fileIsARegularFile    = _fileNameHandler.FilePatternIsMatch(fi.Name),
                    fileIsASmallFile      = _fileNameHandler.SmallPatternIsMatch(fi.Name),
                    fileIsARightSizedFile = _fileNameHandler.RightPatternIsMatch(fi.Name),
                    smallFileInfo         = new FileInfo(Path.Combine(folderFrom.FullName, _fileNameHandler.SmallFileNameMaker(fi.Name)))
                })
                .Select(fi => new
                {
                    fi.fileInfo,
                    fi.fileIsARegularFile,
                    fi.fileIsASmallFile,
                    fi.fileIsARightSizedFile,
                    fi.smallFileInfo,
                    SmallFileExists = fi.smallFileInfo.Exists
                })
                .ToArray();
            var regularFiles = allMatchingFiles
                .Where(fi => fi.fileIsARegularFile 
                         && !fi.fileIsASmallFile
                         && !fi.fileIsARightSizedFile
                )
                .ToArray();
            if (regularFiles.Length <= 0) return;

            OnStartingFolder(folderFrom, regularFiles.Length);
            if (_cancel) return;
            _progressIndicator?.InitProgress(regularFiles.Length, folderFrom.FullName);
            foreach (var fi in regularFiles)
            {
                _progressIndicator?.Progress(fi.fileInfo.Name);
                OnFoundAFile(
                    !fi.SmallFileExists
                    ? fi.smallFileInfo
                    : MakeSmallImage(fi.fileInfo, fi.smallFileInfo.FullName)
                );
                if (_cancel) return;
            }
            OnEndingFolder(folderFrom);
            _progressIndicator?.CloseProgress();
        }

        bool _cancel;

        void OnStartingFolder(DirectoryInfo di, int count) { StartingFolder?.Invoke(this, new FolderEventArgs { DirectoryInfo = di, MatchingFilesCount=count }); }
        void OnEndingFolder  (DirectoryInfo di) { EndingFolder  ?.Invoke(this, new FolderEventArgs { DirectoryInfo = di }); }
        void OnFoundAFile    (FileInfo      fi) { FoundAFile    ?.Invoke(this, new FileEventArgs   { FileInfo      = fi }); }

        void ProgressIndicator_CancelEvent(object sender, EventArgs e){ _cancel = true; }

        static FileInfo MakeSmallImage(FileInfo sourceFileInfo, string newFileName)
        {
            using (var img = Image.FromFile(sourceFileInfo.FullName))
            using (var newImg = img.Scale(0.2))
            {
                newImg.Save(newFileName, ImageFormat.Jpeg);
                return new FileInfo(newFileName);
            }
        }
    }

    public class FileEventArgs:EventArgs
    {
        public FileInfo FileInfo { get; set; }
    }

    public class FolderEventArgs:EventArgs
    {
        public DirectoryInfo DirectoryInfo { get; set; }
        public int MatchingFilesCount { get; set; }
    }

    public static class ImageExtensions
    {
        public static Image Scale(this Image image, double factor)
        {
            var wi = (int)(image.Width * factor);
            var he = (int)(image.Height * factor);
            var newImage = new Bitmap(wi, he);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, wi, he);

            return newImage;

        }
    }
}
