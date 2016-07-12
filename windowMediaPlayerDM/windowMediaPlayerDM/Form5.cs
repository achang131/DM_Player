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
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
namespace windowMediaPlayerDM
{
    public partial class VLC_test : Form
    {

       // DirectoryInfo dc = new DirectoryInfo("C:\\Users\\Alan\\Documents\\GitHub\\DM_Player\\lib");

        List<Label> testlist = new List<Label>();
        List<Label> testlist2 = new List<Label>();
        List<Label> testlist3 = new List<Label>();
        System.Timers.Timer timer = new System.Timers.Timer();
        Label[] atest = new Label[100];

        public VLC_test()
        {
            InitializeComponent();

            // use this to switch tracks

            //vlcControl1.Audio.Tracks.

          //  AXVLC.IVLCControl2 c2 = ((AXVLC.IVLCControl2)vlcControl1);
            
            
            
         //   vlcControl1.VlcLibDirectory = dc;
            

            for (int i = 0; i < 100; i++) {

                Label l = new Label();
                l.Location = new Point(ClientRectangle.Right+i,i);
                l.Text = " test ";
                this.Controls.Add(l);
             //   this.testlist.Add(l);

                Label j = new Label();
                j.Text = " test ";
                j.Location = new Point(ClientRectangle.Right + i, i);
                this.testlist.Add(j);
                Label k = new Label();
                this.testlist3.Add(k);
                k.Text = " test ";
                k.Location = new Point(ClientRectangle.Right + i, i);
                atest[i] = k;
            }

            
            
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

             //   label1.Text = vlcControl1.State.ToString() +"   " + vlcControl1.GetCurrentMedia().Duration.Milliseconds + "/" + vlcControl1.GetCurrentMedia().Duration.TotalMilliseconds + vlcControl1.Audio.Channel;
            
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

        private void test_button_Click(object sender, EventArgs e)
        {

            Stopwatch sw = new Stopwatch();

            int listn = testlist.Count;



            sw.Start();
            int controln = (Controls.OfType<Label>().Count());
            while (Controls.OfType<Label>().Count() > 0)
            {


                for (int i = 0; i < Controls.OfType<Label>().Count(); i++)
                {

                    if (Controls.OfType<Label>().ElementAt(i).Location.X > 0)
                    {

                        Controls.OfType<Label>().ElementAt(i).Location = new Point(Controls.OfType<Label>().ElementAt(i).Location.X - 10, Controls.OfType<Label>().ElementAt(i).Location.Y);


                    }
                    else
                    {

                        Controls.OfType<Label>().ElementAt(i).Dispose();
                    }



                }



            }




            for (int i = 0; i < Controls.OfType<Label>().Count(); i++)
            {

                Controls.OfType<Label>().ElementAt(i).Text = " " + i;

            }
            sw.Stop();




            long totalt = sw.ElapsedMilliseconds;

            foreach (Label l in testlist)
            {

                this.Controls.Add(l);
            }
            sw.Reset();
            
            
            
            sw.Start();
            while (testlist.Count > 0)
            {
                List<Label> removelist = new List<Label>();

                for (int i = 0; i < testlist.Count; i++)
                {

                    if (testlist.ElementAt(i).Location.X > 0)
                    {

                        testlist.ElementAt(i).Location = new Point(testlist.ElementAt(i).Location.X - 10, testlist.ElementAt(i).Location.Y);


                    }
                    else {
                        removelist.Add(testlist.ElementAt(i));
                        testlist.ElementAt(i).Dispose();
                    }



                }

                if (removelist.Count > 0)
                {
                    for (int i = 0; i < removelist.Count; i++)
                    {
                        testlist.Remove(removelist.ElementAt(i));

                    }
                }

            }
            sw.Stop();

            

            long totalt2 = sw.ElapsedMilliseconds;

            foreach (Label l in testlist3)
            {

                this.Controls.Add(l);
            }
            int arrayn = atest.Length;
            sw.Reset();

            sw.Start();
            int c = 0;

            while (c < 100) {

                for (int i = 0; i < atest.Length; i++) {

                    if (atest[i] != null)
                    {
                        if (atest[i].Location.X > 0)
                        {
                            atest[i].Location=new Point(atest[i].Location.X-10,atest[i].Location.Y);



                        }
                        else {


                            atest[i].Dispose();
                            atest[i] = null;
                            c++;
                        }


                    }

                
                
                
                
                }
            
            
            
            
            
            }

            sw.Stop();

            long totalt3 = sw.ElapsedMilliseconds;

            test_sLabel.Text = "controls " + totalt + " Lists " + totalt2 + " array " + totalt3;


            print.Text = listn + " " + controln + " " + arrayn;


        }
        


    }
}
