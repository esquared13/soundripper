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
            prgbDownloadProgress = new ProgressBar();
            SuspendLayout();
            // 
            // prgbDownloadProgress
            // 
            prgbDownloadProgress.Location = new Point(0, 0);
            prgbDownloadProgress.Name = "prgbDownloadProgress";
            prgbDownloadProgress.Size = new Size(397, 23);
            prgbDownloadProgress.TabIndex = 0;
            // 
            // frmDownloadProgress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 450);
            Controls.Add(prgbDownloadProgress);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmDownloadProgress";
            Text = "soundripper Download Progress";
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar prgbDownloadProgress;
    }
}