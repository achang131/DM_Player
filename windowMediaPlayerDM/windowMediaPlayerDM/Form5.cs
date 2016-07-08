using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

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

                label1.Text = vlcControl1.State.ToString() +"   " + vlcControl1.GetCurrentMedia().Duration.Milliseconds + "/" + vlcControl1.GetCurrentMedia().Duration.TotalMilliseconds + vlcControl1.Audio.Channel;
            
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
        getWebVideo gb;
        List<Uri> fullurls;
        private void web_button_Click(object sender, EventArgs e)
        {
             downloadstatus2.Text = "Fetching Url" + "http://himado.in/336121";
            String local_path= dir_box.Text;
            
            
         
            //enter url here
            Uri link = new Uri("http://himado.in/336121");


            //
        //    jsontest();
        
             gb = new getWebVideo(link,local_path);
  
            

            fullurls = gb.getDownlist;
            downloadstatus2.Text = "Url fetch comeplete!";

            for(int i=0;i<fullurls.Count;i++){

                Url_List.Items.Add(fullurls.ElementAt(i).LocalPath);
               // throw new Exception("origin") { };
            
            }

            Url_List.SelectedValueChanged += new EventHandler(Url_List_SelectedValueChanged);

           // link = new Uri(gb.getbasicUrl);
            
            //vlcControl1.SetMedia(link);

         //   FileInfo testfile = new FileInfo(gb.dlurl);

         //   vlcControl1.SetMedia(testfile);

        }

        void Url_List_SelectedValueChanged(object sender, EventArgs e)
        {
            // old code
            //gb.playDownlist = fullurls.ElementAt(Url_List.SelectedIndex);
            downloadstatus2.Text = "";
            using (WebClient nwb = new WebClient())
            {
                nwb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(nwb_DownloadProgressChanged);
                nwb.DownloadFileCompleted += new AsyncCompletedEventHandler(nwb_DownloadFileCompleted);
                gb.downlaodFile(fullurls.ElementAt(Url_List.SelectedIndex), nwb);

                nwb.Dispose();
            }

            vlcControl1.SetMedia(gb.playDownlist);
        }

        void nwb_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            FileInfo file = new FileInfo(gb.playDownlist.LocalPath);

            if (file.Length == 0) {

                downloadstatus2.Text = "Some thing went wrong, Can't download the file online !";
                file.Delete();
            }
            else
            {
                downloadstatus2.Text = "Download Complete !";
            }
                download_status.Text = "";
            downloadbar.Value = 0;
            

        }

        void jsontest() {

    

            using (WebClient wb = new WebClient()) {

                var json = wb.DownloadString(new Uri("http://himado.in/&isOtherSource=&post_id=386378&sourceid=&commentlink="));

                

                comment_test.Text=   json.ToString();
                



                wb.Dispose();
            }
        
        
        }

        void nwb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadbar.Minimum = 0;
            downloadbar.Maximum = 100;
            downloadbar.Value = e.ProgressPercentage;

            String bytereceive = e.BytesReceived.ToString();
            String totalbyte = e.TotalBytesToReceive.ToString();

            download_status.Text = bytereceive + "/" + totalbyte;

            //throw new NotImplementedException();

            
        }
        


    }
}
