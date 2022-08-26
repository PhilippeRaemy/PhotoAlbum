﻿using PictureHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PicturesSorter
{
    using System.Drawing;
    using System.Threading;

    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 250;
        const int PICTURE_HEIGHT = 250;
        const int MAX_TASKS = 8;
        readonly List<PictureSignature> _signatures = new List<PictureSignature>();
        double _similarityFactor = .95;
        bool _formIsAlive = true;

        DirectoryInfo _directory;

        public SimilarPicturesForm() => InitializeComponent();

        readonly HashSet<PictureSignature> _distinctSignatures = new HashSet<PictureSignature>();

        readonly Dictionary<PictureSignature, List<PictureSignature>> _similarSignatures =
            new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        void IncrementProgress()
        {
            if (ProgressBar.InvokeRequired)
                ProgressBar.Invoke(new Action(IncrementProgress));
            else
                ProgressBar.Value += 1;
        }

        void SetLabelFileText(string text)
        {
            if (labelFile.InvokeRequired)
                labelFile.Invoke(new Action(() => SetLabelFileText(text)));
            else
                labelFile.Text = text;
        }

        void ReceiveSignature(PictureSignature signature)
        {
            var actions = new List<Action>();
            Console.WriteLine($"Got {signature.FileInfo.FullName}");
            IncrementProgress();
            var handled = false;
            lock (_similarSignatures)
            {
                foreach (var s in _similarSignatures.Keys
                             .Where(s => s.GetSimilarityWith(signature) > _similarityFactor))
                {
                    Console.WriteLine($"    Found similar with {s.FileInfo.Name}. Pre-existing.");
                    _similarSignatures[s].Add(signature);
                    var maxLength = _similarSignatures[s].Max(ss => ss.FileInfo.Length);
                    actions.AddRange(
                        from ss in _similarSignatures[s]
                        select (Action)(() => ss.Selected = ss.FileInfo.Length < maxLength));

                    actions.Add(() =>
                    {
                        signature.PictureBox = CreatePictureBox(
                            signature.SetLocation(_similarSignatures[s].Count * PICTURE_WIDTH, s.Location.Y), 
                            PICTURE_WIDTH, PICTURE_HEIGHT, signature.FileInfo.Length < maxLength);
                    });
                    handled = true;
                }

                if(!handled)
                    foreach (var s in _distinctSignatures.ToArray())
                    {
                        if (s.GetSimilarityWith(signature) > _similarityFactor)
                        {
                            Console.WriteLine($"    Found similar with {s.FileInfo.Name}. New.");
                            var top = _similarSignatures.Count * PICTURE_HEIGHT;
                            actions.Add(() =>
                            {
                                s.PictureBox = CreatePictureBox(s.SetLocation(0, top), PICTURE_WIDTH, PICTURE_HEIGHT, 
                                    s.FileInfo.Length < signature.FileInfo.Length);
                            });

                            actions.Add(() =>
                            {
                                signature.PictureBox = CreatePictureBox(signature.SetLocation(PICTURE_WIDTH, top), PICTURE_WIDTH,
                                    PICTURE_HEIGHT, signature.FileInfo.Length < s.FileInfo.Length);
                            });
                            _similarSignatures.Add(s, new[] { signature }.ToList());
                            Console.WriteLine($"    {s.FileInfo.Name} removed from distincts.");
                            _distinctSignatures.Remove(s);
                            handled = true;
                        }
                    }
                if(!handled)
                {
                    Console.WriteLine($"    {signature.FileInfo.Name} added to distincts.");
                    _distinctSignatures.Add(signature);
                }
            }

            foreach (var action in actions) action.Invoke();
        }

        SelectablePictureBox CreatePictureBox(PictureSignature signature, int w, int h, bool selected, SelectablePictureBox ppb = null)
        {
            var pb = ppb ?? new SelectablePictureBox(signature);
            var fileInfo = signature.FileInfo;
            if (PanelMain.InvokeRequired)
            {
                Console.WriteLine($"   Invoke creating picture at ({signature.Location}) for {fileInfo.Name}.");
                PanelMain.Invoke(new Action(() => CreatePictureBox(signature, w, h, selected, pb)));
                return pb;
            }

            Console.WriteLine($"   Creating picture at ({signature.Location}) for {fileInfo.Name}.");
            pb = new SelectablePictureBox(signature);

            PanelMain.Controls.Add(pb);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) pb).BeginInit();

            pb.Location = signature.Location;
            pb.Name = "pictureBox_" + fileInfo.Name;
            pb.Size = new Size(w, h);
            pb.TabIndex = int.MaxValue;
            pb.TabStop = true;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = PictureHelper.ReadImageFromFileInfo(fileInfo);
            pb.BorderStyle = selected ? BorderStyle.Fixed3D : BorderStyle.None;

            pb.Tag = fileInfo;
            pb.MouseHover += Pb_MouseHover(
                $"{fileInfo.FullName}({fileInfo.Length / 1024.0 / 1024.0:f2}Mb)[{pb.Image.Width}x{pb.Image.Height}]");

            pb.Click += PictureBox_Click;
            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) pb).EndInit();
            return pb;
        }

        void PictureBox_Click(object sender, EventArgs e) => (sender as SelectablePictureBox)?.ToggleSelection();

        EventHandler Pb_MouseHover(string text) => (sender, args) => SetLabelFileText(text);

        void SimilarPicturesForm_Load(object sender, EventArgs e) { }


        public void LoadPictures(DirectoryInfo directory)
        {
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                _similarityFactor = (double) similarityFactor.Value / 100;
                _directory = directory;
                if (directory is null)
                {
                    Close();
                    return;
                }

                var files = new Queue<FileInfo>(
                    directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories).ToArray()
                        .OrderByDescending(fi => fi.Length)); // better (and heavier) images first

                ProgressBar.Maximum = files.Count;

                void LoadPictureThread()
                {
                    while (_formIsAlive)
                    {
                        FileInfo file;
                        lock (files)
                        {
                            if (files.Count == 0) return; // we're done!
                            file = files.Dequeue();
                        }

                        var signature = new PictureSignature(file, 16, 4, false);
                        Console.WriteLine($"LoadPictureThread: {file.Name}");
                        signature.GetSignature(ReceiveSignature);
                        lock (_signatures) _signatures.Add(signature);
                    }
                }

                var threads = Enumerable.Range(0, MAX_TASKS)
                    .Select(_ => new Thread(LoadPictureThread));

                foreach (var thread in threads) thread.Start();

            }
        }

        void buttonGo_Click(object sender, EventArgs e)
        {
            _distinctSignatures.Clear();
            _similarSignatures.Clear();
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                foreach (var control in PanelMain.Controls.Cast<Control>().ToArray())
                    if (control is PictureBox)
                        PanelMain.Controls.Remove(control);
                _similarityFactor = (double)similarityFactor.Value / 100;
                ProgressBar.Value = 0;
                lock (_signatures)
                    foreach (var signature in _signatures)
                    {
                        if (!_formIsAlive) break;
                        if (signature.PictureBox != null)
                        {
                            signature.PictureBox.Dispose();
                            signature.PictureBox = null;
                        }
                        signature.FileInfo.Refresh();
                        if (signature.FileInfo.Exists) ReceiveSignature(signature);
                    }
            }
        }

        void SimilarPicturesForm_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine($"{(e.Control ? "<ctrl>" : string.Empty)}{(e.Alt ? "<alt>" : string.Empty)}{(e.Shift ? "<shift>" : string.Empty)}{e.KeyCode}");
            IEnumerable<SelectablePictureBox> GetPictureBoxes(Control control)
            {
                if (control is SelectablePictureBox pb) yield return pb;
                foreach (Control child in control.Controls)
                foreach (var sb in GetPictureBoxes(child))
                    yield return sb;
            }

            if (e.KeyCode == Keys.Delete && !e.Alt && !e.Control && !e.Shift && !e.Handled)
            {
                var count = 0;
                foreach (var pb in GetPictureBoxes((Control)sender))
                {
                    if (!pb.Selected) continue;
                    pb.Image = null;
                    pb.FileInfo.Delete();
                    pb.Parent.Controls.Remove(pb);

                    count++;
                }
                MessageBox.Show($"Delete key pressed. {count} pictures deleted.");
            }

            e.Handled = true;
            e.SuppressKeyPress=true;
        }

        void SimilarPicturesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formIsAlive = false;
        }
    }
}
