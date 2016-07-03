namespace windowMediaPlayerDM
{
    partial class Settings
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
            this.offset_label1 = new System.Windows.Forms.Label();
            this.offset_box = new System.Windows.Forms.TextBox();
            this.offset_check = new System.Windows.Forms.CheckBox();
            this.confirm = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.cdistance = new System.Windows.Forms.Label();
            this.comment_speed_box = new System.Windows.Forms.TextBox();
            this.comment_switch_label = new System.Windows.Forms.Label();
            this.comment_switch_box = new System.Windows.Forms.ComboBox();
            this.comment_end_label = new System.Windows.Forms.Label();
            this.comment_end_box = new System.Windows.Forms.TextBox();
            this.apply_button = new System.Windows.Forms.Button();
            this.default_button = new System.Windows.Forms.Button();
            this.audio_label = new System.Windows.Forms.Label();
            this.audio_up = new System.Windows.Forms.Button();
            this.audio_down = new System.Windows.Forms.Button();
            this.current_audio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.total_audio = new System.Windows.Forms.Label();
            this.audio_info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // offset_label1
            // 
            this.offset_label1.AutoSize = true;
            this.offset_label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.offset_label1.Location = new System.Drawing.Point(74, 76);
            this.offset_label1.Name = "offset_label1";
            this.offset_label1.Size = new System.Drawing.Size(81, 17);
            this.offset_label1.TabIndex = 0;
            this.offset_label1.Text = "Time Offset";
            // 
            // offset_box
            // 
            this.offset_box.Location = new System.Drawing.Point(161, 75);
            this.offset_box.Name = "offset_box";
            this.offset_box.Size = new System.Drawing.Size(100, 20);
            this.offset_box.TabIndex = 1;
            // 
            // offset_check
            // 
            this.offset_check.AutoSize = true;
            this.offset_check.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.offset_check.Location = new System.Drawing.Point(12, 76);
            this.offset_check.Name = "offset_check";
            this.offset_check.Size = new System.Drawing.Size(56, 21);
            this.offset_check.TabIndex = 2;
            this.offset_check.Text = "Auto";
            this.offset_check.UseVisualStyleBackColor = true;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(16, 478);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(52, 23);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "Confirm";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(230, 478);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(52, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // cdistance
            // 
            this.cdistance.AutoSize = true;
            this.cdistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cdistance.Location = new System.Drawing.Point(9, 129);
            this.cdistance.Name = "cdistance";
            this.cdistance.Size = new System.Drawing.Size(112, 17);
            this.cdistance.TabIndex = 5;
            this.cdistance.Text = "Comment Speed";
            // 
            // comment_speed_box
            // 
            this.comment_speed_box.Location = new System.Drawing.Point(161, 129);
            this.comment_speed_box.Name = "comment_speed_box";
            this.comment_speed_box.Size = new System.Drawing.Size(100, 20);
            this.comment_speed_box.TabIndex = 6;
            // 
            // comment_switch_label
            // 
            this.comment_switch_label.AutoSize = true;
            this.comment_switch_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comment_switch_label.Location = new System.Drawing.Point(9, 25);
            this.comment_switch_label.Name = "comment_switch_label";
            this.comment_switch_label.Size = new System.Drawing.Size(123, 17);
            this.comment_switch_label.TabIndex = 7;
            this.comment_switch_label.Text = "Comment ON/OFF";
            // 
            // comment_switch_box
            // 
            this.comment_switch_box.FormattingEnabled = true;
            this.comment_switch_box.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comment_switch_box.Location = new System.Drawing.Point(161, 21);
            this.comment_switch_box.Name = "comment_switch_box";
            this.comment_switch_box.Size = new System.Drawing.Size(121, 21);
            this.comment_switch_box.TabIndex = 8;
            // 
            // comment_end_label
            // 
            this.comment_end_label.AutoSize = true;
            this.comment_end_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comment_end_label.Location = new System.Drawing.Point(9, 181);
            this.comment_end_label.Name = "comment_end_label";
            this.comment_end_label.Size = new System.Drawing.Size(132, 17);
            this.comment_end_label.TabIndex = 9;
            this.comment_end_label.Text = "Comment End Point";
            // 
            // comment_end_box
            // 
            this.comment_end_box.Location = new System.Drawing.Point(161, 178);
            this.comment_end_box.Name = "comment_end_box";
            this.comment_end_box.Size = new System.Drawing.Size(100, 20);
            this.comment_end_box.TabIndex = 6;
            // 
            // apply_button
            // 
            this.apply_button.Location = new System.Drawing.Point(93, 478);
            this.apply_button.Name = "apply_button";
            this.apply_button.Size = new System.Drawing.Size(52, 23);
            this.apply_button.TabIndex = 10;
            this.apply_button.Text = "Apply";
            this.apply_button.UseVisualStyleBackColor = true;
            this.apply_button.Click += new System.EventHandler(this.apply_button_Click);
            // 
            // default_button
            // 
            this.default_button.Location = new System.Drawing.Point(299, 478);
            this.default_button.Name = "default_button";
            this.default_button.Size = new System.Drawing.Size(52, 23);
            this.default_button.TabIndex = 4;
            this.default_button.Text = "Default";
            this.default_button.UseVisualStyleBackColor = true;
            this.default_button.Click += new System.EventHandler(this.default_button_Click);
            // 
            // audio_label
            // 
            this.audio_label.AutoSize = true;
            this.audio_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.audio_label.Location = new System.Drawing.Point(9, 242);
            this.audio_label.Name = "audio_label";
            this.audio_label.Size = new System.Drawing.Size(131, 17);
            this.audio_label.TabIndex = 9;
            this.audio_label.Text = "Current AudioTrack";
            // 
            // audio_up
            // 
            this.audio_up.Location = new System.Drawing.Point(262, 236);
            this.audio_up.Name = "audio_up";
            this.audio_up.Size = new System.Drawing.Size(29, 23);
            this.audio_up.TabIndex = 11;
            this.audio_up.Text = "↑";
            this.audio_up.UseVisualStyleBackColor = true;
            // 
            // audio_down
            // 
            this.audio_down.Location = new System.Drawing.Point(161, 236);
            this.audio_down.Name = "audio_down";
            this.audio_down.Size = new System.Drawing.Size(29, 23);
            this.audio_down.TabIndex = 11;
            this.audio_down.Text = "↓";
            this.audio_down.UseVisualStyleBackColor = true;
            // 
            // current_audio
            // 
            this.current_audio.AutoSize = true;
            this.current_audio.Location = new System.Drawing.Point(196, 242);
            this.current_audio.Name = "current_audio";
            this.current_audio.Size = new System.Drawing.Size(16, 13);
            this.current_audio.TabIndex = 12;
            this.current_audio.Text = "-1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "/";
            // 
            // total_audio
            // 
            this.total_audio.AutoSize = true;
            this.total_audio.Location = new System.Drawing.Point(240, 242);
            this.total_audio.Name = "total_audio";
            this.total_audio.Size = new System.Drawing.Size(16, 13);
            this.total_audio.TabIndex = 12;
            this.total_audio.Text = "-1";
            // 
            // audio_info
            // 
            this.audio_info.AutoSize = true;
            this.audio_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.audio_info.Location = new System.Drawing.Point(143, 285);
            this.audio_info.Name = "audio_info";
            this.audio_info.Size = new System.Drawing.Size(118, 17);
            this.audio_info.TabIndex = 9;
            this.audio_info.Text = "Current AudioInfo";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 513);
            this.Controls.Add(this.total_audio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.current_audio);
            this.Controls.Add(this.audio_down);
            this.Controls.Add(this.audio_up);
            this.Controls.Add(this.apply_button);
            this.Controls.Add(this.audio_info);
            this.Controls.Add(this.audio_label);
            this.Controls.Add(this.comment_end_label);
            this.Controls.Add(this.comment_switch_box);
            this.Controls.Add(this.comment_switch_label);
            this.Controls.Add(this.comment_end_box);
            this.Controls.Add(this.comment_speed_box);
            this.Controls.Add(this.cdistance);
            this.Controls.Add(this.default_button);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.offset_check);
            this.Controls.Add(this.offset_box);
            this.Controls.Add(this.offset_label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label offset_label1;
        private System.Windows.Forms.TextBox offset_box;
        private System.Windows.Forms.CheckBox offset_check;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label cdistance;
        private System.Windows.Forms.TextBox comment_speed_box;
        private System.Windows.Forms.Label comment_switch_label;
        private System.Windows.Forms.ComboBox comment_switch_box;
        private System.Windows.Forms.Label comment_end_label;
        private System.Windows.Forms.TextBox comment_end_box;
        private System.Windows.Forms.Button apply_button;
        private System.Windows.Forms.Button default_button;
        private System.Windows.Forms.Label audio_label;
        private System.Windows.Forms.Button audio_up;
        private System.Windows.Forms.Button audio_down;
        private System.Windows.Forms.Label current_audio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label total_audio;
        private System.Windows.Forms.Label audio_info;
    }
}