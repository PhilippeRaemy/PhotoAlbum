namespace PicturesSorter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AlbumWordAddin;
    using AlbumWordAddin.UserPreferences;

    internal class ImageHost : IDisposable {
        Image _image;
        Image _smallImage;
        Image Image
        {
            get
            {
                lock (this)
                {
                    ScheduleReset();
                    if (_image != null) return _image;
                    using (var stream = new FileStream(FullName, FileMode.Open, FileAccess.Read))
                    {
                        return _image = Image.FromStream(stream);
                    }
                }
            }
        }

        void ScheduleReset()
        {
            _loadNumber++;
            if (_delayedResetTask == null || _delayedResetTask.IsCompleted)
            {
                _delayedResetTask = Task.Factory.StartNew(DelayedReset);
            }
        }

        Image SmallImage
        {
            get
            {
                lock (this)
                {
                    ScheduleReset();
                    if (_smallImage != null) return _smallImage;
                    using (var stream = new FileStream(GetSmallFile().FullName, FileMode.Open, FileAccess.Read))
                    {
                        return _smallImage = Image.FromStream(stream);
                    }
                }
            }
        }

        void DelayedReset()
        {
            var loadNumber = _loadNumber;
            var wait = 0;
            while (++wait < 60) { 
                Thread.Sleep(1000);
                if (loadNumber != _loadNumber)
                {
                    loadNumber = _loadNumber;
                    wait = 0;
                }
            }
            Reset();
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

        public void Release()
        {
            if (--_useCount < 0) return;
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

            var di = string.Equals(FileInfo.Directory.Name, "spare", StringComparison.InvariantCultureIgnoreCase)
                ? FileInfo.Directory.Parent
                : new DirectoryInfo(Path.Combine(FileInfo.DirectoryName, "spare"));
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
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            var smallFile = new FileInfo(fileNameHandler.SmallFileNameMaker(FileInfo.FullName));
            return smallFile;
        }
        FileInfo GetRightFile()
        {
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            var rightFile = new FileInfo(fileNameHandler.RightFileNameMaker(FileInfo.FullName));
            return rightFile;
        }

    public void Dispose()
        {
            _image?.Dispose();
            _image = null;
        }

        public bool Render(PictureBox pictureBox, Label label, bool force = false)
        {
            if (force || (string)label.Tag != FileInfo.FullName)
            {
                try
                {
                    pictureBox.Image = Image;
                    label.Tag = FileInfo.FullName;
                    pictureBox.Refresh();
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                    var host = pictureBox.Parent;
                    var bogusPic = pictureBox;
                    host.Controls.Remove(bogusPic);
                    pictureBox = new PictureBox();
                    foreach (var prop in typeof(PictureBox).GetProperties().Where(p=>p.CanRead && p.CanWrite))
                    {
                        prop.SetValue(pictureBox, prop.GetValue(bogusPic));
                    }
                    host.Controls.Add(pictureBox);
                    return false;
                }
            }
            // ReSharper disable once LocalizableElement
            label.Text = $"{FileInfo.Name} - {1 + Parent.IndexOf(this)}/{Parent.Count}";
            _useCount++;
            return true;
        }

        public void Rotate(RotateFlipType rotateFlipType)
        {
            Image.RotateFlip(rotateFlipType);
            Image.Save(FullName, ImageFormat.Jpeg);
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