namespace potato_launcher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            header_latest = new Label();
            header_current = new Label();
            label_latest = new Label();
            label_current = new Label();
            label_process = new Label();
            label_info = new Label();
            label_github = new LinkLabel();
            progress_bar = new ProgressBar();
            SuspendLayout();
            // 
            // header_latest
            // 
            header_latest.AutoSize = true;
            header_latest.ForeColor = Color.FromArgb(242, 243, 245);
            header_latest.Location = new Point(12, 9);
            header_latest.Name = "header_latest";
            header_latest.Size = new Size(79, 15);
            header_latest.TabIndex = 0;
            header_latest.Text = "Latest Version";
            // 
            // header_current
            // 
            header_current.AutoSize = true;
            header_current.ForeColor = Color.FromArgb(242, 243, 245);
            header_current.Location = new Point(381, 9);
            header_current.Name = "header_current";
            header_current.Size = new Size(88, 15);
            header_current.TabIndex = 1;
            header_current.Text = "Current Version";
            // 
            // label_latest
            // 
            label_latest.AutoSize = true;
            label_latest.ForeColor = Color.FromArgb(242, 243, 245);
            label_latest.Location = new Point(11, 29);
            label_latest.Name = "label_latest";
            label_latest.Size = new Size(78, 15);
            label_latest.TabIndex = 2;
            label_latest.Text = "latest_version";
            label_latest.TextAlign = ContentAlignment.MiddleLeft;
            label_latest.Visible = false;
            // 
            // label_current
            // 
            label_current.ForeColor = Color.FromArgb(242, 243, 245);
            label_current.Location = new Point(339, 29);
            label_current.Name = "label_current";
            label_current.Size = new Size(130, 15);
            label_current.TabIndex = 3;
            label_current.Text = "current_version";
            label_current.TextAlign = ContentAlignment.MiddleRight;
            label_current.Visible = false;
            // 
            // label_process
            // 
            label_process.AutoSize = true;
            label_process.ForeColor = Color.Gray;
            label_process.Location = new Point(12, 104);
            label_process.Name = "label_process";
            label_process.Size = new Size(77, 15);
            label_process.TabIndex = 4;
            label_process.Text = "label_process";
            label_process.TextAlign = ContentAlignment.MiddleLeft;
            label_process.Visible = false;
            // 
            // label_info
            // 
            label_info.ForeColor = Color.Gray;
            label_info.Location = new Point(252, 104);
            label_info.Name = "label_info";
            label_info.Size = new Size(213, 15);
            label_info.TabIndex = 5;
            label_info.Text = "{0} / {1} MB @ {2} MB/s";
            label_info.TextAlign = ContentAlignment.MiddleRight;
            label_info.Visible = false;
            // 
            // label_github
            // 
            label_github.ActiveLinkColor = Color.LimeGreen;
            label_github.AutoSize = true;
            label_github.LinkColor = Color.LimeGreen;
            label_github.Location = new Point(383, 131);
            label_github.Name = "label_github";
            label_github.Size = new Size(82, 15);
            label_github.TabIndex = 6;
            label_github.TabStop = true;
            label_github.Text = "Github Source";
            label_github.LinkClicked += label_github_LinkClicked;
            // 
            // progress_bar
            // 
            progress_bar.ForeColor = SystemColors.GrayText;
            progress_bar.Location = new Point(12, 65);
            progress_bar.MarqueeAnimationSpeed = 1;
            progress_bar.Name = "progress_bar";
            progress_bar.Size = new Size(453, 31);
            progress_bar.Style = ProgressBarStyle.Marquee;
            progress_bar.TabIndex = 7;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 38, 41);
            ClientSize = new Size(473, 151);
            Controls.Add(progress_bar);
            Controls.Add(label_github);
            Controls.Add(label_info);
            Controls.Add(label_process);
            Controls.Add(label_current);
            Controls.Add(label_latest);
            Controls.Add(header_current);
            Controls.Add(header_latest);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Potato Launcher";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label header_latest;
        private Label header_current;
        private Label label_latest;
        private Label label_current;
        private Label label_process;
        private Label label_info;
        private LinkLabel label_github;
        private ProgressBar progress_bar;
    }
}
