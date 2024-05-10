namespace soundripper
{
    partial class frmDownloadProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloadProgress));
            prgbrDownloadProgress = new ProgressBar();
            fileSystemWatcher1 = new FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // prgbrDownloadProgress
            // 
            prgbrDownloadProgress.Location = new Point(12, 12);
            prgbrDownloadProgress.Name = "prgbrDownloadProgress";
            prgbrDownloadProgress.Size = new Size(397, 23);
            prgbrDownloadProgress.TabIndex = 0;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // frmDownloadProgress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 57);
            Controls.Add(prgbrDownloadProgress);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmDownloadProgress";
            Text = "soundripper Download Progress";
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar prgbrDownloadProgress;
        private FileSystemWatcher fileSystemWatcher1;
    }
}