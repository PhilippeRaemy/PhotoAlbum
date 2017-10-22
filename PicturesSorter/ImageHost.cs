namespace PicturesSorter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AlbumWordAddin;

    /*TODO: 
     * handle all derived images (small, right, etc.) at once.
     * Trace when the images are disposed: there must be a bad case there!
     */

    internal class ImageHost : IDisposable {

        string Spare { get; }
        FileNameHandler FileNameHandler { get; }
         

        Image _image;
        Image _smallImage;
        Image Image
        {
            get
            {
                lock (this)
                {
                    // ScheduleReset();
                    if (_image != null) return _image;
                    using (var stream = new FileStream(FullName, FileMode.Open, FileAccess.Read))
                    {
                        Trace.WriteLine($"ImageHost reading from {FileInfo.FullName}");
                        return _image = Image.FromStream(stream);
                    }
                }
            }
        }

        Image SmallImage
        {
            get
            {
                lock (this)
                {
                    // ScheduleReset();
                    if (_smallImage != null) return _smallImage;
                    using (var stream = new FileStream(GetSmallFile().FullName, FileMode.Open, FileAccess.Read))
                    {
                        return _smallImage = Image.FromStream(stream);
                    }
                }
            }
        }

        /// <summary>
        /// Reset cached image and small image binaries, so that they are re-read from file on next access.
        /// </summary>
        void Reset()
        {
            lock (this)
            {
                _image = _smallImage = null;
            }
        }

        public FileInfo FileInfo { get; set; }
        string FullName => FileInfo.FullName;
        public LinkedList<ImageHost> Parent { private get; set; }

        int _useCount;
        Task _delayedResetTask;
        int _loadNumber;

        public ImageHost(FileNameHandler fileNameHandler, string spare)
        {
            FileNameHandler = fileNameHandler;
            Spare = spare;
        }

        public void Release()
        {
            Trace.WriteLine($"ImageHost releasing. _useCount={_useCount-1}: {FileInfo.FullName}");
            if (--_useCount > 0) return;
            Dispose();
            _useCount = 0;
        }

        public void ArchivePicture()
        {
            if (FileInfo               == null 
                || !FileInfo.Exists
            ) return;
            var smallFile = GetSmallFile();
            var rightFile = GetRightFile();

            var di = string.Equals(FileInfo.Directory.Name, Spare, StringComparison.InvariantCultureIgnoreCase)
                ? FileInfo.Directory.Parent
                : new DirectoryInfo(Path.Combine(FileInfo.DirectoryName, Spare));
            if (di == null) return;
            di.Create();
            File.Move(FileInfo.FullName, Path.Combine(di.FullName, FileInfo.Name));
            if (smallFile.Exists)
            {
                File.Move(smallFile.FullName, Path.Combine(di.FullName, smallFile.Name));
            }
            if (rightFile.Exists)
            {
                File.Move(rightFile.FullName, Path.Combine(di.FullName, rightFile.Name));
            }
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
            Trace.WriteLine($"IMageHost disposing of {FileInfo.FullName}");
            Trace.WriteLine(new StackTrace());

            _image?.Dispose();
            _image = null;
        }

        public bool Render(PictureBox pictureBox, Label label, bool force = false)
        {
            if (force || (string)label.Tag != FileInfo.FullName)
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
                        _image?.Dispose();
                        _image = null;
                    }
                    pictureBox.Image = Image;
                    pictureBox.Refresh();
                    Trace.WriteLine($"ImageHost has replaced pictureBox. {FileInfo.FullName}");
                    return false;
                }
            }
            // ReSharper disable once LocalizableElement
            label.Text = $"{FileInfo.Name} - {1 + Parent.IndexOf(this)}/{Parent.Count}";
            _useCount++;
            Trace.WriteLine($"ImageHost Render. _useCount={_useCount}: {FileInfo.FullName}");
            return true;
        }

        public void Rotate(RotateFlipType rotateFlipType)
        {
            Image.RotateFlip(rotateFlipType);
            try
            {
                Image.Save(FullName, ImageFormat.Jpeg);
            }
            catch //  sometimes the save fails: save to temp and copy
            {
                var tempFile = Path.GetTempFileName();
                Image.Save(tempFile, ImageFormat.Jpeg);
                FileInfo.Delete();
                new FileInfo(tempFile).MoveTo(FullName);
                FileInfo = new FileInfo(FullName);
            }
            var smallImg = GetSmallFile();
            if (smallImg.Exists)
            {
                SmallImage.RotateFlip(rotateFlipType);
                SmallImage.Save(smallImg.FullName, ImageFormat.Jpeg);
            }
            Reset();
        }
    }
}