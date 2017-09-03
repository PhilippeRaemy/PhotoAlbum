namespace AlbumWordAddin
{
    using System;
    using System.IO;
    using System.Linq;

    internal class DualFile
    {
        readonly string _documentFullFileName;
        public FileInfo FileInfo { get; }
        public FileInfo DualFileInfo { get; }
        public bool DualExists => DualFileInfo.Exists;
        public bool Exists => FileInfo.Exists;

        public DualFile(string fullFileName, string documentFullFileName, Func<string, string> fileNameMaker)
        {
            _documentFullFileName = documentFullFileName;
            FileInfo =  ValidateFileInfo(new FileInfo(fullFileName));

            if (FileInfo.DirectoryName == null) return;
            DualFileInfo = ValidateFileInfo(new FileInfo(Path.Combine(FileInfo.DirectoryName, fileNameMaker(FileInfo.Name))));
        }

        FileInfo ValidateFileInfo(FileInfo fi)
        {
            if(fi.Exists) return fi;
            var documentFileInfo = new FileInfo(_documentFullFileName);
            if (documentFileInfo.DirectoryName == null) return fi;
            var documentFolder = new DirectoryInfo(documentFileInfo.DirectoryName);
            var candidate = documentFolder.GetFiles(fi.Name).FirstOrDefault();
            if (candidate?.Exists ?? false) return candidate;
            candidate = documentFolder.Parent?.GetFiles(fi.Name).FirstOrDefault();
            if (candidate?.Exists ?? false) return candidate;
            return fi;
        }
    }
}