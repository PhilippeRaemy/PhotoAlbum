namespace AlbumWordAddin
{
    using System;
    using System.IO;

    internal class DualFile
    {
        public FileInfo FileInfo { get; }
        public FileInfo DualFileInfo { get; }
        public bool DualExists => DualFileInfo.Exists;
        public bool Exists => FileInfo.Exists;

        public DualFile(string fullFileName, string documentFullFileName, Func<string, string> fileNameMaker)
        {
            FileInfo = new FileInfo(fullFileName);

            if (FileInfo.DirectoryName == null) return;
            DualFileInfo = new FileInfo(Path.Combine(FileInfo.DirectoryName, fileNameMaker(FileInfo.Name)));
        }



    }
}