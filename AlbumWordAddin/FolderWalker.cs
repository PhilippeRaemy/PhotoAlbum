namespace AlbumWordAddin
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using MoreLinq;
    using VstoEx.Progress;

    public class FolderWalker
    {
        readonly FileNameHandler _fileNameHandler;
        readonly IProgress       _progressIndicator;
        readonly DirectoryInfo   _diFolderFrom;
        readonly DirectoryInfo   _diFolderTo;

        public FolderWalker(
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
            if (string.Compare(folderFrom, folderTo, StringComparison.InvariantCultureIgnoreCase) > 0) throw new InvalidOperationException("Please pick an upper bound folder alphabetically after the lower bound folder");
        }

        public event EventHandler<FolderEventArgs> StartingFolder;
        public event EventHandler<FolderEventArgs> EndingFolder;
        public event EventHandler<FileEventArgs  > FoundAFile;

        public void Run()
        {
            _cancel = false;
            Run(_diFolderFrom);
        }

        void Run(DirectoryInfo folderFrom)
        {
            if (_fileNameHandler.FolderExcludeMatch(folderFrom.Name)) return;
            var matchingFiles1 = folderFrom
                .EnumerateFiles("*",SearchOption.TopDirectoryOnly)
                .Where(fi => _fileNameHandler.FileMatch(fi.Name, includeSmalls:true))
                .Select(fi=>new
                {
                    fileInfo=fi,
                    fileMatch  = _fileNameHandler.FilePatternIsMatch(fi.Name),
                    smallMatch = _fileNameHandler.SmallPatternIsMatch(fi.Name),
                    smallName  = Path.Combine(folderFrom.FullName, _fileNameHandler.SmallFileNameMaker(fi.Name))
                })
                .ToArray();
            var matchingFiles2=matchingFiles1
                .Select(fi=> new
                {
                    fi.fileInfo, fi.fileMatch, fi.smallMatch, fi.smallName, 
                    SmallFileExists = fi.smallMatch || new FileInfo(fi.smallName).Exists
                })
                .ToArray();
            var matchingFiles=matchingFiles2
                .Where(fi => fi.fileMatch && !fi.smallMatch && !fi.SmallFileExists // this is an new hi res file
                          || fi.smallMatch //  this is an already prepared low res file
                )
                .ToArray();
            if (matchingFiles.Length > 0)
            {
                OnStartingFolder(folderFrom, matchingFiles.Length);
                if (_cancel) return;
                _progressIndicator?.InitProgress(matchingFiles.Length, folderFrom.FullName);
                foreach (var fi in matchingFiles)
                {
                    if (fi.smallMatch)
                    {
                        _progressIndicator?.Progress(fi.fileInfo.Name);
                        OnFoundAFile(fi.fileInfo);
                        if (_cancel) return;
                        continue;
                    }
                    if (!fi.fileMatch) continue;
                    _progressIndicator?.Progress(fi.fileInfo.Name);
                    OnFoundAFile(MakeSmallImage(fi.fileInfo, fi.smallName));
                    if (_cancel) return;
                }
                OnEndingFolder(folderFrom);
                _progressIndicator?.CloseProgress();
            }
            if (_cancel) return;
            folderFrom
                .EnumerateDirectories()
                .TakeWhile(di=>string.Compare(di.FullName.Substring(0, _diFolderTo.FullName.Length), _diFolderTo.FullName, StringComparison.InvariantCultureIgnoreCase) <= 0)
                .ForEach(Run);
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
