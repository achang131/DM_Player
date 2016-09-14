using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VLClibDM;
using System.Runtime.InteropServices;
namespace testVLC
{


    public partial class Form1 : Form
    {

        #region importdll
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        #endregion

        VlcInstance Pbase;
        VlcMediaPlayer player;
        bool media_set = false;
        public Form1()
        {
            InitializeComponent();

          string[] args = new string[] {
                "-I", "dummy", "--ignore-config",
                @"--plugin-path=C:\Users\Alan\Documents\GitHub\DM_Player\lib under test\testVLC\testVLC\bin\Debug\plugins",
                "--vout-filter=deinterlace", "--deinterlace-mode=blend"
            };
        
          Pbase = new VlcInstance();

          player = new VlcMediaPlayer(Pbase,panel1.Handle);
  
          player.setDeinterlace("auto");
          player.setKeyInput = true;
          player.setMouseInput = true;

          player.vlc.StateChange += new EventHandler<VlcMediaPlayer.VLCEventManager.StateChangeArgs>(vlc_StateChange);
              player.PlayTimeChange+= new EventHandler<VlcMediaPlayer.PlayTimeArgs>(player_PlayTimeChange);
          this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
          AllocConsole();

          soundbar.MouseClick += new MouseEventHandler(soundbar_MouseClick);
          soundbar.Maximum = 100;
          soundbar.Minimum = 0;
          optionbox.SelectedIndexChanged += new EventHandler(optionbox_SelectedIndexChanged);
          trackbar.ValueChanged += new EventHandler(trackbar_ValueChanged);
          trackbar.MouseDown += new MouseEventHandler(trackbar_MouseDown);
          trackbar.MouseUp += new MouseEventHandler(trackbar_MouseUp);
          player.vlc.EndReach += new EventHandler<VlcMediaPlayer.VLCEventManager.EndReachArgs>(vlc_EndReach);
          player.MediaChanged += new EventHandler<VlcMediaPlayer.MediaChangedArgs>(player_MediaChanged);
        }

        void player_MediaChanged(object sender, VlcMediaPlayer.MediaChangedArgs e)
        {
            //throw new NotImplementedException();
            Console.WriteLine(e.MediaName);
        }
        bool end = false;
        void vlc_EndReach(object sender, VlcMediaPlayer.VLCEventManager.EndReachArgs e)
        {
            //throw new NotImplementedException();
            Console.WriteLine("end reached");
           // changeTrack(0);
           // player.currentVideoTime = 0;
            
           // player.Stop();
            player.currentVideoTime = 0;
        }

        void vlc_StateChange(object sender, VlcMediaPlayer.VLCEventManager.StateChangeArgs e)
        {
            //throw new NotImplementedException();

            changeText(e.currentState);
            setSound(player.Volume);
           
            if (e.currentState == "Playing"&&currentSelectedOption!=0)
            {
                
                setboxIndex(0);
                

            }
     

        }
        delegate void setint(int i);
        void setboxIndex(int i) {
            if (optionbox.InvokeRequired)
            {
                setint c = new setint(setboxIndex);
                optionbox.Invoke(c, new object[] { i});
            }
            else {
                optionbox.SelectedIndex = (int)i;
            }
        }
        void trackbar_MouseUp(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            switch (e.Button) { 
                case MouseButtons.Left:
                    mousedown = false;
                    break;
            
            }
        }

        void soundbar_MouseClick(object sender, MouseEventArgs e)
        {
           // throw new NotImplementedException();
            ProgressBar p = sender as ProgressBar;
            int x = e.X;
            int y = e.Y;
           int max= p.Size.Width;
           int value = (x *100)/ max;
            Console.WriteLine("X: " + x + " Y: " + y);
            player.Volume = value;
            p.Value = value;
            Console.WriteLine("Current Video sound is set to : "+player.Volume);

        }

        void optionbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ComboBox cb = sender as ComboBox;
          
            if (cb.SelectedItem != null)
            {

                currentSelectedOption = cb.SelectedIndex;
                switch (cb.SelectedIndex)
                {




                    case 0:
                        //Time
                        Console.WriteLine("Length: " + player.VideoLength);
                        trackbar.Maximum = (int)player.VideoLength;
                        trackbar.Minimum = 0;

                        break;
                    case 1:
                        trackbar.Maximum = 1;
                        trackbar.Minimum = 0;
                        if (player.EnableAdjustOptions == 1)
                        {
                            trackbar.Value = 1;
                        }
                        else
                        {
                            trackbar.Value = 0;
                        }

                        break;
                    case 2:
                        trackbar.Maximum = 200;
                        trackbar.Minimum = 0;
                        trackbar.Value = (int)player.Contrast;

                        break;
                    case 3:
                        trackbar.Maximum = 200;
                        trackbar.Minimum = 0;
                        trackbar.Value = (int)player.Brightness;

                        break;
                    case 4:
                        trackbar.Maximum = 200;
                        trackbar.Minimum = 0;
                        trackbar.Value = (int)player.Hue;

                        break;
                    case 5:
                        trackbar.Maximum = 200;
                        trackbar.Minimum = 0;
                        trackbar.Value = (int)player.Saturation;

                        break;
                    case 6:
                        trackbar.Maximum = 200;
                        trackbar.Minimum = 0;
                        trackbar.Value = (int)player.Gamma;

                        break;

                }
            }
        }
        bool mousedown=false;
        
