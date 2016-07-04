using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace windowMediaPlayerDM
{
    public partial class VLC_test : Form
    {

       // DirectoryInfo dc = new DirectoryInfo("C:\\Users\\Alan\\Documents\\GitHub\\DM_Player\\lib");

        public VLC_test()
        {
            InitializeComponent();

            // use this to switch tracks

            //vlcControl1.Audio.Tracks.

          //  AXVLC.IVLCControl2 c2 = ((AXVLC.IVLCControl2)vlcControl1);
            
            
            
         //   vlcControl1.VlcLibDirectory = dc;

            
            
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog()== DialogResult.OK) {

                FileInfo t = new FileInfo(ofd.FileName);

                vlcControl1.SetMedia(t);


               
            }


        }

        private void Play_Click(object sender, EventArgs e)
        {
            if (vlcControl1.GetCurrentMedia() != null) {

                vlcControl1.Play();
                label1.Text = vlcControl1.GetCurrentMedia().Duration.Milliseconds + "/" + vlcControl1.GetCurrentMedia().Duration.TotalMilliseconds;
            
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (vlcControl1.GetCurrentMedia() != null) {


                vlcControl1.Stop();
            }
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            if (vlcControl1.GetCurrentMedia() != null) {


                vlcControl1.Pause();
            }
        }
    }
}
