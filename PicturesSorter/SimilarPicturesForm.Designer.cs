namespace PicturesSorter
{
    partial class SimilarPicturesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.labelFile = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.similarityFactor = new System.Windows.Forms.NumericUpDown();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.PanelHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.similarityFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.AutoScroll = true;
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 31);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(800, 404);
            this.PanelMain.TabIndex = 3;
            // 
            // PanelHeader
            // 
            this.PanelHeader.Controls.Add(this.labelFile);
            this.PanelHeader.Controls.Add(this.buttonGo);
            this.PanelHeader.Controls.Add(this.panel1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(800, 31);
            this.PanelHeader.TabIndex = 1;
            // 
            // labelFile
            // 
            this.labelFile.AutoEllipsis = true;
            this.labelFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFile.Location = new System.Drawing.Point(161, 0);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(564, 31);
            this.labelFile.TabIndex = 3;
            this.labelFile.Text = "labelFile";
            this.labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonGo
            // 
            this.buttonGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonGo.Location = new System.Drawing.Point(725, 0);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 31);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.similarityFactor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 31);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Similarity:";
            // 
            // similarityFactor
            // 
            this.similarityFactor.Location = new System.Drawing.Point(62, 3);
            this.similarityFactor.Minimum = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.similarityFactor.Name = "similarityFactor";
            this.similarityFactor.Size = new System.Drawing.Size(88, 20);
            this.similarityFactor.TabIndex = 3;
            this.similarityFactor.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // ProgressBar
            // 
            this.ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressBar.Location = new System.Drawing.Point(0, 435);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(800, 15);
            this.ProgressBar.TabIndex = 2;
            // 
            // SimilarPicturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.PanelHeader);
            this.Name = "SimilarPicturesForm";
            this.Text = "Similar pictures";
            this.Load += new System.EventHandler(this.SimilarPicturesForm_Load);
            this.PanelHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.similarityFactor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown similarityFactor;
    }
}