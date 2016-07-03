using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace windowMediaPlayerDM
{
    public partial class Settings : Form
    {
        int comment_speed;
        bool auto_match, comment_switch;
        int time_offset;
        int auto_offset;
        int endpoint;

        

        public Settings()
        {
            InitializeComponent();


            
            
        }

        public void formSetup(){
        
        
        
        
        }
        public Button getconfirm {

            set { this.confirm = value; }

            get { return this.confirm; }
        
        }
        public Button getcancel
        {

            set { this.Cancel = value; }

            get { return this.Cancel; }

        }
        public Button getapply
        {

            set { this.apply_button = value; }

            get { return this.apply_button; }

        }
        public Button getdefault
        {

            set { this.default_button = value; }

            get { return this.default_button; }

        }
        public ComboBox select_commentswitch {

            set { this.comment_switch_box = value; }
            get { return this.comment_switch_box; }
        
        
        }

        public Button getAudio_up {

            set { this.audio_up = value; }

            get { return this.audio_up; }
        
        }
        public Button getAudio_down
        {

            set { this.audio_down = value; }

            get { return this.audio_down; }

        }
        public Label getCurrentAudio {

            set { this.current_audio = value; }

            get { return this.current_audio; }
        
        
        }
        public Label getTotalAudio
        {

            set { this.total_audio = value; }

            get { return this.total_audio; }


        }
        public Label getAudioInfo
        {

            set { this.audio_info = value; }

            get { return this.audio_info; }


        }
        public CheckBox auto_mode_check {

            set { this.offset_check = value; }
            get { return this.offset_check; }
        
        
        }

        public TextBox timeoffset {

            set { //this.time_offset =Int32.Parse(value.Text);
            this.offset_box = value;
            }
            get { return this.offset_box; }
        }
        public TextBox Cspeed {

            set { this.comment_speed_box = value; }

            get { return this.comment_speed_box; }
        
        
        }
        public TextBox Cend
        {

            set { this.comment_end_box = value; }

            get { return this.comment_end_box; }


        }



        /// /////////////////

        private void confirm_Click(object sender, EventArgs e)
        {
            // save all changes here


       
        }

        private void Cancel_Click(object sender, EventArgs e)
        {


    
        }

        private void apply_button_Click(object sender, EventArgs e)
        {

        }

        private void default_button_Click(object sender, EventArgs e)
        {
            //reload data from fm1?  apply button click in fm1 for reloading data?
        }
    }
}
/*
            speed_control = 1;

            time_offset = 0;

            move_distance = 3;

            timer1.Interval = 1;

            commentdestroy = -200;

            sommentswitch = true;

            threading_mode = false;

            vpos = -1;

            userColor = Color.DarkGray;

            replacetimer1_interval = 5;

            auto_TimeMatch = true;

            offset_auto = -1400;
*/