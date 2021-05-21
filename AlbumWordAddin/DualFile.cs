namespace AlbumWordAddin
{
    using System;
    using System.IO;
    using System.Linq;

    internal class DualFile
    {
        readonly string _documentFullFileName;
        public FileInfo FileInfo      { get; private set; }
        public FileInfo LargeFileInfo { get; private set; }
        public FileInfo DualFileInfo  { get; private set; }
        public bool LargeExists => LargeFileInfo.Exists;
        public bool DualExists  => DualFileInfo .Exists;
        public bool Exists      => FileInfo     .Exists;

        public DualFile(string fullFileName, string documentFullFileName, Func<string, string> fileNameMaker, Func<string, string> largeFileNameMaker)
        {
            _documentFullFileName = documentFullFileName;
            FileInfo =  ValidateFileInfo(new FileInfo(fullFileName));

            // ReSharper disable once AssignNullToNotNullAttribute
            LargeFileInfo = ValidateFileInfo(new FileInfo(Path.Combine(FileInfo.DirectoryName, largeFileNameMaker(FileInfo.Name))));
            DualFileInfo  = ValidateFileInfo(new FileInfo(Path.Combine(FileInfo.DirectoryName, fileNameMaker(FileInfo.Name))));
        }

        FileInfo ValidateFileInfo(FileInfo fi)
        {
            if(fi.Exists) return fi;
            var documentFileInfo = new FileInfo(_documentFullFileName);
            // ReSharper disable once AssignNullToNotNullAttribute
            var documentFolder = new DirectoryInfo(documentFileInfo.DirectoryName);
            var candidate = documentFolder.GetFiles(fi.Name).FirstOrDefault();
            if (candidate?.Exists ?? false) return candidate;
            candidate = documentFolder.Parent?.GetFiles(fi.Name).FirstOrDefault();
            if (candidate?.Exists ?? false) return candidate;
            return fi;
        }

        public void Refresh()
        {
            FileInfo      = new FileInfo(FileInfo.FullName);
            LargeFileInfo = new FileInfo(LargeFileInfo.FullName);
            DualFileInfo  = new FileInfo(DualFileInfo.FullName); 
        }
    }
}