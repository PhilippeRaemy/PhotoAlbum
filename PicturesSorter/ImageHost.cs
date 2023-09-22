using UserPreferences;


namespace PicturesSorter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using MoreLinq;
    using PictureHandler;

    internal class ImageHost : IDisposable {

        string ShelfName { get; }
        FileNameHandler FileNameHandler { get; }

        Image[] _images;
        public Image Image => Images.First();
        readonly Func<FileInfo>[] _imageNamesGetters; 
        IEnumerable<Image> Images => _images.Select((im, i) => im ?? (_images[i] = GetImage(_imageNamesGetters[i]())));

        Image GetImage(FileInfo imageFullPathName)
        {
            lock (this)
                return PictureHelper.ReadImageFromFileInfo(imageFullPathName);
        }

        /// <summary>
        /// Reset cached image and small image binaries, so that they are re-read from file on next access.
        /// </summary>
        void Reset()
        {
            lock (this)
            {
                _images = new Image[_images.Length];
            }
        }

        public FileInfo FileInfo { get; private set; }
        string FullName => FileInfo.FullName;
        public LinkedList<ImageHost> Parent { private get; set; }

        int _useCount;

        ImageHost(FileInfo fileInfo)
        {
            _images = new Image[3];
            _imageNamesGetters = new Func<FileInfo>[]
            {
                () => FileInfo,
                GetSmallFile,
                GetRightFile
            };
            FileInfo = fileInfo;
        }

        public ImageHost(FileNameHandler fileNameHandler, string shelfName, FileInfo fileInfo) : this(fileInfo)
        {
            FileNameHandler = fileNameHandler;
            ShelfName = shelfName;
        }

        public void Release()
        {
            Trace.WriteLine($"ImageHost releasing. _useCount={_useCount-1}: {FileInfo.FullName}");
            if (--_useCount > 0) return;
            Dispose();
            _useCount = 0;
        }

        /// <summary>
        /// Shelve or unshelve a picture, depending on the name of the directory the picture is in.
        /// </summary>
        /// <returns>The path teh picture file is moved to</returns>
        public string ShelvePicture(bool delete = false)
        {
            if (FileInfo                  == null 
                || !FileInfo.Exists
                || FileInfo.Directory     == null
                || FileInfo.DirectoryName == null
            ) return null;

            if (delete)
            {
                FileInfo.Delete();
                return string.Empty;
            }

            var di = string.Equals(FileInfo.Directory.Name, ShelfName, StringComparison.InvariantCultureIgnoreCase)
                ? FileInfo.Directory.Parent
                : new DirectoryInfo(Path.Combine(FileInfo.DirectoryName, ShelfName));
            return MovePicture(di);
        }

        string MovePicture(DirectoryInfo destinationDirectoryInfo)
        {
            if (FileInfo == null
                || !FileInfo.Exists
                || FileInfo.Directory == null
                || FileInfo.DirectoryName == null
                || destinationDirectoryInfo == null
            ) return null;

            destinationDirectoryInfo.Create();
            _imageNamesGetters
                .Select(ig => ig())
                .Where(fi => fi.Exists)
                .ForEach(fi => File.Move(fi.FullName, Path.Combine(destinationDirectoryInfo.FullName, fi.Name)));
            Reset();
            FileInfo = new FileInfo(Path.Combine(destinationDirectoryInfo.FullName, FileInfo.Name));
            return FileInfo.FullName;
        }

        FileInfo GetSmallFile()
        {
            var smallFile = new FileInfo(FileNameHandler.SmallFileNameMaker(FileInfo.FullName));
            return smallFile;
        }
        FileInfo GetRightFile()
        {
            var rightFile = new FileInfo(FileNameHandler.RightFileNameMaker(FileInfo.FullName));
            return rightFile;
        }

        public void Dispose()
        {
            Trace.WriteLine($"ImageHost disposing of {FileInfo.FullName}");

            for (var i = 0; i < _images.Length; i++)
            {
                _images[i]?.Dispose();
                _images[i] = null;
            }
        }

        public void Render(PictureBox pictureBox, Label label, bool force = false)
        {
            if (Image != null && force || (string)label.Tag != FileInfo.FullName)
            {
                try
                {
                    label.Tag = FileInfo.FullName;
                    pictureBox.Image = Image;
                    pictureBox.Refresh();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"ImageHost Render error: {e}. _useCount={_useCount}: {FileInfo.FullName}");
                    var host = pictureBox.Parent;
                    var bogusPic = pictureBox;
                    host.Controls.Remove(bogusPic);
                    pictureBox = new PictureBox();
                    foreach (var prop in typeof(PictureBox).GetProperties().Where(p=>p.CanRead && p.CanWrite))
                    {
                        prop.SetValue(pictureBox, prop.GetValue(bogusPic));
                    }
                    host.Controls.Add(pictureBox);
                    lock (this)
                    {
                        Dispose();
                    }
                    pictureBox.Image = Image;
                    pictureBox.Refresh();
                    Trace.WriteLine($"ImageHost has replaced pictureBox. {FileInfo.FullName}");
                }
            }
            // ReSharper disable once LocalizableElement
            label.Text = $"{FileInfo.Name} - {1 + Parent.IndexOf(this)}/{Parent.Count} - {FileInfo.LastWriteTime:g} - {FormatLength(FileInfo.Length)}";
            _useCount++;
            Trace.WriteLine($"ImageHost Render. _useCount={_useCount}: {FileInfo.FullName}");
        }

        string FormatLength(long fileInfoLength) =>
            fileInfoLength < 1024
                ? $"{fileInfoLength}b"
                : FormatLength(fileInfoLength/1024f, new[] {"Kb", "Mb", "Gb", "Tb"});

        string FormatLength(float fileInfoLength, string[] unit)
            => fileInfoLength < 1024 || unit.Length == 1
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                ? fileInfoLength > 100 || Math.Round(fileInfoLength, 0) == Math.Round(fileInfoLength, 1)
                    ? $"{fileInfoLength:f0}{unit[0]}"
                    : $"{fileInfoLength:f1}{unit[0]}"
                : FormatLength(fileInfoLength/1024f, unit.Skip(1).ToArray());

        public void Rotate(RotateFlipType rotateFlipType)
        {
            for (var i = 0; i < _images.Length; i++)
            {
                if (_images[i] == null) continue;
                _images[i].RotateFlip(rotateFlipType);
                var tempFile = Path.GetTempFileName();
                _images[i].Save(tempFile, ImageFormat.Jpeg);
                var fi = _imageNamesGetters[i]();
                if(fi.Exists) fi.Delete();
                new FileInfo(tempFile).MoveTo(fi.FullName);
            }
            Reset();
        }
    }
}