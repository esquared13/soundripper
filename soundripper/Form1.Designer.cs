﻿namespace soundripper
{
    partial class frmsoundripper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmsoundripper));
            lblURL = new Label();
            txtVideoLink = new TextBox();
            btnConvert = new Button();
            chkbxKeepVideo = new CheckBox();
            SuspendLayout();
            // 
            // lblURL
            // 
            lblURL.AutoSize = true;
            lblURL.Location = new Point(12, 9);
            lblURL.Name = "lblURL";
            lblURL.Size = new Size(62, 15);
            lblURL.TabIndex = 0;
            lblURL.Text = "Video Link";
            // 
            // txtVideoLink
            // 
            txtVideoLink.Location = new Point(12, 27);
            txtVideoLink.Name = "txtVideoLink";
            txtVideoLink.Size = new Size(278, 23);
            txtVideoLink.TabIndex = 1;
            // 
            // btnConvert
            // 
            btnConvert.Location = new Point(215, 56);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(75, 23);
            btnConvert.TabIndex = 2;
            btnConvert.Text = "&Convert!";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // chkbxKeepVideo
            // 
            chkbxKeepVideo.AutoSize = true;
            chkbxKeepVideo.Location = new Point(14, 56);
            chkbxKeepVideo.Name = "chkbxKeepVideo";
            chkbxKeepVideo.Size = new Size(153, 19);
            chkbxKeepVideo.TabIndex = 3;
            chkbxKeepVideo.Text = "Keep downloaded video";
            chkbxKeepVideo.UseVisualStyleBackColor = true;
            chkbxKeepVideo.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // frmsoundripper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(302, 100);
            Controls.Add(chkbxKeepVideo);
            Controls.Add(btnConvert);
            Controls.Add(txtVideoLink);
            Controls.Add(lblURL);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmsoundripper";
            Text = "soundripper";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblURL;
        private TextBox txtVideoLink;
        private Button btnConvert;
        private CheckBox chkbxKeepVideo;
    }
}
