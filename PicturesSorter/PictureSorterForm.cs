

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
    using MoreLinq;

    public partial class PictureSorterForm : Form
    {
        class NodesTuple // : Tuple<LinkedListNode<ImageHost>, LinkedListNode<ImageHost>>
        {
            public NodesTuple(LinkedListNode<ImageHost> left, LinkedListNode<ImageHost> right)
            {
                Left = left;
                Right = right;
            }
            public LinkedListNode<ImageHost> Left { get; }
            public LinkedListNode<ImageHost> Right { get; }
        }

        enum Side { Left, Right}

        DirectoryInfo _currentDirectory;
        LinkedList<ImageHost> _currentFiles;
        NodesTuple _fileIndex;

        public PictureSorterForm()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        void PictureSorterForm_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = pictureBox2.Width = ClientRectangle.Width / 2;
            labelLeft.Width = labelRight.Width = ClientRectangle.Width / 2;
        }

        void PictureSorterForm_Load(object sender, EventArgs e)
        {
            OpenFolder();
        }

        void PictureSorterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            new PersistedUserPreferences {FolderImportStart = _currentDirectory.FullName}.Save();
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
            OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false),
                new DirectoryInfo(folderBrowserDialog.SelectedPath), fileNameHandler, userPrefs.ShelfName);
            userPrefs.Save();
        }

        void OpenNextFolder(DirectoryInfo currentDirectory, FolderDirection folderDirection)
        {
            var folder = GetNextFolder(currentDirectory, folderDirection);
            if (folder == null) return;
            var userPrefs = new PersistedUserPreferences();
            var fileNameHandler = new FileNameHandler(userPrefs);
            OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false), folder, fileNameHandler, userPrefs.ShelfName);
            userPrefs.Save();
        }

        public static DirectoryInfo GetNextFolder(DirectoryInfo currentDirectory, FolderDirection folderDirection)
        {
            switch (folderDirection)
            {
                case FolderDirection.Forward: return GetNextFolder(currentDirectory);
                case FolderDirection.Backward: return GetPreviousFolder(currentDirectory);
                default:
                    throw new ArgumentOutOfRangeException(nameof(folderDirection), folderDirection, null);
            }
        }

        static DirectoryInfo GetNextFolder(DirectoryInfo currentDirectory, bool ignoreSubFolders = false)
        {
            while (true)
            {
                var comparer = new Func<string, string, bool>((f1, f2) => string.Compare(f1, f2, StringComparison.InvariantCultureIgnoreCase) > 0);
                if (currentDirectory == null) return null;
                if (!currentDirectory.Exists) return null;
                if (!ignoreSubFolders)
                {
                    foreach (var subDirectory in currentDirectory.EnumerateDirectories().OrderBy(d => d.Name))
                    {
                        return subDirectory;
                    }
                }
                if (currentDirectory.Parent == null) return null;
                var directory = currentDirectory;
                foreach (var subDirectory in
                    currentDirectory.Parent.EnumerateDirectories().OrderBy(d => d.Name).Where(d => comparer(d.Name, directory.Name)))
                {
                    return subDirectory;
                }
                currentDirectory = currentDirectory.Parent;
                ignoreSubFolders = true;
            }
        }

        static DirectoryInfo GetPreviousFolder(DirectoryInfo currentDirectory, bool diveSubFolders = false)
        {

            var comparer = new Func<string, string, bool>((b1, b2) => string.Compare(b1, b2, StringComparison.InvariantCultureIgnoreCase) < 0);
            if (currentDirectory == null) return null;
            if (!currentDirectory.Exists) return null;
            if (currentDirectory.Parent == null) return null;
            var directory = currentDirectory;
            if (diveSubFolders)
            {
                foreach (var subDirectory in currentDirectory.EnumerateDirectories().OrderByDescending(d => d.Name))
                {
                    return GetPreviousFolder(subDirectory, diveSubFolders: true);
                }
                return currentDirectory;
            }

            foreach (
                var subDirectory in
                currentDirectory.Parent.EnumerateDirectories().OrderByDescending(d => d.Name).Where(d => comparer(d.Name, directory.Name)))
            {
                return GetPreviousFolder(subDirectory, diveSubFolders: true);
            }

            return currentDirectory.Parent;
        }

        void OpenFolderImpl(Func<string, bool> fileNameMatcher, DirectoryInfo selectedPath, FileNameHandler fileNameHandler, string shelf)
        {
            _shelvedFiles.Clear();
            buttonUndo.Enabled = false;

            _currentDirectory = selectedPath;
            Text = _currentDirectory.FullName;
            _currentFiles = new LinkedList<ImageHost>(
                _currentDirectory
                    .EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                    .Where         (f => fileNameMatcher(f.Name))
                    .OrderBy       (f => f.Name)
                    .Select        (f => new ImageHost(fileNameHandler, shelf, f))
            );
            _currentFiles.ForEach(ih => ih.Parent = _currentFiles);
            switch (_currentFiles.Count)
            {
                case 0:
                    // MessageBox.Show($"There are no pictures to sort in folder {selectedPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _fileIndex = new NodesTuple(null, null);
                    return;
                case 1:
                    _fileIndex = LoadPictures(new NodesTuple(_currentFiles.First, _currentFiles.First), 0, 0, noRelease: true);
                    break;
                default:
                    _fileIndex = LoadPictures(new NodesTuple(_currentFiles.First, _currentFiles.First.Next), 0, 0, noRelease: true);
                    break;
            }
            buttonShelfLeft.Image = buttonShelfRight.Image =
                (_currentDirectory.Parent?.Name.Equals(shelf, StringComparison.InvariantCultureIgnoreCase) ?? false)
                    ? Properties.Resources.SmallUnshelve
                    : Properties.Resources.SmallShelve;
        }

        NodesTuple LoadPictures(NodesTuple idx, int stepLeft, int stepRight, bool noRelease = false)
        {
            var rc = SelectIndexes(idx, stepLeft, stepRight);
            if (rc.Left == null || rc.Right == null) return null;
            Trace.WriteLine($"LoadPictures({rc.Left.Value.FileInfo.Name}, {rc.Right.Value.FileInfo.Name}, {stepLeft}, {stepRight}, {noRelease})");
            rc.Left?.Value?.Render(pictureBox1, labelLeft);
            rc.Right?.Value?.Render(pictureBox2, labelRight);
            if (!noRelease)
            {
                idx?.Left?.Value?.Release();
                idx?.Right?.Value?.Release();
            }
            Trace.WriteLine($"LoadPictures returns({rc.Left.Value.FileInfo.Name}, {rc.Right.Value.FileInfo.Name})");
            return rc;
        }

        static NodesTuple SelectIndexes(NodesTuple idx, int step1, int step2)
            => new NodesTuple(idx?.Left.SafeStep(step1), idx?.Right.SafeStep(step2));

