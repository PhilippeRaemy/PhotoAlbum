﻿

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
        class Nodes : Tuple<LinkedListNode<ImageHost>, LinkedListNode<ImageHost>> {
            public Nodes(LinkedListNode<ImageHost> item1, LinkedListNode<ImageHost> item2) : base(item1, item2) { }
        }

        DirectoryInfo _currentDirectory;
        LinkedList<ImageHost> _currentFiles;
        Nodes _fileIndex;

        public PictureSorterForm()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
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

        void PictureSorterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            (new PersistedUserPreferences {FolderImportStart = _currentDirectory.FullName}).Save();
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
                new DirectoryInfo(folderBrowserDialog.SelectedPath));
            userPrefs.Save();
        }

        void OpenNextFolder(DirectoryInfo currentDirectory, FolderDirection folderDirection)
        {
            var folder = GetNextFolder(currentDirectory, folderDirection);
            if (folder != null)
            {
                var userPrefs = new PersistedUserPreferences();
                var fileNameHandler = new FileNameHandler(userPrefs);
                OpenFolderImpl(n => fileNameHandler.FileMatch(n, includeSmalls: false), folder);
                userPrefs.Save();
            }
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

        public static DirectoryInfo GetNextFolder(DirectoryInfo currentDirectory, bool ignoreSubFolders = false)
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
            foreach (
                var subDirectory in
                currentDirectory.Parent.EnumerateDirectories().OrderBy(d => d.Name).Where(d => comparer(d.Name, directory.Name)))
            {
                return subDirectory;
            }
            return GetNextFolder(currentDirectory.Parent, ignoreSubFolders=true);
        }

        public static DirectoryInfo GetPreviousFolder(DirectoryInfo currentDirectory, bool diveSubFolders = false)
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
                    return GetPreviousFolder(subDirectory, diveSubFolders = true);
                }
                return currentDirectory;
            }

            foreach (
                var subDirectory in
                currentDirectory.Parent.EnumerateDirectories().OrderByDescending(d => d.Name).Where(d => comparer(d.Name, directory.Name)))
            {
                return GetPreviousFolder(subDirectory, diveSubFolders = true);
            }

            return currentDirectory.Parent;
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
            _currentFiles.ForEach(ih=>ih.Parent= _currentFiles);
            switch (_currentFiles.Count)
            {
                case 0:
                    // MessageBox.Show($"There are no pictures to sort in folder {selectedPath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (rc.Item1 == null || rc.Item2 == null) return null;
            Trace.WriteLine($"LoadPictures({rc.Item1.Value.FileInfo.Name}, {rc.Item2.Value.FileInfo.Name}, {step1}, {step2}, {noRelease})");
            rc.Item1?.Value?.Render(pictureBox1, label1);
            rc.Item2?.Value?.Render(pictureBox2, label2);
            if (!noRelease)
            {
                idx?.Item1?.Value?.Release();
                idx?.Item2?.Value?.Release();
            }
            Trace.WriteLine($"LoadPictures returns({rc.Item1.Value.FileInfo.Name}, {rc.Item2.Value.FileInfo.Name})");
            return rc;
        }

        Nodes SelectIndexes(Nodes idx, int step1, int step2)
            => new Nodes(idx?.Item1.SafeStep(step1), idx?.Item2.SafeStep(step2));

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
            _fileIndex.Item1.Value.Rotate(RotateFlipType.Rotate90FlipNone);
            _fileIndex.Item1.Value.Render(pictureBox1, label1, force: true);
        }

        void RotateLeftAnti_Click(object sender, EventArgs e)
        {
            _fileIndex.Item1.Value.Rotate(RotateFlipType.Rotate270FlipNone);
            _fileIndex.Item1.Value.Render(pictureBox1, label1, force: true);
        }

        void RotateRightClock_Click(object sender, EventArgs e)
        {
            _fileIndex.Item2.Value.Rotate(RotateFlipType.Rotate90FlipNone);
            _fileIndex.Item2.Value.Render(pictureBox2, label2, force: true);
        }

        void RotateRightAnti_Click(object sender, EventArgs e)
        {
            _fileIndex.Item2.Value.Rotate(RotateFlipType.Rotate270FlipNone);
            _fileIndex.Item2.Value.Render(pictureBox2, label2, force: true);
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

    }
}
