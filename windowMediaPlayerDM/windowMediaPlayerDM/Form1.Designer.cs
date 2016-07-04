﻿namespace windowMediaPlayerDM
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Media_Player = new AxWMPLib.AxWindowsMediaPlayer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openMeidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setDMsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Media_DM_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Media_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.Danmoku_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.test_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.debugtab = new System.Windows.Forms.ToolStripStatusLabel();
            this.debugtab2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Video_comment = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Media_Player)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Media_Player
            // 
            this.Media_Player.AllowDrop = true;
            this.Media_Player.Enabled = true;
            this.Media_Player.Location = new System.Drawing.Point(0, 29);
            this.Media_Player.Name = "Media_Player";
            this.Media_Player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Media_Player.OcxState")));
            this.Media_Player.Size = new System.Drawing.Size(996, 527);
            this.Media_Player.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMeidaToolStripMenuItem,
            this.Media_DM_menu,
            this.Settings_menu,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openMeidaToolStripMenuItem
            // 
            this.openMeidaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDMToolStripMenuItem,
            this.setDMToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.openMeidaToolStripMenuItem.Name = "openMeidaToolStripMenuItem";
            this.openMeidaToolStripMenuItem.Size = new System.Drawing.Size(50, 22);
            this.openMeidaToolStripMenuItem.Text = "Open";
            this.openMeidaToolStripMenuItem.Click += new System.EventHandler(this.openMeidaToolStripMenuItem_Click);
            // 
            // setDMToolStripMenuItem
            // 
            this.setDMToolStripMenuItem.Name = "setDMToolStripMenuItem";
            this.setDMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setDMToolStripMenuItem.Text = "Set Meida";
            this.setDMToolStripMenuItem.Click += new System.EventHandler(this.setDMToolStripMenuItem_Click);
            // 
            // setDMToolStripMenuItem1
            // 
            this.setDMToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDMsToolStripMenuItem});
            this.setDMToolStripMenuItem1.Name = "setDMToolStripMenuItem1";
            this.setDMToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.setDMToolStripMenuItem1.Text = "Set DM";
            this.setDMToolStripMenuItem1.Click += new System.EventHandler(this.setDMToolStripMenuItem1_Click);
            // 
            // setDMsToolStripMenuItem
            // 
            this.setDMsToolStripMenuItem.Name = "setDMsToolStripMenuItem";
            this.setDMsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setDMsToolStripMenuItem.Text = "Set DMs";
            this.setDMsToolStripMenuItem.Click += new System.EventHandler(this.setDMsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Media_DM_menu
            // 
            this.Media_DM_menu.Name = "Media_DM_menu";
            this.Media_DM_menu.Size = new System.Drawing.Size(104, 22);
            this.Media_DM_menu.Text = "Media/DM List";
            this.Media_DM_menu.Click += new System.EventHandler(this.Media_DM_menu_Click);
            // 
            // Settings_menu
            // 
            this.Settings_menu.Name = "Settings_menu";
            this.Settings_menu.Size = new System.Drawing.Size(68, 22);
            this.Settings_menu.Text = "Settings";
            this.Settings_menu.Click += new System.EventHandler(this.Settings_menu_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Media_status,
            this.Danmoku_status,
            this.test_label,
            this.debugtab,
            this.debugtab2,
            this.Video_comment});
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(996, 23);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Media_status
            // 
            this.Media_status.Name = "Media_status";
            this.Media_status.Size = new System.Drawing.Size(62, 18);
            this.Media_status.Text = "No Media";
            // 
            // Danmoku_status
            // 
            this.Danmoku_status.Name = "Danmoku_status";
            this.Danmoku_status.Size = new System.Drawing.Size(47, 18);
            this.Danmoku_status.Text = "No DM";
            // 
            // test_label
            // 
            this.test_label.Name = "test_label";
            this.test_label.Size = new System.Drawing.Size(0, 18);
            // 
            // debugtab
            // 
            this.debugtab.Name = "debugtab";
            this.debugtab.Size = new System.Drawing.Size(0, 18);
            // 
            // debugtab2
            // 
            this.debugtab2.Name = "debugtab2";
            this.debugtab2.Size = new System.Drawing.Size(0, 18);
            // 
            // Video_comment
            // 
            this.Video_comment.Name = "Video_comment";
            this.Video_comment.Size = new System.Drawing.Size(0, 18);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(996, 582);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Media_Player);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DM Player";
            ((System.ComponentModel.ISupportInitialize)(this.Media_Player)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer Media_Player;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openMeidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDMToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Media_status;
        private System.Windows.Forms.ToolStripStatusLabel Danmoku_status;
        private System.Windows.Forms.ToolStripStatusLabel test_label;
        private System.Windows.Forms.ToolStripStatusLabel debugtab;
        private System.Windows.Forms.ToolStripStatusLabel debugtab2;
        private System.Windows.Forms.ToolStripMenuItem Media_DM_menu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem setDMsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel Video_comment;
        private System.Windows.Forms.ToolStripMenuItem Settings_menu;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

