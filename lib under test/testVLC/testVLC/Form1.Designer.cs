namespace testVLC
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Playb = new System.Windows.Forms.Button();
            this.Pauseb = new System.Windows.Forms.Button();
            this.Stopb = new System.Windows.Forms.Button();
            this.Openb = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.Label();
            this.down = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.Button();
            this.Track = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.soundbar = new System.Windows.Forms.ProgressBar();
            this.optionbox = new System.Windows.Forms.ComboBox();
            this.trackbar = new System.Windows.Forms.TrackBar();
            this.backward = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(837, 438);
            this.panel1.TabIndex = 0;
            // 
            // Playb
            // 
            this.Playb.Location = new System.Drawing.Point(260, 465);
            this.Playb.Name = "Playb";
            this.Playb.Size = new System.Drawing.Size(75, 23);
            this.Playb.TabIndex = 0;
            this.Playb.Text = "Play";
            this.Playb.UseVisualStyleBackColor = true;
            this.Playb.Click += new System.EventHandler(this.Playb_Click);
            // 
            // Pauseb
            // 
            this.Pauseb.Location = new System.Drawing.Point(341, 465);
            this.Pauseb.Name = "Pauseb";
            this.Pauseb.Size = new System.Drawing.Size(75, 23);
            this.Pauseb.TabIndex = 0;
            this.Pauseb.Text = "Pause";
            this.Pauseb.UseVisualStyleBackColor = true;
            this.Pauseb.Click += new System.EventHandler(this.Pauseb_Click);
            // 
            // Stopb
            // 
            this.Stopb.Location = new System.Drawing.Point(422, 465);
            this.Stopb.Name = "Stopb";
            this.Stopb.Size = new System.Drawing.Size(75, 23);
            this.Stopb.TabIndex = 0;
            this.Stopb.Text = "Stop";
            this.Stopb.UseVisualStyleBackColor = true;
            this.Stopb.Click += new System.EventHandler(this.Stopb_Click);
            // 
            // Openb
            // 
            this.Openb.Location = new System.Drawing.Point(740, 465);
            this.Openb.Name = "Openb";
            this.Openb.Size = new System.Drawing.Size(75, 23);
            this.Openb.TabIndex = 1;
            this.Openb.Text = "Open";
            this.Openb.UseVisualStyleBackColor = true;
            this.Openb.Click += new System.EventHandler(this.Openb_Click);
            // 
            // console
            // 
            this.console.AutoSize = true;
            this.console.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.console.Location = new System.Drawing.Point(533, 471);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(36, 20);
            this.console.TabIndex = 2;
            this.console.Text = "test";
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(101, 465);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(27, 23);
            this.down.TabIndex = 3;
            this.down.Text = "↓";
            this.down.UseVisualStyleBackColor = true;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(50, 465);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(27, 23);
            this.up.TabIndex = 3;
            this.up.Text = "↑";
            this.up.UseVisualStyleBackColor = true;
            this.up.Click += new System.EventHandler(this.up_Click);
            // 
            // Track
            // 
            this.Track.AutoSize = true;
            this.Track.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Track.Location = new System.Drawing.Point(134, 468);
            this.Track.Name = "Track";
            this.Track.Size = new System.Drawing.Size(36, 20);
            this.Track.TabIndex = 2;
            this.Track.Text = "test";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(-392, 462);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // soundbar
            // 
            this.soundbar.Location = new System.Drawing.Point(740, 494);
            this.soundbar.Name = "soundbar";
            this.soundbar.Size = new System.Drawing.Size(141, 23);
            this.soundbar.TabIndex = 4;
            // 
            // optionbox
            // 
            this.optionbox.FormattingEnabled = true;
            this.optionbox.Items.AddRange(new object[] {
            "Time",
            "Enable",
            "Contrast",
            "Brightness",
            "Hue",
            "Saturation",
            "Gamma"});
            this.optionbox.Location = new System.Drawing.Point(50, 495);
            this.optionbox.Name = "optionbox";
            this.optionbox.Size = new System.Drawing.Size(121, 21);
            this.optionbox.TabIndex = 5;
            // 
            // trackbar
            // 
            this.trackbar.AutoSize = false;
            this.trackbar.Location = new System.Drawing.Point(177, 494);
            this.trackbar.Name = "trackbar";
            this.trackbar.Size = new System.Drawing.Size(354, 32);
            this.trackbar.TabIndex = 6;
            this.trackbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // backward
            // 
            this.backward.Location = new System.Drawing.Point(537, 496);
            this.backward.Name = "backward";
            this.backward.Size = new System.Drawing.Size(75, 23);
            this.backward.TabIndex = 7;
            this.backward.Text = "backward";
            this.backward.UseVisualStyleBackColor = true;
            this.backward.Click += new System.EventHandler(this.backward_Click);
            // 
            // forward
            // 
            this.forward.Location = new System.Drawing.Point(618, 496);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(75, 23);
            this.forward.TabIndex = 7;
            this.forward.Text = "forward";
            this.forward.UseVisualStyleBackColor = true;
            this.forward.Click += new System.EventHandler(this.forward_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 531);
            this.Controls.Add(this.forward);
            this.Controls.Add(this.backward);
            this.Controls.Add(this.trackbar);
            this.Controls.Add(this.optionbox);
            this.Controls.Add(this.soundbar);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.up);
            this.Controls.Add(this.Track);
            this.Controls.Add(this.down);
            this.Controls.Add(this.console);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Playb);
            this.Controls.Add(this.Pauseb);
            this.Controls.Add(this.Stopb);
            this.Controls.Add(this.Openb);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Openb;
        private System.Windows.Forms.Button Stopb;
        private System.Windows.Forms.Button Pauseb;
        private System.Windows.Forms.Button Playb;
        private System.Windows.Forms.Label console;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Label Track;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar soundbar;
        private System.Windows.Forms.ComboBox optionbox;
        private System.Windows.Forms.TrackBar trackbar;
        private System.Windows.Forms.Button backward;
        private System.Windows.Forms.Button forward;
    }
}

