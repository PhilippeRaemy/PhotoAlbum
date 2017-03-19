

namespace PicturesSorter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using AlbumWordAddin;
    using AlbumWordAddin.UserPreferences;

    public partial class PictureSorterForm : Form
    {
        class Nodes : Tuple<LinkedListNode<ImageHost>, LinkedListNode<ImageHost>> {
            public Nodes(LinkedListNode<ImageHost> item1, LinkedListNode<ImageHost> item2) : base(item1, item2) { }
        }

        DirectoryInfo _currentDirectory;
        LinkedList<ImageHost> _currentFiles;
        Nodes _fileIndex;

        public PictureSorterForm()
        {
            InitializeComponent();
        }

        void PictureSorterForm_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = pictureBox2.Width = ClientRectangle.Width / 2;
            label1.Width = label2.Width = ClientRectangle.Width / 2;
        }

        void PictureSorterForm_Load(object sender, EventArgs e)
        {
            OpenFolder();
        }

        void OpenFolder()
        {
            var userPrefs = new PersistedUserPreferences();
            var fileNameHandler = new FileNameHandler(userPrefs);
            if (string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                folderBrowserDialog.SelectedPath = userPrefs.FolderImportStart;
            }
            folderBrowserDialog.ShowDialog();
            OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false), new DirectoryInfo(folderBrowserDialog.SelectedPath));
        }

        void OpenNextFolder(DirectoryInfo currentDirectory, FolderDirection folderDirection, string currentDirectoryName = null)
        {
            var strComp = folderDirection == FolderDirection.Forward
                    ? new Func<string, string, bool>(
                        (f1, f2) => string.Compare(f1, f2, StringComparison.InvariantCultureIgnoreCase) > 0)
                    : new Func<string, string, bool>(
                        (b1, b2) => string.Compare(b1, b2, StringComparison.InvariantCultureIgnoreCase) < 0)
                ;
            var sorter = folderDirection == FolderDirection.Forward
                    ? new Func<IEnumerable<DirectoryInfo>, IEnumerable<DirectoryInfo>>(e => e.OrderBy(d => d.Name))
                    : new Func<IEnumerable<DirectoryInfo>, IEnumerable<DirectoryInfo>>(
                        e => e.OrderByDescending(d => d.Name))
                ;
            var userPrefs = new PersistedUserPreferences();
            var fileNameHandler = new FileNameHandler(userPrefs);
            if (currentDirectory == null) return;
            if (!currentDirectory.Exists) return;
            var name = currentDirectoryName;
            foreach (
                var subDirectory in
                sorter(
                    currentDirectory.EnumerateDirectories()
                        .Where(d => string.IsNullOrWhiteSpace(name) || strComp(d.Name, name))))
            {
                OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false), subDirectory);
                return;
            }
            if (currentDirectory.Parent == null) return;
            var directory = currentDirectory;
            foreach (
                var subDirectory in
                sorter(currentDirectory.Parent.EnumerateDirectories().Where(d => strComp(d.Name, directory.Name))))
            {
                OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false), subDirectory);
                return;
            }
            OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false), currentDirectory.Parent);
        }

        void OpenFolderImpl(Func<string, bool> fileNameMatcher, DirectoryInfo selectedPath)
        {
            _currentDirectory = selectedPath;
            Text = _currentDirectory.FullName;
            _currentFiles = new LinkedList<ImageHost>(
                    _currentDirectory
                    .EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                    .Where         (f => fileNameMatcher(f.Name))
                    .OrderBy       (f => f.Name)
                    .Select        (f => new ImageHost { FileInfo = f })
                );
            switch (_currentFiles.Count)
            {
                case 0:
                    MessageBox.Show($"There are no pictures to sort in folder {selectedPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _fileIndex = new Nodes(null, null);
                    return;
                case 1:
                    _fileIndex = LoadPictures(new Nodes(_currentFiles.First, _currentFiles.First), 0, 0, noRelease: true);
                    break;
                default:
                    _fileIndex = LoadPictures(new Nodes(_currentFiles.First, _currentFiles.First.Next), 0, 0, noRelease: true);
                    break;
            }
        }

        Nodes LoadPictures(Nodes idx, int step1, int step2, bool noRelease = false)
        {
            var rc = SelectIndexes(idx, step1, step2);
            rc.Item1.Value.Render(pictureBox1, label1);
            rc.Item2.Value.Render(pictureBox2, label2);
            if (!noRelease)
            {
                idx?.Item1.Value.Release();
                idx?.Item2.Value.Release();
            }
            return rc;
        }

        Nodes SelectIndexes(Nodes idx, int step1, int step2)
            => new Nodes(idx.Item1.SafeStep(step1), idx.Item2.SafeStep(step2));

        void LoadPicture(PictureBox pb, Label lbl, FileSystemInfo fi)
        {
            lbl.Text = fi.FullName;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            using (var stream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
            {
                pb.Image = Image.FromStream(stream);
            }
            pb.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return ProcessCmdKeyImpl(ref msg, keyData);
        }

        bool ProcessCmdKeyImpl(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left                             : _fileIndex = LoadPictures(_fileIndex, -1, -1); break;
                case Keys.Right                            : _fileIndex = LoadPictures(_fileIndex, +1, +1); break;
                case Keys.Control | Keys.Left              : _fileIndex = LoadPictures(_fileIndex, -1,  0); break;
                case Keys.Control | Keys.Shift | Keys.Left : _fileIndex = LoadPictures(_fileIndex,  0, -1); break;
                case Keys.Control | Keys.Right             : _fileIndex = LoadPictures(_fileIndex,  1,  0); break;
                case Keys.Control | Keys.Shift | Keys.Right: _fileIndex = LoadPictures(_fileIndex,  0,  1); break;
                case Keys.NumPad1:
                case Keys.D1:
                    ArchiveLeftPicture();
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    ArchiveRightPicture();
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        void ArchiveRightPicture()
        {
            ArchivePicture(_fileIndex.Item2.Value, 0, 1);
        }

        void ArchiveLeftPicture()
        {
            ArchivePicture(_fileIndex.Item1.Value, -1, 0);
        }

        void ArchivePicture(ImageHost imageHost, int step1, int step2)
        {
            _fileIndex = LoadPictures(_fileIndex, step1, step2);
            imageHost.ArchivePicture();
            imageHost.Dispose();
            _currentFiles.Remove(imageHost);
        }

        void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, -1, -1);
        }

        void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 1, 1);
        }

        void leftPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, -1, 0);
        }
        void leftNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 1, 0);
        }

        void rightNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 0, 1);
        }

        void rightPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 0, -1);
        }

        void archiveLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveLeftPicture();
        }

        void archiveRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveRightPicture();
        }

        void pickDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolder();
        }

        void RotateLeftClock_Click(object sender, EventArgs e)
        {

        }

        void RotateLeftAnti_Click(object sender, EventArgs e)
        {

        }

        void RotateRightClock_Click(object sender, EventArgs e)
        {

        }

        void RotateRightAnti_Click(object sender, EventArgs e)
        {

        }

        void nextFolder_Click(object sender, EventArgs e)
        {
            OpenNextFolder(_currentDirectory, FolderDirection.Forward);
        }

        void previousFolder_Click(object sender, EventArgs e)
        {
            OpenNextFolder(_currentDirectory, FolderDirection.Backward);
        }

        void openInWindowsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo =
                {
                    FileName = @"explorer.exe",
                    Arguments = @"file:\\\" + _currentDirectory
                }
            };
            p.Start();
        }
    }

    internal static class ArrayExtensions {
        public static int NextNonNullIndex<T>(this T[] a, int startIndex, int step) 
        {
            for( var i = 1; i <= a.Length;i++)
                if(!Equals(a[(startIndex + i * step + a.Length) % a.Length], default(T)))
                    return (startIndex + i * step + a.Length) % a.Length;
            return -1;
        }
    }

    internal static class GenericExtentions {
        public static LinkedListNode<T> SafeNext<T>(this LinkedListNode<T> lln) => lln.Next ?? lln.List.First;
        public static LinkedListNode<T> SafePrev<T>(this LinkedListNode<T> lln) => lln.Previous ?? lln.List.Last;
        public static LinkedListNode<T> SafeStep<T>(this LinkedListNode<T> lln, int step) 
            => step < 0 ? lln.SafePrev()
             : step > 0 ? lln.SafeNext()
             : lln;
    }

    internal class ImageHost : IDisposable {
        Image _image;
        public  Image Image
        {
            get
            {
                if (_image != null) return _image;
                using (var stream = new FileStream(FileInfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    _image = Image.FromStream(stream);
                }
                return _image;
            }
        }
        public FileInfo FileInfo { get; set; }
        public string FullName => FileInfo.FullName;

        int _useCount;
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
             || FileInfo.Directory     == null 
             || FileInfo.DirectoryName == null
            ) return;
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            var smallFile = new FileInfo(fileNameHandler.SmallFileNameMaker(FileInfo.FullName));


            var di = string.Equals(FileInfo.Directory.Name, "spare", StringComparison.InvariantCultureIgnoreCase)
                    ? FileInfo.Directory
                    : new DirectoryInfo(Path.Combine(FileInfo.DirectoryName, "spare"));
            di.Create();
            File.Move(FileInfo.FullName, Path.Combine(di.FullName, FileInfo.Name));
            if (smallFile.Exists)
            {
                File.Move(smallFile.FullName, Path.Combine(di.FullName, smallFile.Name));
            }
        }

        public void Dispose()
        {
            _image?.Dispose();
            _image = null;
        }

        public void Render(PictureBox pictureBox, Label label)
        {
            if (label.Text != FileInfo.Name)
            {
                pictureBox.Image = Image;
                label.Text = FileInfo.Name;
                pictureBox.Refresh();
            }
            _useCount++;
        }
    }

    internal enum FolderDirection { Forward, Backward}
}
