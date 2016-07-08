namespace windowMediaPlayerDM
{
    partial class VLC_test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VLC_test));
            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.Open = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.web_button = new System.Windows.Forms.Button();
            this.dir_box = new System.Windows.Forms.TextBox();
            this.Url_List = new System.Windows.Forms.ComboBox();
            this.downloadbar = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.download_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.downloadstatus2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.comment_test = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vlcControl1
            // 
            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Location = new System.Drawing.Point(0, -2);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(953, 396);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 0;
            this.vlcControl1.VlcLibDirectory = ((System.IO.DirectoryInfo)(resources.GetObject("vlcControl1.VlcLibDirectory")));
            this.vlcControl1.VlcMediaplayerOptions = null;
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(13, 420);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(75, 23);
            this.Open.TabIndex = 1;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(133, 420);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 1;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(242, 420);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(337, 420);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(75, 23);
            this.Pause.TabIndex = 1;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(512, 429);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // web_button
            // 
            this.web_button.Location = new System.Drawing.Point(431, 420);
            this.web_button.Name = "web_button";
            this.web_button.Size = new System.Drawing.Size(75, 23);
            this.web_button.TabIndex = 2;
            this.web_button.Text = "get web";
            this.web_button.UseVisualStyleBackColor = true;
            this.web_button.Click += new System.EventHandler(this.web_button_Click);
            // 
            // dir_box
            // 
            this.dir_box.Location = new System.Drawing.Point(590, 422);
            this.dir_box.Name = "dir_box";
            this.dir_box.Size = new System.Drawing.Size(351, 20);
            this.dir_box.TabIndex = 3;
            // 
            // Url_List
            // 
            this.Url_List.FormattingEnabled = true;
            this.Url_List.Location = new System.Drawing.Point(590, 449);
            this.Url_List.Name = "Url_List";
            this.Url_List.Size = new System.Drawing.Size(351, 21);
            this.Url_List.TabIndex = 4;
            // 
            // downloadbar
            // 
            this.downloadbar.Location = new System.Drawing.Point(590, 477);
            this.downloadbar.Name = "downloadbar";
            this.downloadbar.Size = new System.Drawing.Size(351, 23);
            this.downloadbar.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.download_status,
            this.downloadstatus2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 510);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1487, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // download_status
            // 
            this.download_status.Name = "download_status";
            this.download_status.Size = new System.Drawing.Size(0, 17);
            // 
            // downloadstatus2
            // 
            this.downloadstatus2.Name = "downloadstatus2";
            this.downloadstatus2.Size = new System.Drawing.Size(0, 17);
            // 
            // comment_test
            // 
            this.comment_test.Location = new System.Drawing.Point(960, 13);
            this.comment_test.Name = "comment_test";
            this.comment_test.Size = new System.Drawing.Size(515, 494);
            this.comment_test.TabIndex = 7;
            this.comment_test.Text = "";
            // 
            // VLC_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1487, 532);
            this.Controls.Add(this.comment_test);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.downloadbar);
            this.Controls.Add(this.Url_List);
            this.Controls.Add(this.dir_box);
            this.Controls.Add(this.web_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.vlcControl1);
            this.Name = "VLC_test";
            this.Text = "Form5";
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Vlc.DotNet.Forms.VlcControl vlcControl1;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button web_button;
        private System.Windows.Forms.TextBox dir_box;
        private System.Windows.Forms.ComboBox Url_List;
        private System.Windows.Forms.ProgressBar downloadbar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel download_status;
        private System.Windows.Forms.ToolStripStatusLabel downloadstatus2;
        private System.Windows.Forms.RichTextBox comment_test;
    }
}