        void trackbar_MouseDown(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            switch(e.Button){
                case MouseButtons.Left:
                mousedown = true;
                break;

        }
        }

        void trackbar_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TrackBar t = sender as TrackBar;
            if (mousedown)
            {
                int value = t.Value;

                switch (currentSelectedOption)
                {

                    case 0:

  

                            
                            player.currentVideoTime = value;
                            Console.WriteLine("try to set time to " + value);


                        break;
                    
                    case 1:
                        if (trackbar.Value==1) {
                            player.EnableAdjustOptions = 1;
                        } else {
                            player.EnableAdjustOptions = 0;
                        }
                        
                        break;
                    case 2:

                        player.Contrast = value/10f;

                        break;
                    case 3:

                        player.Brightness = value/10f;
                        break;
                    case 4:
                        player.Hue = value;
                        break;
                    case 5:

                        player.Saturation = value/10f;
                        break;
                    case 6:
                        player.Gamma = value/10f;
                        break;
                }

            }
   

        }




        
        Int64 time;
        Int64 crtime
        {
            get { return time; }
            set { time = value;
            console.Text = time.ToString();
            }
        }
        List<Int64> ilist = new List<Int64>();
        int currentSelectedOption = -1;
        void player_PlayTimeChange(object sender, VlcMediaPlayer.PlayTimeArgs e)
        {
     
            //throw new NotImplementedException();
        
             //   crtime = e.currenttime;
               // console.Text = e.currenttime.ToString();
            Console.WriteLine(e.currenttime);
            //Console.WriteLine("x: " + player.currentVideoTime);
              
            changeText(e.currenttime);
                if (!mousedown)
                {
                    switch (currentSelectedOption)
                    {
                        case 0:
                
                                changeTrack(e.currenttime);
                            
                            break;
                  
                    }
                }
     
        }
       public delegate void ctext(string t);
        public void changeText(string t) {
            if (this.InvokeRequired)
            {
                ctext c = new ctext(changeText);
                this.Invoke(c,new object[] {t});

            }
            else {
                console.Text = t;
            }
        
        
        }
        
        public delegate void cint64(Int64 i);
        public void changeTrack(Int64 t) {
            if (trackbar.InvokeRequired)
            {
                cint64 c = new cint64(changeTrack);
                trackbar.Invoke(c, new object[] { t });
            }
            else { 
                int value = (int)t;
                if(t<=trackbar.Maximum&&t>=0){
                    trackbar.Value = value;
            }
            }
        }
        public void changeText(Int64 t) {
            if (console.InvokeRequired)
            {
                cint64 c = new cint64(changeText);
                console.Invoke(c, new object[] { t });

            }
            else
            {
                console.Text = t.ToString();
            }
        
        
        }
        public delegate void setValue(int i);
        public void setSound(int i){
            if (soundbar.InvokeRequired) {
                setValue s = new setValue(setSound);
                soundbar.Invoke(s, new object[] { i });
            }else{
                if (i <= soundbar.Maximum && i >= 0)
                {
                    soundbar.Value = i;
                }
            
            }
        
        }


        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //throw new NotImplementedException();
            player.Dispose();
            Pbase.Dispose();

        }

        private void Playb_Click(object sender, EventArgs e)
        {
            
                player.Play();
                //throw new Exception("watch");
                // console.Text = player.playerState;

             //   console.Text = player.AudioTrackCount.ToString();
         /*
            VlcMedia c = player.getMedia();
                Int64 it = c.duration;
                console.Text = it.ToString();
                c.Dispose();
            */
            
        }

        private void Pauseb_Click(object sender, EventArgs e)
        {
            
            player.Pause();
  
        }

        private void Stopb_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void Openb_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                String path = ofd.FileName;
               // VLClibDM.VlcMedia vm = new VlcMedia(Pbase, path);
                player.setVlcMedia(path);
                media_set = true;
              //  console.Text = player.playerState;
                console.Text = player.Duration.ToString();
                
            }		

        }

        private void updateTrack(Label trac) {
          //  int total = player.AudioTrackCount;
         //   int current = player.currentAudioTrack;
            //trac.Text = current + "/" + total;

            trac.Text = player.currentSubtitleName;
            console.Text = player.currentSubtitle.ToString() ;
        }
        private int increment(int total,int current) {

            if (current < total)
            {
                return current+1;
            }
            else {
                return current;
            }
        
        }
        private int decrement(int total, int current)
        {

            if (current > 0 )
            {
                return current-1;
            }
            else
            {
                return current;
            }

        }
        private void up_Click(object sender, EventArgs e)
        {
            int total = player.SubtitleCount;
            int current = player.currentSubtitle;
            player.currentSubtitle = increment(total, current);
            updateTrack(Track);
        }

        private void down_Click(object sender, EventArgs e)
        {
            int total = player.SubtitleCount;
            int current = player.currentSubtitle;
            player.currentSubtitle = decrement(total, current);
            updateTrack(Track);


        }

        private void forward_Click(object sender, EventArgs e)
        {
            player.Forwared();
        }

        private void backward_Click(object sender, EventArgs e)
        {
            player.Backward();
        }

    }
}