/*
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
*/

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
                case Keys.PageDown: OpenNextFolder(_currentDirectory, FolderDirection.Forward ); break;
                case Keys.PageUp  : OpenNextFolder(_currentDirectory, FolderDirection.Backward); break;
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
            _shelvedFiles.Push(Tuple.Create(_fileIndex.Right.Value.ShelvePicture(), Side.Right));
            buttonUndo.Enabled = true;
            ArchivePicture(_fileIndex.Right.Value, 0, 1);
        }

        void ArchiveLeftPicture()
        {
            _shelvedFiles.Push(Tuple.Create(_fileIndex.Left.Value.ShelvePicture(), Side.Left));
            buttonUndo.Enabled = true;
            ArchivePicture(_fileIndex.Left.Value, -1, 0);
        }

        void ArchivePicture(ImageHost imageHost, int step1, int step2)
        {
            _fileIndex = LoadPictures(_fileIndex, step1, step2);
            _currentFiles.Remove(imageHost);
            imageHost.Dispose();
        }

        readonly Stack<Tuple<string, Side>> _shelvedFiles =new Stack<Tuple<string, Side>>();

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
            _fileIndex.Left.Value.Rotate(RotateFlipType.Rotate90FlipNone);
            _fileIndex.Left.Value.Render(pictureBox1, labelLeft, force: true);
        }

        void RotateLeftAnti_Click(object sender, EventArgs e)
        {
            _fileIndex.Left.Value.Rotate(RotateFlipType.Rotate270FlipNone);
            _fileIndex.Left.Value.Render(pictureBox1, labelLeft, force: true);
        }

        void RotateRightClock_Click(object sender, EventArgs e)
        {
            _fileIndex.Right.Value.Rotate(RotateFlipType.Rotate90FlipNone);
            _fileIndex.Right.Value.Render(pictureBox2, labelRight, force: true);
        }

        void RotateRightAnti_Click(object sender, EventArgs e)
        {
            _fileIndex.Right.Value.Rotate(RotateFlipType.Rotate270FlipNone);
            _fileIndex.Right.Value.Render(pictureBox2, labelRight, force: true);
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
                    Arguments = @"file:\\\" + _currentDirectory.FullName
                }
            };
            p.Start();
        }

        void buttonNavigateLeftLeft_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, -1, 0);
        }

        void buttonNavigateRight_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 1, 0);
        }

        void buttonNavigateBothLeft_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, -1, -1);
        }

        void buttonNavigateBothRight_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 1, 1);
        }

        void buttonNavigateRightLeft_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 0, -1);
        }

        void buttonNavigateRightRight_Click(object sender, EventArgs e)
        {
            _fileIndex = LoadPictures(_fileIndex, 0, 1);
        }

        void buttonShelfRight_Click(object sender, EventArgs e)
        {
            ArchiveRightPicture();
        }

        void buttonShelfLeft_Click(object sender, EventArgs e)
        {
            ArchiveLeftPicture();
        }

        void buttonUndo_Click(object sender, EventArgs e)
        {
            if (_shelvedFiles.Count == 0) return;
            if (_shelvedFiles.Count == 1) buttonUndo.Enabled = false;

            var userPrefs = new PersistedUserPreferences();
            var shelvedFile = _shelvedFiles.Pop();
            var host = new ImageHost(
                new FileNameHandler(userPrefs), 
                userPrefs.ShelfName,
                new FileInfo(shelvedFile.Item1)
            ) {Parent = _currentFiles};
            host.ShelvePicture();
            var nextNode = _currentFiles.First;
            while (nextNode != null
               && string.Compare(nextNode.Value.FileInfo.Name, host.FileInfo.Name, StringComparison.InvariantCultureIgnoreCase)<0
            )
            {
                nextNode = nextNode.Next;
            }
            NodesTuple displayLocation;
            if (nextNode == null)
            {
                _currentFiles.AddLast(host);
                displayLocation = _currentFiles.Count == 1
                    ? new NodesTuple(_currentFiles.First, _currentFiles.First)
                    : new NodesTuple(_currentFiles.Last.Previous, _currentFiles.Last);
            }
            else
            {
                var newNode = _currentFiles.AddBefore(nextNode, host);
                if (shelvedFile.Item2 == Side.Right && newNode.Previous != null)
                    displayLocation = new NodesTuple(newNode.Previous, newNode);
                else displayLocation = new NodesTuple(newNode, newNode.Next);
            }
            _fileIndex = LoadPictures(displayLocation, 0, 0, noRelease: true);
        }
    }
}
