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
          
          player.vlc.StateChange += new EventHandler(player_StateChange);
         player.PlayTimeChange+= new EventHandler<VlcMediaPlayer.PlayTimeArgs>(player_PlayTimeChange);
          this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
          AllocConsole();
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
        void player_PlayTimeChange(object sender, VlcMediaPlayer.PlayTimeArgs e)
        {
            //throw new NotImplementedException();
        
             //   crtime = e.currenttime;
               // console.Text = e.currenttime.ToString();
            Console.WriteLine(e.currenttime);
            //Console.WriteLine("x: " + player.currentVideoTime);
                changeText(e.currenttime);
     
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
        void player_StateChange(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            changeText(player.playerState);
           
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

    }
}
