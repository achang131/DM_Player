﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.IO;
using System.Net.Http;
using System.Net;


namespace windowMediaPlayerDM
{
    public partial class Form1 : Form{
        String media_dir;
         String danmoku_dir;
        OpenFileDialog media;
        OpenFileDialog danmoku;
        int ycurrent;
        int xstart;
        int time_counter;
        int speed_control;
        Color userColor;
        List<Label> comment_storage = new List<Label>();
        XmlTextReader dm_comment;
        List<String[]> comment = new List<String[]>();
      //  List<Label> remove_List = new List<Label>();
        int time_offset;
        int move_distance;
        int playedcomment;
        List<String[]> Media_List = new List<String[]>();
        List<String[]> DM_List = new List<String[]>();
        List<String[]> FullDM_List = new List<String[]>();

        int _duplicates;

        bool _first_load;
        WMPLib.IWMPPlaylist Media_Playlist;
        bool sommentswitch;

        Settings fm4;
        int commentdestroy;
        int auto_base;
        int vpos;

        int currentLanguage;

        int vpos_end;

        int vpos_video;

        bool threading_mode;

        int offset_auto;

        delegate void tmovelabel(Label l,int x, int y);

        delegate void exmovelabel(Label l);

        delegate void commentEnginecomp();

        delegate void setTrack_t(int time);

        delegate void setDouble(double d);

        delegate void setInt(int i);

        delegate void setString(String s);

        delegate void setUPCommentMoveEngine(comment_move_engine cme,List<Label> storage, int moved);

        Dictionary<int, String> comment2 = new Dictionary<int, String>();

        TaskFactory th1 = new TaskFactory();

        bool videoloop;

        Form2 fm2;

  //      public DispatcherTimer newtimer = new DispatcherTimer();

    //    public DispatcherTimer newTimer2 = new DispatcherTimer();

//        public System.Timers.Timer newTimer2 = new System.Timers.Timer();

      //  Form2 fm2;

        //



        double replacetimer1_interval;

        BackgroundWorker replacetimer1;

        bool _isPlaying;

        Comment_window fm3;

        bool multi_dm_mode;

        bool auto_TimeMatch;

        String audioinfo;

        BackgroundWorker replacetimer2;

        double replacetimer2_interval;

        BackgroundWorker replacetimer3;
        double replacetimer3_interval;

        double _timer1, _timer3;
        int lcount;

        int _distance;
        // Next try add setting (new window)  and Play/DM List(new window possible tabs ?)

        int choose_player; //1 for Windows media player 2, for vlc player


        int current_volume;

        bool mousedown;

        int vlcTIme;

        bool autoMlist;

        int vVolume;

        bool startautotime;

        int commentLimit;

        int commentmethod;




        public Form1()
        {
            InitializeComponent();

            media = new OpenFileDialog();
            danmoku = new OpenFileDialog();
            danmoku.Filter = "xml |*.xml";
            media.Filter = "";
           

         

          //  fm2 = new Form2();

            // controls the speed of the DM  default at 5? the lesser the faster 

            //comment settings


          
            Media_Player.ClickEvent += new AxWMPLib._WMPOCXEvents_ClickEventHandler(Media_Player_ClickEvent);

            Media_Player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Media_Player_PlayStateChange);


            this.ClientSizeChanged += new EventHandler(Form1_ClientSizeChanged);



            Media_Player.windowlessVideo = true;

            Media_Player.stretchToFit = true;



            audioinfo = "test";
           
           /*
            
            this.th1.StartNew(()=>{
                this.runEngine(2);
            
            });


           */
            //maybe add a 3rd backgroundworker to move comment will make the programe even more smoother ?

            BackgroundworkerSetup();


            
       //     Media_Player.SendToBack();
            


            this.LocationChanged += new EventHandler(Form1_LocationChanged);

            if (choose_player == 1)
            {

                Media_Playlist = Media_Player.playlistCollection.newPlaylist("My_List");



                //replacetimer1.WorkerSupportsCancellation = true;

                Media_Player.settings.autoStart = true;

            }

            this.MouseWheel+=new MouseEventHandler(Form1_MouseWheel);

            vlcPlayer.MouseWheel+=new MouseEventHandler(Form1_MouseWheel);
            vlcPlayer.MouseClick += new MouseEventHandler(vlcPlayer_MouseClick);
            vlcPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(vlcPlayer_Playing);
            vlcPlayer.Stopped += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerStoppedEventArgs>(vlcPlayer_Stopped);
            vlcPlayer.Paused += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs>(vlcPlayer_Paused);
            vlcPlayer.MediaChanged += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerMediaChangedEventArgs>(vlcPlayer_MediaChanged);
            vlcPlayer.TimeChanged += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs>(vlcPlayer_TimeChanged);
            vlcPlayer.Opening += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerOpeningEventArgs>(vlcPlayer_Opening);
            
            
            
            vlcPlayer.AllowDrop = true;
            vlcPlayer.DragEnter += new DragEventHandler(vlcPlayer_DragEnter);
            
            

            VLC_track.MouseDown += new MouseEventHandler(VLC_track_MouseDown);
            VLC_track.MouseUp += new MouseEventHandler(VLC_track_MouseUp);
            VLC_track.ValueChanged += new EventHandler(VLC_track_ValueChanged);
            VLC_track.MouseMove += new MouseEventHandler(VLC_track_MouseMove);

            sound_trackbar.Maximum = 150;
            sound_trackbar.Minimum = 0;
            sound_trackbar.Value = vVolume;
            sound_trackbar.ValueChanged += new EventHandler(sound_trackbar_ValueChanged);


            this.KeyUp += new KeyEventHandler(Form1_KeyUp);

            vlcPlayer.KeyUp += new KeyEventHandler(vlcPlayer_KeyUp);
            vlcPlayer.MouseDoubleClick += new MouseEventHandler(vlcPlayer_MouseDoubleClick);
            this.MouseDoubleClick += new MouseEventHandler(Form1_MouseDoubleClick);


            fullscreen = false;

            if (choose_player == 1)
            {

                vlcPlayer.Dispose();
            }
            else {

                Media_Player.Dispose();
            }

        

          //  this.Disposed += new EventHandler(Form1_Disposed);

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);


            //CommentEngineSetup(cme1, comment_storage3, move_distance);
            //CommentEngineSetup(cme2, comment_storage4, move_distance);
          //  commentWindowSetup();

          cme1 = new comment_move_engine(move_distance);
          cme2 = new comment_move_engine(move_distance);
          cme3 = new comment_move_engine(move_distance);
          cme4 = new comment_move_engine(move_distance);
          cme5 = new comment_move_engine(move_distance);
          cme6 = new comment_move_engine(move_distance);

          CSettings();
  
        }
        Comment_window fm3_1, fm3_2, fm3_3,fm3_4,fm3_5,fm3_6,fm3_7;
        comment_move_engine cme1,cme3,cme4,cme5,cme6;
        comment_move_engine cme2;
        int threadNumber;

        void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FullScreenSwitch();
        }

        void vlcPlayer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FullScreenSwitch();
        }

        void vlcPlayer_DragEnter(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException(;)

            
            //printvlc(e.Data.GetDataPresent(DataFormats.Locale).ToString());

        }

        int temp_sound;

        
        
        
        void sound_trackbar_ValueChanged(object sender, EventArgs e)
        {
            
            
                vVolume = sound_trackbar.Value;
                vlcPlayer.Audio.Volume = vVolume;
                printvlc("Volume: " + vVolume);
                if (vVolume == 0)
                {

                    vlcSound_button.BackgroundImage = Properties.Resources.mute;


                }
                else {

                    vlcSound_button.BackgroundImage = Properties.Resources.sound;
                }

        }


        bool fullscreen;
        Size playersize;
        Point playerlocation;
        Size commentPanelsize;
        Point commentPanelLocation;

        void FullScreenSwitch() {
            if (fullscreen)
            {

                show_all_fm1();
            }
            else
            {

                hide_All_fm1();
            }
        
        
        }

        void KeyUpActions(KeyEventArgs e) {
           
            //Danmoku_status.Text = e.KeyValue.ToString();

            switch (e.KeyValue) { 
                    //esc key
                case 27:

                    if (fullscreen) {

                        show_all_fm1();
                    
                    }

                    break;
            
                    //F11 key
                case 122:
                    //hide if false and show if true
                    FullScreenSwitch();

                    break;
            
            
            }
        
        }
        void vlcPlayer_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpActions(e);
        }

        void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpActions(e);
        }

        void show_all_fm1() {
          //  if (Windowstate == FormWindowState.Maximized)
          //  {
               this.WindowState = FormWindowState.Maximized;
         //   }
         //   else {

         ////       this.Size = originalsize;
         //       this.Location = originalLocation;
            
            
         //   }
            for (int i = 0; i < this.Controls.OfType<Button>().Count(); i++)
            {
                this.Controls.OfType<Button>().ElementAt(i).Show();
            }
            for (int i = 0; i < this.Controls.OfType<Label>().Count(); i++)
            {
                this.Controls.OfType<Label>().ElementAt(i).Show();

            }
            this.sound_trackbar.Show();
            this.menuStrip1.Show();

            this.statusStrip1.Show();
          //  this.statusStrip1.BringToFront();

            this.VLC_track.Show();
         //   this.VLC_track.BringToFront();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            fullscreen = false;

            vlcPlayer.Size = playersize;
            vlcPlayer.Location = playerlocation;

          //  this.TopMost = false;

            if (fm3 != null) {

                fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
             //   fm3.TopMost = false;

             //   ///////fm3.Owner = this;
            }

            Cursor.Show();

        }

        void hideCursor() {

            if (vlcPlayer.IsPlaying)
            {
                if (mousemoving)
                {


                    Cursor.Show();
                }
                else
                {
                    Cursor.Hide();
                }
            }
            else
            {
                
                Cursor.Show();
            }
        
        }


        FormWindowState Windowstate;
        Size originalsize;
        Point originalLocation;
        void hide_All_fm1() {

            originalsize = this.currentMediaWindowSize;
            originalLocation = this.currentMediaWindowLocation;

            this.WindowState = FormWindowState.Normal;

            playersize = vlcPlayer.Size;
            playerlocation = vlcPlayer.Location;

            if (fm3 != null)
            {
                commentPanelsize = fm3.Size;
                commentPanelLocation = fm3.Location;


            }
            //hide all buttons
            for (int i = 0; i < this.Controls.OfType<Button>().Count();i++ ){
                this.Controls.OfType<Button>().ElementAt(i).Hide();

            }
            for (int i = 0; i < this.Controls.OfType<Label>().Count(); i++) {
                this.Controls.OfType<Label>().ElementAt(i).Hide();
            
            }
            this.sound_trackbar.Hide();
            this.menuStrip1.Hide();

            this.statusStrip1.Hide();

            this.VLC_track.Hide();

                this.FormBorderStyle = FormBorderStyle.None;


                fullscreen = true;

           //     if (this.ClientRectangle.Size.Height < 1028)
          //      {
             //       vlcPlayer.Size = this.ClientRectangle.Size;
             //       vlcPlayer.Location = new Point(this.ClientRectangle.Location.X, this.ClientRectangle.Location.Y);
          //      }
          //      else {

                   // vlcPlayer.Size = new Size(this.ClientRectangle.Width+8, this.ClientRectangle.Height+30);
//
                    vlcPlayer.Size = new Size(1920,1080);
         //           vlcPlayer.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width,Screen.PrimaryScreen.WorkingArea.Height);
        //           vlcPlayer.Location = new Point(this.ClientRectangle.Location.X+8, this.ClientRectangle.Location.Y+8);
                   vlcPlayer.Location = new Point(0, 0);
         //       }
           
               // vlcPlayer.Location = new Point(this.ClientRectangle.Location.X, this.ClientRectangle.Location.Y);




                if (fm3 != null) {

                    fm3.Size = vlcPlayer.Size;
                    fm3.Location = vlcPlayer.Location;
                
                }

               // this.MaximizedBounds = Screen.PrimaryScreen.Bounds;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                Cursor.Hide();
 
            }

            void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                saveSettings();
            }

            //read the settings form the xml
            void form1_loadSetting_XML() {


                FileInfo config = new FileInfo(@Directory.GetCurrentDirectory() + "\\" + "settings.DMconfig");

                if(config.Exists){
            
                XmlTextReader reader =new XmlTextReader(config.FullName);

                try
                {
                    String currentElement = "";
                    while (reader.Read())
                    {

                        switch (reader.NodeType)
                        {

                            case XmlNodeType.Element:

                                currentElement = reader.Name;

                                break;

                            case XmlNodeType.Text:

                                switch(currentElement){
                            
                                    case "Default_Path":

                                        current_dir_url= reader.Value;


                                        break;

                                    case "Comment_Speed":
                                        _distance = Int32.Parse(reader.Value);

                                        break;


                                    case "volume":

                                        vVolume = Int32.Parse(reader.Value);


                                        break;

                                    case "CommentLimit":

                                        commentLimit = Int32.Parse(reader.Value);

                                        break;
                            
                                }

                                break;

                            case XmlNodeType.EndElement:

                                currentElement = "";
                                break;


                        }




                    }
                    reader.Close();

                }
                catch (XmlException) { };
        
                }
        
        
        
            }

            void saveSettings() {

                //throw new NotImplementedException();
                //save the settings on disposed
                FileInfo settings = new FileInfo(Directory.GetCurrentDirectory() + "\\settings.DMconfig");

                if (settings.Exists)
                {
                    try
                    {
                        settings.Delete();
                    }
                    catch (IOException) { }
                }

                using (XmlWriter writer = XmlWriter.Create("settings.DMconfig"))
                {

                    writer.WriteStartDocument();
                    writer.WriteStartElement("settings");
                    writer.WriteElementString("Default_Path", current_dir_url);
                    writer.WriteElementString("Comment_Speed", _distance.ToString());
                    writer.WriteElementString("volume", vVolume.ToString());
                    writer.WriteElementString("CommentLimit", commentLimit.ToString());
                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Close();

                
                }
            }

            //writes settings into xml file
            void Form1_Disposed(object sender, EventArgs e)
            {


            


            }

            void vlcPlayer_Opening(object sender, Vlc.DotNet.Core.VlcMediaPlayerOpeningEventArgs e)
            {
               // throw new NotImplementedException();
                /*
                onLoadUp();
                if (_first_load == false)
                {
                    vlc_videoSetup();
                }
                test_label.Text = vlcPlayer.State.ToString();



                timerStart();
                 */
                vlcPlayer.Audio.Volume = vVolume;
            Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
            FileInfo nfile = new FileInfo(nuri.LocalPath);
            selectPlaying_box(nfile.Name);
            setVLCname(nfile);
        }
        bool mousemoving;
        Point mouseLocation;
        void VLC_track_MouseMove(object sender, MouseEventArgs e)
        {

            mouseLocation = System.Windows.Forms.Cursor.Position;
                mousemoving = true;

            

        }

        void vlcPlayer_Paused(object sender, Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs e)
        {
            test_label.Text = vlcPlayer.State.ToString();




            timerStop();

        }



        void VLC_track_ValueChanged(object sender, EventArgs e)
        {

            changetime();

        }
        int vlc_track_time = -2;
        void changetime() {


           // if ( mousedown == true && VLC_track.Value != time_counter)
           if(mousedown==true)
            {
             //   timerStop();
             //   vlcPlayer.Time = VLC_track.Value * 10;

                if (vlcPlayer.IsPlaying)
                {
                    vlc_track_time = VLC_track.Value * 10;

                }
                else {

                    vlcPlayer.Time = VLC_track.Value * 10;
                    time_counter = VLC_track.Value;
                
                
                }
               // if (VLC_track.Value * 10 >= vlcPlayer.Length) {

                //    manual_checkend((int)vlcPlayer.Length,VLC_track.Value);
                
                    //diable this for now since already have timer to check

                    //it it doesn't work out maybe disable the one in timer will be better ?
                
             //   }


            }

        
        }

        void VLC_track_MouseUp(object sender, MouseEventArgs e)
        {

            changetime();

            mousedown = false;


        }

        void VLC_track_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
        }

        void vlcPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            vlc_display.Text = "end reached";
            vlcPlayer.Time = 0;
            time_counter = 0;
            timerStop();
            if (vlcPlayer.IsPlaying)
            {
                vlcPlayer.Stop();
            }

        }

        void manual_checkend(int end, int current) {

            //set video loops here

                if (current >=
                    end && end !=0)
                {

                   // vlc_display.Text = vlc_display.Text+ "end reached";
                    if (videoloop == false)
                    {
                        //vlc_track_time = 0;
                        
                        time_counter = 0;
                        if (vlcPlayer.IsPlaying)
                        {
                            vlcPlayer.Stop();
                        }


                       

                    }
                    else
                    {
                        if (Media_List.Count > 1)
                        {
                            int currentinx = -2;
                            try
                            {
                                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                                FileInfo nfile = new FileInfo(nuri.LocalPath);
                                String currentv = nfile.Name;

                                for (int i = 0; i < Media_List.Count(); i++)
                            {

                                if (Media_List.ElementAt(i)[1].Equals(currentv))
                                {

                                    currentinx = i;

                                }

                            }
                            if (currentinx == Media_List.Count - 1)
                            {

                                currentinx = 0;
                            }
                            else
                            {
                                currentinx++;
                            }
                            FileInfo nv = new FileInfo(Media_List.ElementAt(currentinx)[0]);
                      //      if (vlcPlayer.IsPlaying)
                     //       {
                     //           vlcPlayer.Stop();
                     //       }
                            comment.Clear();
                            comment2.Clear();
                            DM_List.Clear();
                            time_counter = 0;
                            playedcomment = 0;
                            vlcPlayer.SetMedia(nv);
                            vlc_track_time = -2;
                            autoLoadByName(nv);
                            vlcPlayer.Play();
                            timerStart();

                            this.Text = "DM Player " + nv.Name;
                            }
                            catch (ArgumentException) { };

                        }
                        else
                        {
                            time_counter = 0;
                            vlc_track_time = 0;
                            playedcomment = 0;


                        }


                    
                }
            }
        }
    
        void vlcPlayer_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
        {
         //   time_counter =(int) e.NewTime;
         //   print2("time: "+e.ToString()+" / "+e.NewTime.ToString());
          //  vlcTime = (int)e.NewTime;

            if (auto_TimeMatch==false)
            {

                time_counter = ((int)(e.NewTime) / 10 );

            }
            else
            {

                time_counter = ((int)(e.NewTime) / 10);

            }


            setTime_track(time_counter);


        }
        void setTime_track(int t) {

            if (InvokeRequired) {

                setInt st = new setInt(setTime_track);

                try
                {
                    this.Invoke(st, new object[] { t });
                }
                catch (Exception) { }
            }


            else {
                try
                {
                    VLC_track.Value = t;
                }
                catch (Exception) { };
            }
        
        
        
        }
        void vlcPlayer_MediaChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerMediaChangedEventArgs e)
        {
            test_label.Text = vlcPlayer.State.ToString();

            _first_load = false;

            playedcomment = 0;

            time_counter = 0;

            vlc_track_time = -2;

            VLC_track.Value = 0;

            timerStop();


            removeAllComments();
        }

        void vlcPlayer_Stopped(object sender, Vlc.DotNet.Core.VlcMediaPlayerStoppedEventArgs e)
        {
            test_label.Text = vlcPlayer.State.ToString();

            playedcomment = 0;

            time_counter = 0;

            vlc_track_time = -2;

            VLC_track.Value = 0;

            timerStop();

       

            removeAllComments();
        }

        void vlcPlayer_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            onLoadUp();
            if(_first_load==false){
                vlc_videoSetup();
            }
            test_label.Text = vlcPlayer.State.ToString();

            

            timerStart();
            vlcPlayer.Audio.Volume = vVolume;
            
        }

        void vlcPlayer_MouseClick(object sender, MouseEventArgs e)
        {
            Media_Player_ClickAction();
           
        



        }

        void CSettings() {



            multi_dm_mode = false;
            if (multi_dm_mode)
            {

                setDMsToolStripMenuItem.Text = setDMsToolStripMenuItem.Text + " Multi On";

            }
            else
            {

                setDMsToolStripMenuItem.Text = setDMsToolStripMenuItem.Text + "Multi Off";
            }

            playedcomment = 0;

            speed_control = 1;

            time_offset = 0;

            move_distance = 10;

            _distance = 10;

            timer1.Interval = 1;

            commentdestroy = -100;

            sommentswitch = true;

            threading_mode = false;

            vpos = -1;

            _first_load = false;

            auto_base = 0;

            userColor = Color.DarkGray;

            replacetimer1_interval = 29;
            replacetimer3_interval = 30;
            replacetimer2_interval = 9;


            
            cme1.setInterval = 40;
            cme2.setInterval = 41;

            threadNumber = 8;

            auto_TimeMatch = true;


            //-1400 for gp movie
            offset_auto = -500;



            _duplicates = 0;

            _timer1 = replacetimer1_interval;
            _timer3 = replacetimer3_interval;

            //   newtimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            _isPlaying = true;

            //         newtimer.Tick += new EventHandler(newtimer_Tick);

            //    newTimer2.Tick += new EventHandler(newTimer2_Tick);

            //          newTimer2.Elapsed += new System.Timers.ElapsedEventHandler(newTimer2_Elapsed);

            //          newtimer.Interval= TimeSpan.FromMilliseconds(.1);

            //    newTimer2.Interval = TimeSpan.FromMilliseconds(.1);

            //         newTimer2.Interval = .1;



            //timer1.interval default at 100 milli second
            time_counter = 0;


            vpos_end = 0;
            vpos_video = 0;


            //initialize the y value to 40 so it will default start at 40
            ycurrent = 40;
            xstart = ClientRectangle.Right;


            //Media_Player.enableContextMenu = true;


            //


            choose_player = 2;

            switchPlayer(choose_player);

            current_volume = vlcPlayer.Audio.Volume;

            mousedown = false;

            vVolume = 50;


            //decide if it's going to load all the same media/xml file under the same directory into the playlist

            autoMlist = true;

            videoloop = true;


            //change this to auto start time match
            startautotime = false;

            commentLimit = 65;


            //set the comment method  0 = default 1 = using list to move labels
            commentmethod = 1;


            form1_loadSetting_XML();


        
        }
        void switchPlayer(int c) {

            switch (c) { 
            
                case 1:
                    vlcPlayer.Hide();
                    VLC_track.Hide();

                    vlcPlay_button.Hide();
                    vlcSound_button.Hide();
                    vlcStop_button.Hide();


                    break;


                case 2:
                    Media_Player.Hide();

                    break;
            
            
            
            }
        
        
        }

        void BackgroundworkerSetup() {

            replacetimer1 = new BackgroundWorker();
            replacetimer1.DoWork += new DoWorkEventHandler(replacetimer1_DoWork);

            replacetimer2 = new BackgroundWorker();
            replacetimer2.DoWork += new DoWorkEventHandler(replacetimer2_DoWork);
            replacetimer2.WorkerSupportsCancellation = true;

            replacetimer3 = new BackgroundWorker();
            replacetimer3.DoWork += new DoWorkEventHandler(replacetimer3_DoWork);
            replacetimer3.WorkerSupportsCancellation = true;
        
        
        }

        void changingSpeedontime() {

            if (fm3 != null)
            {
                int lnumber = fm3.Controls.OfType<Label>().Count();
                if (lnumber > 70)
                {
                    move_distance = (int)(_distance * 3);
                }
                else if (lnumber > 60)
                {
                    move_distance = (int)(_distance * 2.5);
                }
                else if (lnumber > 50)
                {
                    move_distance = (int)(_distance * 1.7);

                }
                else if (lnumber > 40)
                {

                    move_distance = (int)(_distance * 1.5);

                }
                else if (lnumber > 30)
                {

                    move_distance = (int)(_distance * 1.3);
                }
                else
                {

                    move_distance = _distance;
                }
                if (cme1 != null) {

                    cme1.setMoveDistance = move_distance;

                
                }
                if (cme2 != null) {

                    cme2.setMoveDistance = move_distance;
                    cme3.setMoveDistance = move_distance;
                    cme4.setMoveDistance = move_distance;
                    cme5.setMoveDistance = move_distance;
                    cme6.setMoveDistance = move_distance;

                }

            }

        }
        void replacetimer3_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!replacetimer3.CancellationPending)
            {

                changingSpeedontime();
                moveComment_thread2();

                //commentEngine_thread();

                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(replacetimer3_interval));

            }
        }

        void replacetimer2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!replacetimer2.CancellationPending) {

               // moveComment_thread();

                commentEngine_thread();

                changingSpeedontime();

                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(replacetimer2_interval));
            
            }
        }
        void replacetimer3_start()
        {
            if (!replacetimer3.IsBusy)
            {
                replacetimer3.RunWorkerAsync();

            }

        }
        void replacetimer3_stop()
        {
            //only stops if they are running to avoid freeze ?

        //    if (replacetimer3.IsBusy)
        //    {
                try
                {
                    replacetimer3.CancelAsync();
                }
                catch (Exception) { }

          //  }
        }
        void replacetimer2_start() {
            if(!replacetimer2.IsBusy)
            replacetimer2.RunWorkerAsync();
        
        }
        void replacetimer2_stop() {
          //  if (replacetimer2.IsBusy)
          //  {
                try
                {
                    replacetimer2.CancelAsync();
                }
                catch (Exception) { }
           // }
        }

        void printvlc(String s) {
            this.vlc_display.Text = s;
        
        }
        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (vVolume < 0) {

                vVolume = 50;
            }

       //    Danmoku_status.Text=e.Delta.ToString();
            switch (e.Delta)
            {
                case 120:

                    if (choose_player == 1)
                    {
                        Media_Player.settings.volume++;
                    }
                    else {
                        vVolume++;
                        sound_trackbar.Value = vVolume;
                        vlcPlayer.Audio.Volume = vVolume;
                        //vlcPlayer.Audio.Volume++;
                        printvlc("Volume: "+vlcPlayer.Audio.Volume);
                       // throw new Exception("get here");
                    }

                    break;


                case -120:
                    if (choose_player == 1)
                    {
                        Media_Player.settings.volume--;
                    }
                    else
                    {
                        if(vVolume!=0){
                        vVolume--;
                        };
                        sound_trackbar.Value = vVolume;
                        vlcPlayer.Audio.Volume = vVolume;
                        //vlcPlayer.Audio.Volume--;
                        printvlc("Volume: " + vlcPlayer.Audio.Volume);
                    }

                    break;

            }
        }

        void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (fm3 != null)
            {
                fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
               // fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);

                fm3_1.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3_2.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3_3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3_4.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3_5.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3_6.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3_7.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
            }
        }
        public Size currentMediaWindowSize {
            
           
            get {
                Size temp =new Size ( Media_Player.ClientSize.Width, Media_Player.ClientSize.Height-40 );
                
                
                return temp ;}
        
        
        
        }

        public  Point currentMediaWindowLocation {

            get { 
            
                Point temp = new Point(this.ClientRectangle.Left,this.ClientRectangle.Bottom);

                return temp;
            
            }
        
        }


        void replacetimer1_start() {

            if (replacetimer1.IsBusy != true) {

                replacetimer1.RunWorkerAsync();
            }
        
        
        }
        void replacetimer1_stop() {
            if (replacetimer1.IsBusy)
            {
                try
                {
                    replacetimer1.CancelAsync();
                }
                catch (InvalidOperationException) { };
            }
        
        }
        private void replacetimer1_DoWork(object sender, DoWorkEventArgs e) {


           // while (!replacetimer1.CancellationPending)
            while (_isPlaying)
            {

                changingSpeedontime();

  //               commentEngine_thread();

                moveComment_thread();

              //  System.Threading.Thread.Sleep(replacetimer1_interval);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(replacetimer1_interval));
            }
        
        
        }





        //load xml into a List and foreach addcomment ?
        void addComment(int time,String comment) { 


        //time counter dependent
            if (time == time_counter) {

                //createLabel(comment);
                makeCCompact(comment);
            
            }
        
        
        }
        void addComment(int currenttime,Dictionary<int,String> d) {

            if (currenttime!=vpos && d.ContainsKey(currenttime))
            {
                playedcomment++;
               // createLabel(d[currenttime]);
                makeCCompact(d[currenttime]);
                vpos = currenttime;
            }
        
        }

        void addComment(int currenttime, int time, String comment) {

            if (currenttime == time) {

                playedcomment++;
               // createLabel(comment);

                makeCCompact(comment);
                
            }
        }



        void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            Media_Player.Size = new System.Drawing.Size(ClientRectangle.Width,ClientRectangle.Height-49);

            if (fm3 != null)
            {
                if (fullscreen == false)
                {
                    fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_1.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_2.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_4.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_5.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_6.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                    fm3_7.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);

                    fm3_1.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3_2.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3_3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3_4.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3_5.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3_6.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                    fm3_7.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                }
            
            }

            if (fullscreen == false)
            {
                vlcPlayer.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 94);
                vlcPlayer.Location = new Point(0, ClientRectangle.Top + 29);
            }


            vlcPlay_button.Location = new Point(32, statusStrip1.Location.Y - 19);

            vlcStop_button.Location = new Point(77, statusStrip1.Location.Y - 19);

            vlcSound_button.Location = new Point(ClientRectangle.Right - 182, statusStrip1.Location.Y - 19);

            loop_button.Location = new Point(ClientRectangle.Right - 218, statusStrip1.Location.Y - 19);

            last_track.Location = new Point(145, statusStrip1.Location.Y - 19);
            next_track.Location = new Point(194, statusStrip1.Location.Y - 19);

            VLC_track.Size = new Size(statusStrip1.Size.Width-171, 45);
            VLC_track.Location = new Point(0,statusStrip1.Location.Y-46);
            sound_trackbar.Location = new Point(VLC_track.Size.Width, statusStrip1.Location.Y-46);
            
        }
        
        // form1 ends
        void autoTimesetup()
        {
            try
            {
                if (choose_player == 1)
                {
                    var tconsol = ((WMPLib.IWMPControls3)Media_Player.Ctlcontrols);

                    currentLanguage = tconsol.currentAudioLanguageIndex;

                    lcount = tconsol.audioLanguageCount;



                    vpos_video = (int)(Media_Player.Ctlcontrols.currentItem.duration * 100);

                }
                else {


                    vlc_videoSetup();
                 //   vlcPlayer.GetCurrentMedia().StateChanged += new EventHandler<Vlc.DotNet.Core.VlcMediaStateChangedEventArgs>(Form1_StateChanged);
         
                
                }


                cautoSet();

              //  vpos_video = (int)(tconsol.currentItem.duration * 100);
                

                print3("Video Length: " + vpos_video + "/" + "Comment time: " + vpos_end + "  CL: " + currentLanguage + " total: " + lcount);

            

              
            }
            catch (NullReferenceException) { Console.WriteLine("in autoTimeSetup"); }
        }


       
        void cautoSet() {


            if (vpos_end > vpos_video * 1.3 || startautotime==false)
            {

                auto_TimeMatch = false;

            }
            else
            {

                auto_TimeMatch = true;
            }


            if (auto_TimeMatch==true && vpos_video > 0)
            {
                auto_base = vpos_end - vpos_video;

            }


        }
        void vlc_videoSetup()
        {




            vpos_video = (int)vlcPlayer.Length/ 10;

          //  vpos_video = (int)vlcPlayer.Length / 10;

            currentLanguage = -1;



            for (int i = 0; i < vlcPlayer.Audio.Tracks.Count; i++)
            {

                if (vlcPlayer.Audio.Tracks.All.ElementAt(i).ID.Equals(vlcPlayer.Audio.Tracks.Current.ID))


                    currentLanguage = i;


            }


            lcount = vlcPlayer.Audio.Tracks.Count - 1;

            //  vlcPlayer.Audio.Tracks.Current = vlcPlayer.Audio.Tracks.All.ElementAt(2);

            //VLC_track.Maximum = vpos_video;

            setTrackMax(vpos_video);
            VLC_track.Minimum = 0;

            vlcPlayer.Audio.Volume = 50;



        }



        void Form1_StateChanged(object sender, Vlc.DotNet.Core.VlcMediaStateChangedEventArgs e)
        {
          switch(e.State.ToString()){
          
              case "Stopped":
                  time_counter = 0;
                  playedcomment = 0;
                  resetComment();

                  timerStop();


                  break;
              case "Playing":

                  timerStart();

                  break;
              case "Paused":

                  timerStop();

                  break;



              default:

                  timerStop();

                  break;
                
          
          }
        
        }


        void setTrackMax(int time)
        {

            if (this.InvokeRequired)
            {

                setTrack_t temp = new setTrack_t(setTrackMax);


                this.Invoke(temp, new object[] { time });



            }
            else
            {

                VLC_track.Maximum = time-10;
                VLC_track.Minimum = 0;
                VLC_track.TickFrequency = 1;
            }


        }


            void timerStart(){
                
                _isPlaying = true;
                replacetimer1_start();

                replacetimer2_start();
            
                replacetimer3_start();

                try
                {
                    cme1.Start();
                    cme2.Start();
                    cme3.Start();
                    cme4.Start();
                    cme5.Start();
                    cme6.Start();
                }
                catch (NullReferenceException) { };
            
            
        }
        void timerStop(){
        
                  
            _isPlaying = false;
                
            replacetimer1_stop();

            
            replacetimer2_stop();

            replacetimer3_stop();

            if (cme1 != null)
            {
                cme1.Stop();
            }
            if (cme2 != null)
            {
                cme2.Stop();
                cme3.Stop();
                cme4.Stop();
                cme5.Stop();
                cme6.Stop();
            }
        }
        void removeAllComments() {


            if (fm3 != null)
            {


      
                for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++)
                {

                    fm3.Controls.OfType<Label>().ElementAt(i).Dispose();

                    

                }
                fm3.Refresh();
            }

        
        }
        void Media_Player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            test_label.Text = Media_Player.playState.ToString();

            onLoadUp();

            switch(Media_Player.playState.ToString()){
            
            
                case "wmppsPlaying":

  //                  onLoadUp();

                    timerStart();
                    break;
                case "wmppsStopped":

                  
                    playedcomment = 0;

                    time_counter = 0;

  //                  onLoadUp();

                    timerStop();
                
  //                  resetComment();

                    removeAllComments();
                    break;
                
                case "wmppsReady":

   //                 onLoadUp();

                    break;

                default:

                    
                    timerStop();

                    break;
            
            }
          
        }
        String makeCCompactComponet(String comment) {
            String result = "";

            if (comment.Length / commentLimit > 0)
            {

                for (int j = 0; j < comment.Length / commentLimit; j++)
                {
                    //use commentLimit-1 instead commentLimit to avoid overflow
                    comment = comment.Insert((commentLimit - 1) * (j + 1), Environment.NewLine);


                }
            }

            result = comment;

            return result;
        
        
        }

        void makeCCompact(String comment) {


            if (comment.Length > commentLimit)
            {

                //if the comment is longer than limit and it contains newline
                if (comment.Contains(Environment.NewLine))
                {

                    string[] comments = comment.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    // now all the comments originally in newlines are now seperate

                    for (int i = 0; i < comments.Length; i++)
                    {

                        //iterate  through each strings

                        comments[i] = makeCCompactComponet(comments[i]);
                        createLabel(comments[i]);



                    }



                }
                else {

                    comment = makeCCompactComponet(comment);
                    createLabel(comment);
                
                }




            }
            else {


                createLabel(comment);
            
            }
            




        }

        bool core0 = false;
        bool core1 = false;
        bool core2 = false;
        bool core3 = false;


        void createLabel(String comment) {
            //instead of filtering out the comment make it more compact on the screen ?

          //  comment = comment.Insert(commentLimit, Environment.NewLine);


        //    if (comment.Length < commentLimit || comment.Contains(Environment.NewLine))
        //    {



                Label dm = new Label();

                //

                dm.Text = comment;
                dm.Name = comment;
                dm.TabIndex = 3;
                dm.Visible = true;
                dm.AutoSize = true;

                dm.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Bold);
                dm.ForeColor = userColor;
                dm.MouseClick += new MouseEventHandler(dm_MouseClick);


                Random ypos = new Random();

                if (!fullscreen)
                {
                    if (40 < fm3.ClientRectangle.Bottom - 80)
                    {
                        ycurrent = ypos.Next(40, fm3.ClientRectangle.Bottom - 80);
                    }
                    else
                    {

                        ycurrent = 40;
                    }
                }
                else {


                    ycurrent = ypos.Next(fm3.ClientRectangle.Top, fm3.ClientRectangle.Bottom - dm.Size.Height);
                
                }
                dm.Location = new Point(ClientRectangle.Right, ycurrent);


            
                if (commentmethod == 1)
                {

                    //basically splits the comments into two lists



                    switch (playedcomment % threadNumber) { 
                    
                        case 1:

                                comment_storage2.Add(dm);
                                fm3_1.Controls.Add(dm);

                            break;
                        case 2:
                            if (cme1 != null)
                            {
              
                                    cme1.setStorage.Add(dm);
                                    fm3_2.Controls.Add(dm);

                            }
                            break;
                        case 3:
                            if (cme2 != null)
                            {
     
                                    cme2.setStorage.Add(dm);
                                    fm3_3.Controls.Add(dm);
          
                               
                            }
                            break;
                        case 4:
                            cme3.setStorage.Add(dm);
                            fm3_4.Controls.Add(dm);
                            break;

                        case 5:

                            cme4.setStorage.Add(dm);

                            fm3_5.Controls.Add(dm);
                            break;

                        case 6:


                            cme5.setStorage.Add(dm);

                            fm3_6.Controls.Add(dm);
                            break;

                        case 7:


                            cme6.setStorage.Add(dm);

                            fm3_7.Controls.Add(dm);

                            break;

                        default:

                                comment_storage.Add(dm);
                                fm3.Controls.Add(dm);
      
                            break;
                    
                    }




                }
                dm.BringToFront();
                dm.Show();
         //   }
        
        }

        void safecontrol(Label dm) {

            if (this.InvokeRequired)
            {


            }
            else {

                fm3.Controls.Add(dm);
            }
        
        
        }

       public void Media_Player_ClickAction() {

           if (choose_player == 1)
           {
               test_label.Text = Media_Player.playState.ToString();


               switch (Media_Player.playState.ToString())
               {

                   case "wmppsStopped":
                       Media_Player.Ctlcontrols.play();



                       timerStart();

                       break;

                   case "wmppsPaused":
                       Media_Player.Ctlcontrols.play();

                       timerStart();

                       break;

                   case "wmppsPlaying":
                       Media_Player.Ctlcontrols.pause();

                       timerStop();
                       break;
                   case "wmppsReady":
                       //          onLoadUp();

                       Media_Player.Ctlcontrols.play();

                       timerStart();


                       break;

               }
           }
           else {

               test_label.Text = vlcPlayer.State.ToString();

               switch (vlcPlayer.State.ToString())
               {

                   case "Stopped":

                       test_label.Text = vlcPlayer.State.ToString();
                       vlcPlay_button.BackgroundImage = Properties.Resources.pause;
                       vlcPlayer.Play();

                     

                       timerStart();

                       break;

                   case "Paused":
                       

                       vlcPlay_button.BackgroundImage = Properties.Resources.pause;
                       vlcPlayer.Play();

                       test_label.Text = vlcPlayer.State.ToString();

                      timerStart();

                       break;

                   case "Playing":
                      

                       vlcPlay_button.BackgroundImage = Properties.Resources.start;
                       vlcPlayer.Pause();

                       test_label.Text = vlcPlayer.State.ToString();

                       timerStop();
                       break;

                   default:
                       if (vlcPlayer.GetCurrentMedia() != null) {

                           vlcPlay_button.BackgroundImage = Properties.Resources.pause;
                           vlcPlayer.Play();
                           vlcPlayer.Audio.Volume = 50;
                           test_label.Text = vlcPlayer.State.ToString();

                           timerStart();
                       
                       }


                       break;


               }
           
           
           }
        
        }
        void dm_MouseClick(object sender, MouseEventArgs e)
        {

            //check whick button is pressed on label , if right then bring it to front else stop/continue video
            switch(e.Button.ToString()){
          
              
            
            case "Right":
                    try
                    {
                        Label temp = (Label)sender;
                        temp.BringToFront();
                    }
                    catch (Exception) { 
                    
                    }
            break;

            default:

            Media_Player_ClickAction();
            
            break;

        }

            
                        
        }
        void resetLabelPos(Label l) {
            l.Location = new Point(ClientRectangle.Right, l.Location.Y);
        
        }
        

        void MoveLabel(Label l) {
            
            // where the DM start to hit
            int xstart = ClientRectangle.Right;
           // int xend = commentdestroy;

            int xend = 0 - l.Size.Width;
            //where the DM ends


            //adjest height position range from 0 to height using random to generate ?
            // not needed for now will use this when creating new TransparentLabels for DM
            // int ydown = this.Height;
            // int yup = 0;

            int initialx = l.Location.X;
            if (initialx > xend)
            {
                l.Location = new Point(initialx - move_distance, l.Location.Y);
              
            }
            else {
                //need to de comment this when the xml actually loads since it's going to add the comments on depending on the time counter real time


              //  comment_storage.Remove(l);
             //   remove_List.Add(l);
                //remove_List.AddLast(l);

          
                l.Dispose();
               
            
            }
        
        }

        void MoveLabel_thread(Label l) {

            // where the DM start to hit
            int xstart = ClientRectangle.Right;
            int xend = commentdestroy;
            //where the DM ends


            //adjest height position range from 0 to height using random to generate ?
            // not needed for now will use this when creating new TransparentLabels for DM
            // int ydown = this.Height;
            // int yup = 0;

            int initialx = l.Location.X;
            if (initialx > xend)
            {
             //   l.Location = new Point(initialx - move_distance, l.Location.Y);
                LabelLocation_thread(l, initialx - move_distance, l.Location.Y);
            }
            else
            {
                //need to de comment this when the xml actually loads since it's going to add the comments on depending on the time counter real time


                //  comment_storage.Remove(l);
                
                //remove_List.AddLast(l);
                l.Dispose();


            }
        
        
        }
        void MoveLabel_threadEX(Label l){

            if (l.InvokeRequired) {
                exmovelabel n = new exmovelabel(MoveLabel_threadEX);
                this.Invoke(n, new object[] { l });
            
            } else {

                // where the DM start to hit
                int xstart = ClientRectangle.Right;

             //   int xend = commentdestroy;

                int xend = 0 - l.Size.Width;
                //where the DM ends


                //adjest height position range from 0 to height using random to generate ?
                // not needed for now will use this when creating new TransparentLabels for DM
                // int ydown = this.Height;
                // int yup = 0;

                int initialx = l.Location.X;
                
                if (initialx > xend)
                {
                    l.Location = new Point(initialx - move_distance, l.Location.Y);
                }
                else
                {
                    //need to de comment this when the xml actually loads since it's going to add the comments on depending on the time counter real time


                    //  comment_storage.Remove(l);
                   // remove_List.AddLast(l);

                    l.Dispose();


                }
            
            }
        
        
        }
        void LabelLocation_thread(Label l,int x,int y) {

            if (l.InvokeRequired)
            {
                tmovelabel n = new tmovelabel(LabelLocation_thread);

                this.Invoke(n, new object[] { l,x,y });
            }
            else {
                l.Location = new Point(x, y);
            
            }
        
        }
        void resetComment() {
            foreach (Label l in fm3.Controls.OfType<Label>()) {

                resetLabelPos(l);
            }
        
        
        }
        void resetComment(List<Label> l) {

            foreach (Label comment in l) {
                resetLabelPos(comment);
            
            }
        
        }
         void Media_Player_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            Media_Player_ClickAction();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replacetimer1.Dispose();
            this.Close();

            

        }
        private void MediaPlayerClick(object sender, MouseEventArgs e) {
            test_label.Text = Media_Player.playState.ToString();
        
        }
        void setMedia_Multi(List<String[]> medias) {
            switch(choose_player){
                case 1: 
                    
                    
                    for (int i = 0; i < medias.Count(); i++)
            {
                Media_Playlist.appendItem(Media_Player.newMedia(medias.ElementAt(i)[0]));
                Media_List.Add(medias.ElementAt(i));

            }
            media_dir = medias.ElementAt(0)[0];
            Media_status.Text = "Playlist Set";
            Media_Player.currentPlaylist = Media_Playlist;

             Media_Player.Ctlcontrols.stop();


                break;

                case 2:
                    for (int i = 0; i < medias.Count(); i++)
            {
                
                Media_List.Add(medias.ElementAt(i));

            }
            media_dir = medias.ElementAt(0)[0];
            Media_status.Text = "Playlist Set";

            FileInfo temp = new FileInfo(medias.ElementAt(0)[0]);
            vlcPlayer.SetMedia(temp);
            vlc_track_time = -2;
            autoLoadByName(temp);
            //onLoadUp();
            // need to manulally create a method to play the list of files in media_linklist after the first loaded video done playing
            
            break;
                
            
            }

            
        }

        void autoLoadMlist(String[] mediadirs) {

            Media_List.Clear();

        int end = mediadirs[0].LastIndexOf("\\");
        String temp_dir = mediadirs[0].Substring(0, end);

        DirectoryInfo fdir = new DirectoryInfo(temp_dir);

          //if the file doesn't contain the . extension (meaning it's most likely dled by this player)
        // or if the current file dir is the same as the dl file dir
            //loads all files dispite file extension type
        if (!mediadirs[1].Contains(".")||mediadirs[1].Contains(".mp")||fdir.FullName==current_dir_url)
        {
            FileInfo[] file = fdir.GetFiles();
            for (int i = 0; i < file.Count(); i++)
            {

                String[] ml = { file[i].FullName, file[i].Name };

                //only load if it's not xml file do this to avoid reading xml in wrong place
                if(!file[i].Name.Contains(".xml")){
                Media_List.Add(ml);
                }

                if (fm2 != null)
                {

                    fm2.setMListBox.Items.Add(ml[1]);

                }

            }

        }
        else
        {

            end = mediadirs[1].LastIndexOf(".");

            String extension = mediadirs[1].Substring(end, mediadirs[1].Length - end);

            FileInfo[] file = fdir.GetFiles("*" + extension);

            for (int i = 0; i < file.Count(); i++)
            {

                String[] ml = { file[i].FullName, file[i].Name };

                Media_List.Add(ml);

                if (fm2 != null)
                {

                    fm2.setMListBox.Items.Add(ml[1]);

                }

            }
        }        
        
        }

        //loaded after setmedia(single or multi all applied) && loaded after set dm
        //for this list instead of clearing everything maybe just checking duplicates will be fine ?
       
        void autoLoadDMlist(String[] dmdirs) {

            int end = dmdirs[0].LastIndexOf("\\");
            String temp_dir = dmdirs[0].Substring(0, end);

            DirectoryInfo ddir = new DirectoryInfo(temp_dir);

            //no need to check for extensions here since all are reading .xml. . . for now

            String extension = ".xml";

            FileInfo[] file = ddir.GetFiles("*" + extension);

            //now all the files under the dir with .xml are now in files array

            for (int i = 0; i < file.Count(); i++) {

                String[] results = { file[i].FullName, file[i].Name };
                bool exists=false;

                for (int l = 0; l < FullDM_List.Count(); l++) {
                  //  if (FullDM_List.Count > 0)
                 //   {
                        if (FullDM_List.ElementAt(l)[0].Equals(results[0]))
                        {
                            exists = true;

                        }
                //    }
                
                }


                    if (!exists)
                    {
                        FullDM_List.Add(results);

                        //if the list menu windows is left opened when video/comment is loaded through dropdown menu maynot be need ? 
                        if (fm2 != null)
                        {
                            fm2.setFullDMBox.Items.Add(results[1]);
                        }

                    }
            
            
            }

        
        
        
        
        }

        void setMedia_Single(List<String[]> medias)
        {

            if (medias != null)
            {
                media_dir = medias.ElementAt(0)[0];
                Media_status.Text = "Media Set";
                Media_List.Add(medias.ElementAt(0));



                if (fm3 != null)
                {

                    ///////fm3.Owner = this;

                }

                if (autoMlist == true) {

                    autoLoadMlist(medias.ElementAt(0));
                
                }

            }
            else
            {
                Media_status.Text = "No Media";
            }
            //   Media_Player.settings.autoStart=false;

            switch(choose_player){
                case 1:
                Media_Player.URL = media_dir;
            Media_Player.Ctlcontrols.stop();

            break;
            case 2:
            if (media_dir != null)
            {
                FileInfo temp = new FileInfo(media_dir);

                vlcPlayer.SetMedia(temp);
                vlc_track_time = -2;
                autoLoadByName(temp);
            }
           // onLoadUp();
            break;
        }

            if (fm3 != null)
            {

                ///////fm3.Owner = this;
            }
            


        }


        void PlayMedia(List<String[]> medias) {

            bool multi_media;
            if (medias != null && medias.Count > 1)
            {
                //multiple media file is selected;
                multi_media = true;


            }
            else
            {

                multi_media = false;
            }

            if (multi_media)
            {

                setMedia_Multi(medias);

            }
            else
            {

                setMedia_Single(medias);


            }
            
           
            _first_load = false;
        
        }

        void setMedia() {

            if (fm3 != null)
            {
                ///////fm3.Owner = this;

            }


            List<String[]> medias = setFile(media);

            if (medias != null)
            {
                if (vlcPlayer.IsPlaying) {
                    vlcPlayer.Stop();
                    comment2.Clear();
                    comment.Clear();
                    _duplicates = 0;
                
                }
                PlayMedia(medias);
                autoLoadDMlist(medias.ElementAt(0));
            }

            
        
        
        }
        private void setDMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMedia();
        }
        void print3(String s) {

            Video_comment.Text = s;
        
        }
        private void readXML(String danmoku_dir)
        {
            String[] temp_comment = new String[2];

            dm_comment = new XmlTextReader(danmoku_dir);

            while (dm_comment.Read())
            {

                switch (dm_comment.NodeType)
                {

                    case XmlNodeType.Element:



                        while (dm_comment.MoveToNextAttribute())
                        {

                            //get the time value if the attribute is vpos
                            if (dm_comment.Name == "vpos")
                            {
                                temp_comment[0] = dm_comment.Value;

                            }

                        }
                        break;

                    case XmlNodeType.Text:
                        temp_comment[1] = dm_comment.Value;
                        comment.Add(temp_comment);
                        int vpost = Int32.Parse(temp_comment[0]);

                        //this will ends with the final vpos in the xml files
                        if (vpost > vpos_end) {

                            vpos_end = vpost;
                        }


                        if (comment2.ContainsKey(vpost))
                        {

                            String tempc = comment2[vpost] + Environment.NewLine + temp_comment[1];
                            _duplicates++;
                            comment2.Remove(vpost);
                            comment2.Add(vpost, tempc);


                        }
                        else
                        {
                            comment2.Add(vpost, temp_comment[1]);
                        }
                        //temp_comment = new String[2];


                        break;

                    case XmlNodeType.EndElement:
                        if (dm_comment.Name == "</chat>")
                        {

                            temp_comment = new String[2];

                        }

                        break;



                }
            }
        
        
            //every time xml loads
            

            _first_load = false;
  //          onLoadUp();
        }

        void setDM_Multi() { 
        
        
        
        
}

        void setDM_Menu() {

            if (multi_dm_mode == false)
            {
                comment.Clear();
                comment2.Clear();
                _duplicates = 0;

                //clears all the dm that is current loaded
                DM_List.Clear();

            }
            if (fm3 != null)
            {
                try
                {
                    this.Owner = fm3;
                    /////////fm3.Owner = this;
                }
                catch (Exception) { }


            }


            List<String[]> danmokus = setFile(danmoku);

            if (danmokus != null)
            {
                if (danmokus.Count > 1)
                {

                    multi_dm_mode = true;
                    setDMsToolStripMenuItem.Text = "Set DMs" + " Multi On ";
                }





                if (danmokus.Count == 1)
                {
                    danmoku_dir = danmokus.ElementAt(0)[0];
                    Danmoku_status.Text = "DM Set";
                    if (!DM_List.Contains(danmokus.ElementAt(0)))
                    {
                        DM_List.Add(danmokus.ElementAt(0));

                    }
                    //reader.name is the name of the element/attribute
                    //reader.value is the value of the attribute/text

                    readXML(danmoku_dir);

                    danmoku_dir = null;
                    Danmoku_status.Text = "DM Ready";

                }
                else
                {

                    for (int i = 0; i < danmokus.Count(); i++)
                    {


                        //add only if it's not in the list to prevent dulplicate from being loaded
                        if (!DM_List.Contains(danmokus.ElementAt(i)))
                        {
                            danmoku_dir = danmokus.ElementAt(i)[0];
                            DM_List.Add(danmokus.ElementAt(i));
                            readXML(danmoku_dir);

                        }


                    }

                    danmoku_dir = null;
                    Danmoku_status.Text = "DM Ready";


                }
                if (fm3 == null)
                {
                    commentWindowSetup();
                }
                else
                {
                    ///////fm3.Owner = this;

                }

                autoLoadDMlist(danmokus.ElementAt(0));

            }
            else
            {
                Danmoku_status.Text = "No DM";
                if (fm3 != null)
                {
                    ///////fm3.Owner = this;
                }
            }
        
        
        }
        private void setDMToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            setDM_Menu();
        }

        void commentWindowSetup() {

            fm3 = new Comment_window();
            fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
            fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
            fm3.MouseClick += new MouseEventHandler(dm_MouseClick);
            fm3.MouseDoubleClick += new MouseEventHandler(fm3_MouseDoubleClick);

            fm3.KeyUp += new KeyEventHandler(fm3_KeyUp);
            fm3.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            

            fm3_1 = new Comment_window();
            fm3_2 = new Comment_window();
            fm3_3 = new Comment_window();

            fm3_4 = new Comment_window();
            fm3_5 = new Comment_window();
            fm3_6 = new Comment_window();

            fm3_7 = new Comment_window();

                fm3_1.Size = fm3.Size;
                fm3_2.Size = fm3.Size;
                fm3_3.Size = fm3.Size;
                fm3_4.Size = fm3.Size;
                fm3_5.Size = fm3.Size;
                fm3_6.Size = fm3.Size;
                fm3_7.Size = fm3.Size;

                fm3_1.Location = fm3.Location;
                fm3_2.Location = fm3.Location;
                fm3_3.Location = fm3.Location;
                fm3_4.Location = fm3.Location;
                fm3_5.Location = fm3.Location;
                fm3_6.Location = fm3.Location;
                fm3_7.Location = fm3.Location;
            
            
            
                fm3_1.Show();
                fm3_2.Show();
                fm3_3.Show();
                fm3_4.Show();
                fm3_5.Show();
                fm3_6.Show();
                fm3_7.Show();



                fm3_1.Owner = this;
                fm3_2.Owner = fm3_1;
                fm3_3.Owner = fm3_2;
                fm3_4.Owner = fm3_3;
                fm3_5.Owner = fm3_4;
                fm3_6.Owner = fm3_5;
                fm3_7.Owner = fm3_6;
                fm3.Owner = fm3_7;



               // ///////fm3.Owner = this;
        }

        void fm3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FullScreenSwitch();
        }

        void fm3_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpActions(e);
        }
        //modified from String[] to List so multiple files can be selected at once.

        private List<String[]> setFile(OpenFileDialog ofd){
            ofd.Multiselect = true;
        
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

                int end = ofd.FileNames.ElementAt(0).LastIndexOf("\\");
                String dir = ofd.FileNames.ElementAt(0).Substring(0, end);
                ofd.InitialDirectory = dir;
                List<String[]> temp = new List<String[]>();
                for (int i = 0; i < ofd.FileNames.Count(); i++)
                {
                    String[] result = { ofd.FileNames.ElementAt(i), ofd.SafeFileNames.ElementAt(i) };
                    temp.Add(result);


                }
                return temp;

            
            }else{
            
                return null;
            }
        
        
        }


        void change_timeoffset(int s) {
            time_offset = s;
        
        }
        void change_move_distance(int s) {

            _distance = s;
        
        }

        void print(String s) {

            debugtab.Text = s;
        
        }
        void print2(String s) {

            debugtab2.Text = s;
        }

        void autoSetVolumn() {
            if (vlcPlayer.Audio.Volume != vVolume)
            {
                vlcPlayer.Audio.Volume = vVolume;
                sound_trackbar.Value = vVolume;
            };
        
        }

        void autoSetTrackTime() {


            if (vlc_track_time != -2)
            {

                vlcPlayer.Time = vlc_track_time;
                time_counter = vlc_track_time / 10;

                vlc_track_time = -2;

            }
        
        }
        int comment_time;

        void makeComment() {
            
            //sets the player volume if it's not equal to the current user volume
            autoSetVolumn();

            //component for track bar, checks if the track value has changed if yes then set
            autoSetTrackTime();

            //time_counter = (int)(Media_Player.Ctlcontrols.currentPosition * 100);



            //for checking mouse location not in use for now
            /*
            if (System.Windows.Forms.Cursor.Position==mouseLocation) {

                mousemoving = false;
            }
            */

 





            if (choose_player != 1)
            {

                try
                {
                    //checks if the video reaches the end of the video
                    manual_checkend((int)vlcPlayer.Length / 10, time_counter);
                }
                catch (NullReferenceException) { Console.WriteLine("in make comment"); }
            }

           

            
            try
            {
                switch(choose_player){
                    case 1:
                        if (auto_TimeMatch == false)
                {
                    comment_time = (int)(Media_Player.Ctlcontrols.currentPosition * 100) + time_offset;
                }
                else {

                    comment_time = (int)(Media_Player.Ctlcontrols.currentPosition * 100) + auto_base+offset_auto;
                
                }

                        break;


                    default:

                       
                        //if auto match is on then offset the comments by the comment's largest vpos-video time
                        if (auto_TimeMatch == false)
                        {


                            //second important element in the engine, basiclly time_counter with user offset to get the comments to the right video position
                            comment_time = time_counter+ time_offset;
                        }
                        else
                        {

                            comment_time = time_counter + auto_base + offset_auto; 

                        }
                    break;
            }

            }
            catch (Exception) { comment_time = 0; }



            print(showTime(((int)(vlcPlayer.Time)) ) + "/" + comment_time + "/" + time_counter);


            if (fm3 != null)
            {
                if (commentmethod != 1)
                {
                    print2(showTime((int)vlcPlayer.Length) + "  L: " +fm3.Controls.OfType<Label>().Count()+ " " + (playedcomment + _duplicates) + "/" + (comment.Count()));
                }
                else {
       //             print2(showTime((int)vlcPlayer.Length) + "L: " + (comment_storage.Count+comment_storage2.Count) + "  L1: " + (comment_storage.Count) + " L2: " + comment_storage2.Count  + " " + (playedcomment + _duplicates) + "/" + (comment.Count()));
      
              //      print2(showTime((int)vlcPlayer.Length) + "L: "+(fm3.Controls.OfType<Label>().Count())+ "  L1: " + (comment_storage.Count)+ " L2: "+comment_storage2.Count+" L3 "+cme1.setStorage.Count+" L4: "+cme2.setStorage.Count + " " + (playedcomment + _duplicates) + "/" + (comment.Count()));
                    print2(showTime((int)vlcPlayer.Length) + "L: " + (cme1.setStorage.Count + cme2.setStorage.Count + cme3.setStorage.Count + cme4.setStorage.Count + cme5.setStorage.Count + cme6.setStorage.Count + comment_storage.Count + comment_storage2.Count) + "  L1: " + (comment_storage.Count) + " L2: " + comment_storage2.Count + " L3 " + cme1.setStorage.Count + " L4: " + cme2.setStorage.Count + " " + (playedcomment + _duplicates) + "/" + (comment.Count()));
      
                }
                }
            else {

                print2(playedcomment + "/" + (comment.Count() + _duplicates));
            
            }

            //only runs if there's comments avaliable;
            if (comment2.Count > 0)
            {
                addComment(comment_time, comment2);

            }
            //super important the main clock for the comment engine;
            time_counter++;
        
        }

        String showTime(int time) {

            String result="";

            int totalsec = time / 1000;
            int sec = totalsec % 60;
            int min,hour;

            if (totalsec / 60 < 60)
            {
                min = totalsec / 60;
                hour = 0;
            }
            else {

                min = (totalsec / 60) % 60;
                hour = (totalsec / 60) / 60;
            }

            result = String.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
            return result;
        }
        void moveComment() {


            if (time_counter % speed_control == 0)
            {
 /*               foreach (Label l in Controls.OfType<Label>())
                {

                    MoveLabel(l);


                }
                
*/

                    switch(commentmethod){


                        case 1:
                            List<Label> removetemp = new List<Label>();
                            for (int i = 0; i < comment_storage.Count; i++) {


                                MoveLabel(comment_storage.ElementAt(i));

                                if (comment_storage.ElementAt(i).IsDisposed) {

                                    removetemp.Add(comment_storage.ElementAt(i));
                                }
                            
                            }

                            if (removetemp.Count > 0)
                            {
                                for (int i = 0; i < removetemp.Count; i++)
                                {

                                    comment_storage.Remove(removetemp.ElementAt(i));

                            
                                
                                }

                            }
                            //clear removelist here ?    but it should be empty by the end of the code
                           

                                break;
                 
                        
                    default:
                        for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++)
                    {


                        MoveLabel(fm3.Controls.OfType<Label>().ElementAt(i));



                    }

                    break;

                
                
                }

                
                
                
                
                /*
                foreach (Label l in remove_List)
                {
                    Controls.Remove(l);
                  //  comment_storage.Remove(l);
                }

                remove_List.Clear();
                  */
            }
        
        }

        List<Label> comment_storage2 = new List<Label>();
        void moveComment_thread2()
        {
            //   if (time_counter % speed_control == 0 && fm3!=null)
            if (fm3 != null)
            {
                //not sure how fast this is compare to actual label need to do test on this
                switch (commentmethod)
                {


                    case 1:
                        try
                        {

                            List<Label> removetemp2 = new List<Label>();


                            for (int i = 0; i < comment_storage2.Count; i++)
                            {


                                MoveLabel_threadEX(comment_storage2.ElementAt(i));

                                if (comment_storage2.ElementAt(i).IsDisposed)
                                {
                                    removetemp2.Add(comment_storage2.ElementAt(i));
                                }

                            }

                            if (removetemp2.Count > 0)
                            {
                                for (int i = 0; i < removetemp2.Count; i++)
                                {

                                    comment_storage2.Remove(removetemp2.ElementAt(i));



                                }


                            }
                        }
                        catch (Exception) { };
                        //clear removelist here ?    but it should be empty by the end of the code


                        break;


                    default:
                        for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++)
                        {


                            MoveLabel_threadEX(fm3.Controls.OfType<Label>().ElementAt(i));



                        }

                        break;



                }

            }


        }

        void moveComment_thread() {
         //   if (time_counter % speed_control == 0 && fm3!=null)
           if(fm3!=null)
           {
               //not sure how fast this is compare to actual label need to do test on this
                               switch(commentmethod){


                        case 1:
                                       try
                                       {
                                           List<Label> removetemp = new List<Label>();
                               

                                           for (int i = 0; i < comment_storage.Count; i++)
                                           {


                                               MoveLabel_threadEX(comment_storage.ElementAt(i));

                                               if (comment_storage.ElementAt(i).IsDisposed)
                                               {
                                                   removetemp.Add(comment_storage.ElementAt(i));
                                               }

                                           }

                                  
                                           
                                               for (int i = 0; i < removetemp.Count; i++)
                                               {

                                                   comment_storage.Remove(removetemp.ElementAt(i));



                                               }


                                           


                                       }
                                       catch (Exception) { };
                            //clear removelist here ?    but it should be empty by the end of the code
                           

                                break;
                 
                        
                    default:
                        for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++)
                    {


                        MoveLabel_threadEX(fm3.Controls.OfType<Label>().ElementAt(i));



                    }

                    break;

                
                
                }

            }
            
        
        }
        void commentEngine() {

            if(sommentswitch ==true){

                makeComment();

                //moveComment();

            }
             
        
        
        }

        void commentEngine_thread() {

            if (this.InvokeRequired)
            {

                commentEnginecomp c = new commentEnginecomp(commentEngine_thread);
                try
                {
                    this.Invoke(c, new object[] { });

                }
                catch (Exception) { }


            }
            else {

                if (sommentswitch == true)
                {

                    makeComment();

                    //moveComment();

                }
             
        
            
            }
        
        
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
       //   commentEngine(); 
            if (!threading_mode)
            {
                   moveComment();

            }
            
        }
      void  removebyDel_DM(List<String[]> dm,ListBox mbox,bool readxml){



          for (int i = 0; i < dm.Count(); i++)
                        {
                            foreach (object l in mbox.SelectedItems)
                            {
                                if (dm.ElementAt(i)[1].Equals(l.ToString()))
                                {

                                    dm.Remove(dm.ElementAt(i));
                                }
                            }

                        }
                        if (mbox.SelectedItems.Count > 1) {

                            List<object> removel = new List<object>();
                            foreach (object i in mbox.SelectedItems) {

                                removel.Add(i);
                            
                            }
                            for (int i = 0; i < removel.Count; i++) {

                                mbox.Items.Remove(removel.ElementAt(i));
                            }

                        }
                        else
                        {
                            mbox.Items.Remove(mbox.SelectedItem);
                        }

                 // after comment is removed  from the listbox and the list
    
                    if (readxml)
                    {
                        comment2.Clear();
                        comment.Clear();
                        _duplicates = 0;

                        for (int i = 0; i < dm.Count(); i++)
                        {
                            readXML(dm.ElementAt(i)[0]);


                        }
                    }
}
        private void Media_DM_menu_Click(object sender, EventArgs e)
        {
            List_menu_setup();
        }

        void DMMlistsetup() {
            fm2 = new Form2();

            if (fm3 != null)
            {
                fm2.Owner = fm3;


            }
            fm2.Disposed += new EventHandler(fm2_Disposed);


            if (DM_List != null)
            {
                fm2.setDMList = DM_List;
                //   fm2.setDMList(DM_List);
            }
            if (Media_List != null)
            {
                fm2.setMediaList = Media_List;
                //fm2.setMediaList(Media_List);
            }
            if (FullDM_List != null)
            {
                fm2.setFullDMList = FullDM_List;
            }
            fm2.setMListBox.DoubleClick += new EventHandler(setMListBox_DoubleClick);
            fm2.setDMListBox.DoubleClick += new EventHandler(setDMListBox_DoubleClick);
            fm2.setFullDMBox.DoubleClick += new EventHandler(setFullDMBox_DoubleClick);
            fm2.setMListBox.KeyUp += new KeyEventHandler(setMListBox_KeyUp);
            fm2.setDMListBox.KeyUp += new KeyEventHandler(setDMListBox_KeyUp);
            fm2.setFullDMBox.KeyUp += new KeyEventHandler(setFullDMBox_KeyUp);


             if (vlcPlayer.GetCurrentMedia() != null) {
                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                FileInfo nfile = new FileInfo(nuri.LocalPath);
                selectPlaying_box(nfile.Name);
            }

            fm2.Show();

        
        }

        void setFullDMBox_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            ListBox mbox = (ListBox)sender;
            switch (e.KeyValue) { 
            
                case 46:
                    removebyDel_DM(FullDM_List, mbox,false);
                    break;
            
            
            }
        }

        void setDMListBox_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            //bascally do exact the same thing as mlist key up

            ListBox mbox = (ListBox)sender;

            switch (e.KeyValue) { 
            

            
                case 46:



                    removebyDel_DM(DM_List,mbox,true);

                        break;
            
            
            }
        }

        void List_menu_setup() {

            if (fm2 == null)
            {
                DMMlistsetup();

            }
            else {

                fm2.Dispose();
            }
        
        
        
        
        
        
        }

        void setMListBox_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            // set del key action here if del is pressed with an item selected then remove the selected item from the Media List and media list box

            //also probably add a function to play the media selected if enter key is pressed instead of double clicking it ?

            ListBox mbox = (ListBox)sender;



            switch (e.KeyValue) { 
            
            
                case 13:
                    //when enter key is let go
                    runSelectedVido(sender,e);
                    break;


                case 46:
                    //when del key is let go
        
                        for (int i = 0; i < Media_List.Count(); i++)
                        {
                            foreach (object l in mbox.SelectedItems)
                            {
                                if (Media_List.ElementAt(i)[1].Equals(l.ToString()))
                                {

                                    Media_List.Remove(Media_List.ElementAt(i));
                                }
                            }

                        }
                        if (mbox.SelectedItems.Count > 1) {

                            List<object> removel = new List<object>();
                            foreach (object i in mbox.SelectedItems) {

                                removel.Add(i);
                            
                            }
                            for (int i = 0; i < removel.Count; i++) {

                                mbox.Items.Remove(removel.ElementAt(i));
                            }

                        }
                        else
                        {
                            mbox.Items.Remove(mbox.SelectedItem);
                        }

                 

                    break;
            
            
            
            
            
            }
        }



        void setFullDMBox_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
            //basiclly the same build as Mlistbox double click, but do it with dm
            //goes into the current media directory and fetch all .xml files avaliable add to the full dmlistbox and list
            //check if the item clicked is already in the DMlistbox if not then 
            //add item to the left DMlistbox and load load comments with readXML
            //if it's already in there then do nothing !!!

            ListBox cbox = (ListBox)sender;
            bool has = false;
            for (int i = 0; i < DM_List.Count(); i++) {

                
                if (DM_List.ElementAt(i)[1].Equals(cbox.SelectedItem.ToString())) {

                    has = true;
                
                }
            
            
            }
            if (!has) {

                for (int i = 0; i < FullDM_List.Count(); i++) {

                    if (cbox.SelectedItem.ToString().Equals(FullDM_List.ElementAt(i)[1])) {

                        DM_List.Add(FullDM_List.ElementAt(i));
                        fm2.setDMListBox.Items.Add(cbox.SelectedItem.ToString());
                        readXML(FullDM_List.ElementAt(i)[0]);
                    
                    }
                
                }
            
            }
            if (fm3 == null) {

                commentWindowSetup();
                fm2.Owner = fm3;
                fm2.Dispose();
                DMMlistsetup();

            }

        }

        void removeDMitem(object sender, EventArgs e) {

            //throw new NotImplementedException();

            //Unload the DMs 
            // remove the selected dm from the listbox, the list and then do a reload all dms

            ListBox dmbox = (ListBox)sender;


            comment2.Clear();
            comment.Clear();
            _duplicates = 0;

            //loop through to find the selected item if found then remove if not then load to comment2 using read xml
            try
            {
                for (int i = 0; i < DM_List.Count(); i++)
                {

                    if (DM_List.ElementAt(i)[1].Equals(dmbox.SelectedItem.ToString()))
                    {

                        DM_List.Remove(DM_List.ElementAt(i));
                    }
                    else
                    {

                        readXML(DM_List.ElementAt(i)[0]);
                    }

                }
                dmbox.Items.Remove(dmbox.SelectedItem);

            }
            catch (NullReferenceException) { Console.WriteLine("in removeDMitem"); };
        
        }

        void setDMListBox_DoubleClick(object sender, EventArgs e)
        {
            removeDMitem(sender, e);

        }
        //only use when trying menual double click
        String mdclick;

        void runSelectedVido(object sender, EventArgs e)
        {

            //throw new NotImplementedException();

            //implement set media by click method here
            String result = "";

            ListBox mtemp = (ListBox)sender;

            if (vlcPlayer.IsPlaying)
            {

                vlcPlayer.Stop();
            }

            for (int i = 0; i < Media_List.Count(); i++)
            {

                if (Media_List.ElementAt(i)[1].Equals(mtemp.SelectedItem.ToString()))
                {

                    result = Media_List.ElementAt(i)[0];

                }


            }
            if (result != "")
            {
                FileInfo newMedia = new FileInfo(result);
                String current = vlcPlayer.GetCurrentMedia().Mrl;
                vlcPlayer.SetMedia(newMedia);
                vlc_track_time = -2;

                vlcPlayer.Play();

                //unload the loaded dm files, to avoid using the wrong dm on different media files?

                if (!current.Equals(vlcPlayer.GetCurrentMedia().Mrl))
                {
                    comment2.Clear();
                    comment.Clear();
                    DM_List.Clear();
                    _duplicates = 0;

                    if (fm2 != null)
                    {
                        fm2.setDMListBox.Items.Clear();

                    }

                }

                autoLoadByName(newMedia);
                if (fm3 == null) {

                    commentWindowSetup();
                    fm2.Owner = fm3;
                    fm2.Dispose();
                    DMMlistsetup();
                }

                this.Text = "DM Player " + newMedia.Name;
            }
            /*

            //this is manual double click code
            if (mdclick == mtemp.SelectedValue) { 
            
            
            
            }
            
            
            mdclick = mtemp.SelectedValue.ToString();

            */


        }
        void setMListBox_DoubleClick(object sender, EventArgs e)
        {

            runSelectedVido(sender,e);

        }
        void fm2_Disposed(object sender, EventArgs e)
        {
            if (fm3 != null)
            {

                ///////fm3.Owner = this;

                
            }
            fm2 = null;
        }
       
        
        private void openMeidaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setDMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // loads multiple XML files here
            if (multi_dm_mode == false)
            {
                multi_dm_mode = true;
                setDMsToolStripMenuItem.Text = "Set DMs" + " Multi On";
            }
            else {
                multi_dm_mode = false;
                setDMsToolStripMenuItem.Text = "Set DMs" + "Multi Off";
                
            }

        }

        private void Settings_menu_Click(object sender, EventArgs e)
        {
            if (fm4 == null)
            {
                setting_setup();
            }
            else {
                fm4.Dispose();
            }

        }

        void setting_setup() {

            if (fm4 == null)
            {
                // open up a new form for displaying settings options

                if (choose_player == 1)
                {
                    var tconsol = ((WMPLib.IWMPControls3)Media_Player.Ctlcontrols);

                    currentLanguage = tconsol.currentAudioLanguageIndex;

                    lcount = tconsol.audioLanguageCount;
                    if (lcount > 0)
                    {

                        audioinfo = tconsol.getAudioLanguageDescription(currentLanguage);

                    }
                }
                else {



                    lcount = vlcPlayer.Audio.Tracks.Count-1;

                    for (int i = 0; i < lcount; i++) {

                        if (vlcPlayer.Audio.Tracks.Current.ID.Equals(vlcPlayer.Audio.Tracks.All.ElementAt(i))) {

                            currentLanguage = i;
                        
                        }
                    
                    
                    }
                    try
                    {
                        audioinfo = vlcPlayer.Audio.Tracks.Current.Name;
                       
                    }
                    catch (NullReferenceException) { Console.Write("in void settingup"); }
                
                
                }
                fm4 = new Settings();
                fm4.Disposed += new EventHandler(fm4_Disposed);
                fm4.getapply.Click += new EventHandler(fm4_apply);
                fm4.getconfirm.Click += new EventHandler(getconfirm_Click);
                fm4.getcancel.Click += new EventHandler(getcancel_Click);
                fm4.getdefault.Click += new EventHandler(getdefault_Click);
                fm4.getAudio_down.Click += new EventHandler(getAudio_down_Click);
                fm4.getAudio_up.Click += new EventHandler(getAudio_up_Click);
                fm4.auto_mode_check.CheckStateChanged += new EventHandler(auto_mode_check_CheckStateChanged);
                this.loadAll();
                if (fm3 != null)
                {
                    fm4.Owner = fm3;
                }
                fm4.Show();

            }
        
        }

        void auto_mode_check_CheckStateChanged(object sender, EventArgs e)
        {
            auto_TimeMatch = fm4.auto_mode_check.Checked;
            CheckBox temp = (CheckBox)sender;

            if (temp.Checked == true &&vpos_video>0 &&vpos_end>0) {
                auto_base = vpos_end - vpos_video;
            
            }
            loadCheck();
        }

        void getAudio_up_Click(object sender, EventArgs e)
        {
            if (lcount > currentLanguage) {

                if (choose_player == 1)
                {
                    var tconsol = ((WMPLib.IWMPControls3)Media_Player.Ctlcontrols);
                    tconsol.currentAudioLanguageIndex = currentLanguage + 1;
                    currentLanguage++;
                    audioinfo = tconsol.getAudioLanguageDescription((Int32)currentLanguage);
                }
                else {
                    currentLanguage++;
                    vlcPlayer.Audio.Tracks.Current=vlcPlayer.Audio.Tracks.All.ElementAt(currentLanguage);

                    audioinfo = vlcPlayer.Audio.Tracks.Current.Name;

                }
                loadAudio();
            }
        }

        void getAudio_down_Click(object sender, EventArgs e)
        {
            if ( currentLanguage>1)
            {
                if (choose_player == 1)
                {
                    var tconsol = ((WMPLib.IWMPControls3)Media_Player.Ctlcontrols);
                    tconsol.currentAudioLanguageIndex = currentLanguage - 1;
                    currentLanguage--;
                    audioinfo = tconsol.getAudioLanguageDescription((Int32)currentLanguage);

                }
                else {
                    currentLanguage--;
                    vlcPlayer.Audio.Tracks.Current = vlcPlayer.Audio.Tracks.All.ElementAt(currentLanguage);
                    

                    audioinfo = vlcPlayer.Audio.Tracks.Current.Name;
                
                }
                loadAudio();
            }
        }

        void getdefault_Click(object sender, EventArgs e)
        {
            loadAll();
        }

        void getcancel_Click(object sender, EventArgs e)
        {


                fm4.Dispose();
                fm4 = null;
           
        }

        void getconfirm_Click(object sender, EventArgs e)
        {
            applyAll();

            fm4.Dispose();
            fm4 = null;


        }
        void loadAll() {
            if (fm4 != null) {
                switch (this.sommentswitch) { 
                
                    case true:
                        fm4.select_commentswitch.SelectedItem = "ON";

                        break;


                    case false:
                        fm4.select_commentswitch.SelectedItem = "OFF";

                        break;
                
                }
                fm4.auto_mode_check.Checked = this.auto_TimeMatch;

                loadCheck();
                fm4.Cspeed.Text = this._distance.ToString();
                fm4.Cend.Text = this.commentdestroy.ToString();
                fm4.setCommentLimit.Text = commentLimit.ToString();
                loadAudio();
                
            
            }
        
        
        }
        void loadAudio() {

            fm4.getCurrentAudio.Text = this.currentLanguage.ToString();
            fm4.getTotalAudio.Text = this.lcount.ToString();
            fm4.getAudioInfo.Text = this.audioinfo;
            print3("Video Length: " + vpos_video.ToString() + "/" + "Comment time: " + vpos_end + "  CL: " + currentLanguage.ToString() + " total: " + lcount.ToString());
                
        
        }
        void loadCheck() {

            fm4.auto_mode_check.Checked = this.auto_TimeMatch;

            if (auto_TimeMatch==true)
            {
                fm4.timeoffset.Text = offset_auto.ToString();

            }
            else
            {

                fm4.timeoffset.Text = time_offset.ToString();
            }
        
        }
        void applyAll() {
            if (fm4 != null) {

                try
                {
                    switch (fm4.select_commentswitch.SelectedItem.ToString()) { 
                    
                        case "ON":
                            this.sommentswitch = true;
                            break;

                        case "OFF":
                            this.sommentswitch = false;
                            break;
                    
                    
                    
                    }


                    this.auto_TimeMatch = fm4.auto_mode_check.Checked;
                    if (fm4.auto_mode_check.Checked==true)
                    {
                        this.auto_TimeMatch = true;
                        this.offset_auto = Int32.Parse(fm4.timeoffset.Text);
                    
                    }
                    else
                    {
                        this.auto_TimeMatch = false;
                        this.time_offset = Int32.Parse(fm4.timeoffset.Text);

                    }
                    this._distance = Int32.Parse(fm4.Cspeed.Text);
                    this.move_distance = Int32.Parse(fm4.Cspeed.Text);

                    this.commentdestroy = Int32.Parse(fm4.Cend.Text);
                    commentLimit = Int32.Parse(fm4.setCommentLimit.Text);
                    


                }
                catch (Exception) { }
            
            
            
            }
        }
        void fm4_apply(object sender, EventArgs e) {
            applyAll();
        
        }
        
        void fm4_Disposed(object sender, EventArgs e)
        {
            // save all the changes here
            fm4.Dispose();
            fm4 = null;
        }



        void onLoadUp()
        {

            if (comment2.Count > 0 && Media_List.Count > 0)
            {



                if (_first_load == false)
                {
                    playedcomment = 0;

                    time_counter = 0;

                    autoTimesetup();

                    if (choose_player == 1)
                    {
                        this.Text = "DM PLayer   :" + Media_Player.Ctlcontrols.currentItem.name;
                        _first_load = true;
                    }
                    else
                    {
                        Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                        FileInfo nfile = new FileInfo(nuri.LocalPath);
                        setDMtitle(nfile.Name);
                        _first_load = true;
                    }



                }



            }
        }
        void setDMtitle(String s) {

            if (InvokeRequired)
            {
                setString temp = new setString(setDMtitle);

                this.Invoke(temp, new object[] {s });

            }
            else {

                this.Text = "DM PLayer   :" + s;
            }
        
        
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            VLC_test test = new VLC_test();
            test.Show();
        }

        private void vlcPlay_button_Click(object sender, EventArgs e)
        {
            if (!vlcPlayer.IsPlaying)
            {

                vlcPlay_button.BackgroundImage = Properties.Resources.pause;
                vlcPlayer.Play();


            }
            else {
                vlcPlay_button.BackgroundImage = Properties.Resources.start;
                vlcPlayer.Pause();
            
            }
        }

        private void vlcStop_button_Click(object sender, EventArgs e)
        {
            if (vlcPlayer.State.ToString()!="Stopped")
            {

                vlcPlayer.Stop();
                VLC_track.Value = 0;

                vlcPlay_button.BackgroundImage = Properties.Resources.start;

            }

        }

        private void vlcSound_button_Click(object sender, EventArgs e)
        {
            if (!vlcPlayer.Audio.IsMute)
            {
                if (vVolume!=0)
                {
                    vlcSound_button.BackgroundImage = Properties.Resources.mute;
                    vlcPlayer.Audio.IsMute = true;
                }

            }
            else
            {
                if (vVolume!=0)
                {
                    vlcSound_button.BackgroundImage = Properties.Resources.sound;

                    vlcPlayer.Audio.IsMute = false;

                }
                //vlcPlayer.Audio.Volume = current_volume;

            }
        }

        private void loop_button_Click(object sender, EventArgs e)
        {
            videoloop = !videoloop;

            if (videoloop) 
            {
                loop_button.BackgroundImage = Properties.Resources.repeat;
            } 
            else
            {
                loop_button.BackgroundImage = Properties.Resources.stop;
            }


            vlc_display.Text = "loop playlist: "+ videoloop.ToString();
        }

        private void vlcPlayer_Click(object sender, EventArgs e)
        {
            Media_Player_ClickAction();
        }

        private void next_track_Click(object sender, EventArgs e)
        {
            if (Media_List.Count > 1) {
                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                FileInfo nfile = new FileInfo(nuri.LocalPath);
                String current = nfile.Name;
                int currentindex = -2;

                for (int i = 0; i < Media_List.Count; i++) {

                    if (Media_List.ElementAt(i)[1].Equals(current)) {
                        currentindex = i;
                    
                    }
                
                
                }

                if (currentindex + 1 < Media_List.Count)
                {

                    currentindex++;

                }
                else {
                    currentindex = 0;
                
                }

                FileInfo nfile2 = new FileInfo(Media_List.ElementAt(currentindex)[0]);
                if (vlcPlayer.IsPlaying) {
                    vlcPlayer.Stop();
                }
                vlcPlayer.SetMedia(nfile2);
                vlc_track_time = -2;
             
                comment2.Clear();
                comment.Clear();
                DM_List.Clear();
                playedcomment = 0;
                _duplicates = 0;
                time_counter = 0;
                setVLCname(nfile2);
                autoLoadByName(nfile2);
                timerStop();
                vlcPlayer.Play();
   

            }

        }
        void setVLCname(FileInfo file) {

            String temp = file.Name;
            setVLCname_thread("DM Player " + file.Name);
        
        }

        void setVLCname_thread(string s) {
            if (InvokeRequired) {
                setString st = new setString(setVLCname_thread);
                this.Invoke(st, new object[] { s });
            
            
            } else {

                this.Text = s;
            
            }
        
        
        
        }

        void selectPlaying_box(String filename) {
            //search for names in listbox equals to filename and selects it
            if (fm2 != null)
            {
                int currentinx  =-2;
                for (int i = 0; i < Media_List.Count; i++) {
                    if (Media_List.ElementAt(i)[1].Equals(filename)) { 
                    
                    currentinx = i;
                    }
                
                }
                if(currentinx>0){
                    
                    setfm2MBoxindex(currentinx);
                }


            }
        
        }
        void setfm2MBoxindex(int index) {

            if (InvokeRequired)
            {
                setInt i = new setInt(setfm2MBoxindex);
                this.Invoke(i, new object[] { index });
            }
            else {
                fm2.setMListBox.ClearSelected();
                fm2.setMListBox.SelectedIndex = index;
            
            }
        
        }
        private void last_track_Click(object sender, EventArgs e)
        {
            if (Media_List.Count > 1)
            {
                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                FileInfo nfile = new FileInfo(nuri.LocalPath);
                String current = nfile.Name;
                int currentindex = -2;

                for (int i = 0; i < Media_List.Count; i++)
                {

                    if (Media_List.ElementAt(i)[1].Equals(current))
                    {
                        currentindex = i;

                    }


                }

                if (currentindex - 1 > -1)
                {

                    currentindex--;

                }
                else
                {
                    currentindex = Media_List.Count-1;

                }

                FileInfo nfile2 = new FileInfo(Media_List.ElementAt(currentindex)[0]);
                if (vlcPlayer.IsPlaying)
                {
                    vlcPlayer.Stop();
                }
                vlcPlayer.SetMedia(nfile2);
                vlc_track_time = -2;
           
                comment2.Clear();
                comment.Clear();
                DM_List.Clear();
                playedcomment = 0;
                _duplicates = 0;
                time_counter = 0;
                autoLoadByName(nfile2);
                setVLCname(nfile2);
                timerStop();
                vlcPlayer.Play();
              

            }
        }
        // the orginal source page is call links
        // the media file urls are called url

        List<Uri> urls = new List<Uri>();
        List<Uri> Links = new List<Uri>();
        List<string> urlTitles=new List<string>();
        String current_dir_url;
        Uri linkaddress;
        add_link_form fm6;
        Dictionary<Uri, List<Uri>> UrlDictionary= new Dictionary<Uri,List<Uri>>();
        Url_menu fm7;
        getWebVideo gb;
        private void setFromURL_menu_Click(object sender, EventArgs e)
        {
            if (current_dir_url == null || linkaddress == null)
            {
                if (fm6 == null)
                {
                    fm6 = new add_link_form();
                    fm6.setCacnel.Click += new EventHandler(setCacnel_Click);
                    fm6.setConfirm.Click += new EventHandler(setConfirm_Click);
                    fm6.Disposed += new EventHandler(fm6_Disposed);

                    if (current_dir_url != null ) {

                        fm6.getDri = current_dir_url;
                    
                    
                    }


                    fm6.Show();




                }
                else {

                    fm6.Dispose();
                
                }
            }
            else {

                URL_menuLoadup();
            
            }


        }


        //   ・xml
        void URL_menuLoadup() {

            if (fm7 == null)
            {


                fm7 = new Url_menu();
                fm7.setLinkbox.Text = linkaddress.AbsoluteUri;

                //load the list with the urls if exists
                if (Links.Count > 0)
                {
                    for (int i = 0; i < Links.Count; i++)
                    {
                        fm7.getTitle.Items.Add(urlTitles.ElementAt(i));
                        fm7.getLinks.Items.Add(Links.ElementAt(i));
                        //the url list only displays when an items is selected in title or links
                    }
                }
                else
                {
                    //if links is empty that means it's first time run



                    load_Url_from(linkaddress);


                }
                fm7.getTitle.Click += new EventHandler(getTitle_Click);
                fm7.getTitle.DoubleClick += new EventHandler(getTitle_DoubleClick);
                fm7.getLinks.Click += new EventHandler(getLinks_Click);
                fm7.getloadURLbutton.Click += new EventHandler(getloadURLbutton_Click);
                fm7.getFileUrl.DoubleClick += new EventHandler(getFileUrl_DoubleClick);
                fm7.Disposed += new EventHandler(fm7_Disposed);
                fm7.Show();
            }
            else {

                fm7.Dispose();

            
            }
        
        
        }
        void renametoXML() {
            //throw new NotImplementedException();
            //rename the shattered xml file to the current file name

            DirectoryInfo dif = new DirectoryInfo(current_dir_url);

            DirectoryInfo chk = new DirectoryInfo(dif.FullName + "\\temp_xml");
            if (!chk.Exists) {
                chk.Create();
            
            }
            FileInfo[] files = chk.GetFiles("*xml");

            //only do this if there's only 1 file that suits the condition 
            if (files.Count() == 1)
            {

                FileInfo xml = files.ElementAt(0);

                byte[] tempbytes = Encoding.Default.GetBytes(gb.getCurrentTitle);
                string transtemp = Encoding.GetEncoding("shift-jis").GetString(tempbytes);

                xml.CopyTo(dif.FullName + "\\" + transtemp + ".xml", true);

                xml.Delete();

                fm7.getDownloadstatus1.Text = "xml file renamed to : " + transtemp + ".xml";
            }
        
        }
        void getTitle_DoubleClick(object sender, EventArgs e)
        {
            renametoXML();
        }

        void fm7_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            fm7 = null;
        }

        void getFileUrl_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //download files here

            //・xml

            renametoXML();

            DirectoryInfo dif = new DirectoryInfo(current_dir_url);

            ListBox box = (ListBox)sender;
            FileInfo file = new FileInfo(current_dir_url+"\\" + fm7.getTitle.SelectedItem.ToString());

            if (file.Exists)
            {DialogResult dg = MessageBox.Show("Already file existed in the directory are you sure you want to dl the file?","Alert",MessageBoxButtons.YesNo);
            if (dg == DialogResult.Yes)
            {
                downloadFile(sender);
            }
            else {
                if (!vlcPlayer.IsPlaying)
                {
                    if (vlcPlayer.GetCurrentMedia()==null || vlcPlayer.GetCurrentMedia().Title != file.Name)
                    {
                        string[] temp = { file.FullName, file.Name };
                        Media_List.Add(temp);
                        autoLoadMlist(temp);
                        autoLoadDMlist(temp);

                        // vlcPlayer.SetMedia(file);
                        vlc_track_time = -2;
                        autoLoadByName(file);
                        //this should return 1 or 2 files 
                        FileInfo[] findDM = dif.GetFiles(file.Name);
                        for (int i = 0; i < findDM.Count(); i++) { 
                        if(findDM[i].Name.Contains(".xml")){
                            string[] tdm = {findDM[i].FullName,findDM[i].Name};
                            DM_List.Add(tdm);
                            readXML(tdm[0]);

                            Danmoku_status.Text = "DM set";

                            if (fm3 == null)
                            {
                                commentWindowSetup();
                            }
                            else
                            {
                                ///////fm3.Owner = this;

                            }
                        
                        
                        }
                        
                        }

                            Media_status.Text = "Media Set";
                    }
                }
            }
            }else{
            
            downloadFile(sender);
            }


        }

        public AsyncCompletedEventHandler DownloadFileCompleted(Uri filename)
        {
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                var _filename = filename;

                if (e.Error != null)
                {
                    throw e.Error;
                }

                try
                {
                    if (e.Cancelled)
                    {


                        fm7.getDownloadstatus2.Text = "server error";
              

                    }
                }
                catch (Exception) {

                    fm7.getDownloadstatus2.Text = "server error";

                    WebClient wb = (WebClient)sender;
                    wb.Dispose();
                }
            //    if (!gb.downlaodFile.Any())
           //     {
            //        complited = true;
            //    }
            //    downloadFile(sender);


                try
                {
                    wb_DownloadFileCompleted(filename);
                }
                catch (Exception) { fm7.getDownloadstatus2.Text = "server error"; }

            };
            return new AsyncCompletedEventHandler(action);





        }


        // write a method contains load mlist and set vlcplayer media , replace all the current vlcplayer.setmedia

        Dictionary<Uri, String> MultiDownloadLinks = new Dictionary<Uri, string>();

        void downloadFile(object sender) {
            ListBox box = (ListBox)sender;
            using (WebClient wb = new WebClient())
            {
                wb.Encoding = Encoding.UTF8;

                Uri filename = (Uri)box.SelectedItem;
               // wb.DownloadFileCompleted += new AsyncCompletedEventHandler(wb_DownloadFileCompleted);
                wb.DownloadFileCompleted += DownloadFileCompleted(filename);
                wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wb_DownloadProgressChanged);
                gb.downlaodFile((Uri)box.SelectedItem, wb);

                //add the used link as a key for getting the title of the current file
                MultiDownloadLinks.Add((Uri)box.SelectedItem, fm7.getTitle.SelectedItem.ToString());

                vlcPlayer.SetMedia(gb.playDownlist);
                vlc_track_time = -2;
                //autoLoadByName(gb.playDownlist);

                FileInfo file2 = new FileInfo(gb.playDownlist.OriginalString);

                FileInfo[] findDM = file2.Directory.GetFiles(fm7.getTitle.SelectedItem.ToString());

                for (int i = 0; i < findDM.Count(); i++)
                {
                    if (findDM[i].Name.Contains("*.xml"))
                    {
                        string[] tdm = { findDM[i].FullName, findDM[i].Name };
                        DM_List.Add(tdm);
                        readXML(tdm[0]);

                        Danmoku_status.Text = "DM set";

                       
                        }


                    }
                if (fm3 == null)
                {
                    commentWindowSetup();
                }
                else
                {
                    ///////fm3.Owner = this;


                }





                //set the media in vlc player
                urls.Remove((Uri)box.SelectedItem);
                FileInfo file = new FileInfo(gb.playDownlist.OriginalString);
                String[] temp = {file.FullName,file.Name };
                Media_List.Add(temp);
                autoLoadMlist(temp);
                autoLoadDMlist(temp);

                UrlDictionary[(Uri)fm7.getLinks.SelectedItem].Remove((Uri)box.SelectedItem);
                box.Items.Remove((Uri)box.SelectedItem);






                wb.Dispose();

            }
        
        }
        void autoLoadByName(FileInfo file)
        {

            comment2.Clear();
            comment.Clear();
            playedcomment = 0;
            _duplicates = 0;
            DM_List.Clear();


          //  FileInfo file = new FileInfo(media.LocalPath);

          //  throw new Exception("just to see") { };
            //search the parent dir of the file to see if there's other files has the same name
            FileInfo[] files = file.Directory.GetFiles("*.xml");

            //get all xml files in the directory

           // throw new Exception("just to see");
            if (!file.Name.Contains("."))
            {
                //if the file has extension
                for (int i = 0; i < files.Length; i++)
                {

                    if (files[i].Name.Contains(file.Name))
                    {
                        string[] tempstring = { files[i].FullName, files[i].Name };

                        DM_List.Add(tempstring);
                        readXML(files[i].FullName);
                        Danmoku_status.Text = "DM Set";

                    }


                }

            }
            else
            {

                for (int i = 0; i < files.Length; i++)
                {
                    int end = file.Name.LastIndexOf(".");
                    string newName = file.Name.Substring(0, end);
                    if (files[i].Name.Contains(newName))
                    {
                        string[] tempstring = { files[i].FullName, files[i].Name };

                        DM_List.Add(tempstring);
                        readXML(files[i].FullName);
                        Danmoku_status.Text = "DM Set";

                    }


                }


            }


            if (fm3 == null)
            {
                commentWindowSetup();
            }
            else
            {
                ///////fm3.Owner = this;

            }
        
        }

        void autoLoadByName(Uri media) {
            FileInfo file = new FileInfo(media.LocalPath);

         //   throw new Exception("just to see"){};
            //search the parent dir of the file to see if there's other files has the same name
            FileInfo[] files = file.Directory.GetFiles("*.xml");

            //get all xml files in the directory
                
            
            if (!file.Name.Contains("."))
            {
                //if the file has extension
                for (int i = 0; i < files.Length; i++) {

                    if (files[i].Name.Contains(file.Name)) {
                        string[] tempstring = { files[i].FullName, files[i].Name };

                        DM_List.Add(tempstring);
                        readXML(files[i].FullName);
                        Danmoku_status.Text = "DM Set";
                    
                    }
                
                
                }

            }
            else {

                for (int i = 0; i < files.Length; i++)
                {
                    int end = file.Name.LastIndexOf(".");
                    string newName = file.Name.Substring(0,end);
                    if (files[i].Name.Contains(newName))
                    {
                        string[] tempstring = { files[i].FullName, files[i].Name };

                        DM_List.Add(tempstring);
                        readXML(files[i].FullName);
                        Danmoku_status.Text = "DM Set";

                    }


                }

            
            }


            if (fm3 == null)
            {
                commentWindowSetup();
            }
            else
            {
                ///////fm3.Owner = this;

            }
        
        
        }
        void wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
            //show dl progress
            String currentbyte = e.BytesReceived.ToString();
            String totalbyte = e.TotalBytesToReceive.ToString();
            int progress = e.ProgressPercentage;
            if (fm7 != null) {

                fm7.getProgressbar.Maximum = 100;
                fm7.getProgressbar.Minimum = 0;
                fm7.getProgressbar.Value = progress;
                fm7.getDownloadstatus1.Text = currentbyte + "/" + totalbyte;
            
            }
            downlaod_status.Text ="downloading"+gb.getCurrentTitle+" "+ currentbyte + "/" + totalbyte +"  "+progress +"% completed";

            



        }

        void wb_DownloadFileCompleted(Uri ufilename)
        {
            //throw new NotImplementedException();
            //remove urls from url list
            //change dled file name
            //update name in gb dlfile 

            String dllink = ufilename.LocalPath;







            string title = MultiDownloadLinks[ufilename];

            int start = dllink.LastIndexOf("/") + 1;
            int end = dllink.Length;

            String filename = dllink.Substring(start, end - start);


            FileInfo file = new FileInfo(current_dir_url + "\\" + filename);
            string[] tempstring = { file.FullName, file.Name };
            Media_List.Remove(tempstring);
            if (file.Length > 0)
            {
                if (fm7 != null)
                {

                    fm7.getProgressbar.Value = 0;
                    fm7.getDownloadstatus1.Text = "downlaod completed, renaming file now";

                }
                downlaod_status.Text = "download completed, Renaming file now";

                title = title.Replace("\\", " ");
                title = title.Replace("/", " ");


                //パス 'A:\video\hima test video\Fate/kaleid liner プリズマ☆イリヤ ドライ！ 第01話 吸収さん高画質「銀色に沈む街」 - ひまわり動画' の一部が見つかりませんでした。
                FileInfo temp = new FileInfo(file.Directory + "\\" + title);


                if (!temp.Exists || file.Length > temp.Length)
                {
                    file.CopyTo(file.Directory + "\\" + title, true);
                }
                else
                {

                    file.CopyTo(file.Directory + "\\" + title + "(1)", false);
                }

                gb.dlnameUpdate = temp.FullName;
                tempstring = new String[] { file.FullName, file.Name };
                Media_List.Add(tempstring);


                if (vlcPlayer.IsPlaying)
                {


                    String[] m = {temp.FullName,temp.Name };
                    Media_List.Add(m);



                }
                else
                {


                    String[] m = { temp.FullName, temp.Name };
                    Media_List.Add(m);
                  //  vlcPlayer.SetMedia(temp);
                  //  autoLoadByName(temp);
                    //           vlcPlayer.Play();

                }
                file.Delete();

            }
            else
            {

                file.Delete();

            }
            if (fm7 != null)
            {
                fm7.getDownloadstatus1.Text = "";
            }
            downlaod_status.Text = "";

        }

        void wb_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            //remove urls from url list
            //change dled file name
            //update name in gb dlfile 

            string dllink = "";

            
            WebClient wb = (WebClient)sender;
            
                
            
                dllink=wb.BaseAddress;

            

            string title = MultiDownloadLinks[new Uri(dllink)];
            
            int start = dllink.LastIndexOf("\\")+1;
            int end = dllink.Length;

            String filename = dllink.Substring(start, end - start);

            FileInfo file = new FileInfo(current_dir_url+"\\"+filename);
            string[] tempstring = { file.FullName, file.Name };
            Media_List.Remove(tempstring);
            if (file.Length > 0)
            {
                if (fm7 != null)
                {

                    fm7.getProgressbar.Value = 0;
                    fm7.getDownloadstatus1.Text = "downlaod completed, renaming file now";

                }
                downlaod_status.Text = "download completed, Renaming file now";

                title = title.Replace("\\", " ");
                title = title.Replace("/", " ");


                //パス 'A:\video\hima test video\Fate/kaleid liner プリズマ☆イリヤ ドライ！ 第01話 吸収さん高画質「銀色に沈む街」 - ひまわり動画' の一部が見つかりませんでした。
                FileInfo temp = new FileInfo(file.Directory + "\\" + title);
               
                
                if (!temp.Exists || file.Length > temp.Length)
                {
                    file.CopyTo(file.Directory + "\\" + title, true);
                }
                else {

                    file.CopyTo(file.Directory + "\\" + title + "(1)", false);
                }

                gb.dlnameUpdate = temp.FullName;
                tempstring= new String[]{file.FullName,file.Name};
                Media_List.Add(tempstring);
                if (vlcPlayer.IsPlaying)
                {
                    long temptime = vlcPlayer.Time;
                    vlcPlayer.Stop();
                    vlcPlayer.SetMedia(temp);
                    vlc_track_time = -2;
                    autoLoadByName(temp);
                    vlcPlayer.Time = temptime;

                }
                else {

                    FileInfo[] findDM = file.Directory.GetFiles(temp.Name);

                    for (int i = 0; i < findDM.Count(); i++)
                    {
                        if (findDM[i].Name.Contains("*.xml"))
                        {
                            string[] tdm = { findDM[i].FullName, findDM[i].Name };
                            DM_List.Add(tdm);
                            readXML(tdm[0]);

                            Danmoku_status.Text = "DM set";

                            if (fm3 == null)
                            {
                                commentWindowSetup();
                            }
                            else
                            {
                                ///////fm3.Owner = this;

                            }


                        }

                    }

                    vlcPlayer.SetMedia(temp);
                    vlc_track_time = -2;
                    autoLoadByName(temp);
         //           vlcPlayer.Play();
                
                }
                file.Delete();

            }
            else {

                file.Delete();
            
            }
            if (fm7 != null) {
                fm7.getDownloadstatus1.Text = "";
            }
            downlaod_status.Text = "";

        }

        void load_Url_from(Uri address) {

            if (!Links.Contains(address))
            {


                    
                

                fm7.getFileUrl.Items.Clear();
                gb = new getWebVideo(address, current_dir_url);
                Links.Add(address);
                // :/\?<>*|
                gb.getCurrentTitle = gb.getCurrentTitle.Replace("\\", " ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace("/", " ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace(":"," ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace("?", " ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace("<", " ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace(">", " ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace("*", " ");
                gb.getCurrentTitle = gb.getCurrentTitle.Replace("|", " ");

                urlTitles.Add(gb.getCurrentTitle);
                urls = gb.getAllfiles;
                UrlDictionary.Add(address, gb.getAllfiles);

                //load all the info onto the box
                fm7.getLinks.Items.Add(address);
                fm7.getTitle.Items.Add(gb.getCurrentTitle);
                //load url itembox with all the links


                //only do this if it's on himado.in since this only works on there
                if (address.OriginalString.Contains("himado.in"))
                {
                WebBrowser wb = new WebBrowser();

              //  wb.TopLevelControl.Visible = false;
                wb.Visible = false;
                //wb.Document.Encoding = "UTF-8";
                wb.ScriptErrorsSuppressed = true;
               // printvlc(wb.Version.ToString());
                
                wb.Url = address;


               
               // wb.Navigate(address);
                
               

               // wb.Navigated +=new WebBrowserNavigatedEventHandler(wb_Navigated);

                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
                }

                for (int i = 0; i < urls.Count; i++)
                {
                    fm7.getFileUrl.Items.Add(urls.ElementAt(i));

                     
                }
                
                fm7.getLinks.SelectedIndex = fm7.getLinks.Items.Count-1;
                fm7.getTitle.SelectedIndex = fm7.getTitle.Items.Count-1;
            }
        
        }

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            

            fm7.getDownloadstatus2.Text = "document loaded";
            
            


            WebBrowser wb = (WebBrowser)sender;


            wb.Document.Encoding = "UTF-8";

       //     List<String> viewer = new List<string>();
      //      List<String> viewer2 = new List<string>();

            for (int i = 0; i < wb.Document.GetElementsByTagName("select").GetElementsByName("limit")[0].GetElementsByTagName("option").Count; i++) {

               
              //  viewer.Add(wb.Document.GetElementsByTagName("select").GetElementsByName("limit")[0].GetElementsByTagName("option")[i].GetAttribute("value"));

                wb.Document.GetElementsByTagName("select").GetElementsByName("limit")[0].GetElementsByTagName("option")[i].SetAttribute("value","20000");
             //   viewer2.Add(wb.Document.GetElementsByTagName("select").GetElementsByName("limit")[0].GetElementsByTagName("option")[i].GetAttribute("value"));

            
            }
           // throw new Exception("view value");





                for (int i = 0; i < wb.Document.GetElementsByTagName("input").Count; i++)
                {



                    if (wb.Document.GetElementsByTagName("input")[i].GetAttribute("value").Equals("ダウンロード"))
                    {
                        wb.Document.GetElementsByTagName("input")[i].InvokeMember("click");

                        wb.Dispose();
                        break;

                    }
                }
           
            wb.Dispose();
        }

        void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

            fm7.getDownloadstatus2.Text = "document loaded";

            WebBrowser wb = (WebBrowser)sender;
            for (int i = 0; i < wb.Document.GetElementsByTagName("input").Count; i++)
            {
                
             

                if (wb.Document.GetElementsByTagName("input")[i].GetAttribute("type").Equals("submit"))
                {
                    wb.Document.GetElementsByTagName("input")[i].InvokeMember("click");

                    
                }
            }

 
            wb.Dispose();
            
        }
        void getloadURLbutton_Click(object sender, EventArgs e)
        {

           // <input type="submit" value="ダウンロード">
            //throw new NotImplementedException();
            //when the set url button is clicked
            Uri cb;
            try
            {
                 cb = new Uri(fm7.setLinkbox.Text);
            }
            catch (Exception) {
                 cb = null;
            }
            if (cb != null)
            {
                load_Url_from(cb);
            }


        }

        void getLinks_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //basiclly the same as get title with a few lines chagned


            //clear the uri box
            fm7.getFileUrl.Items.Clear();
            ListBox temp = (ListBox)sender;

            int selected = temp.SelectedIndex;
            fm7.getTitle.SelectedIndex = selected;

            //if the link has been read before add if not in else area read the links and load it on dictionary

            if (UrlDictionary.ContainsKey(Links.ElementAt(selected)))
            {
                for (int i = 0; i < UrlDictionary[Links.ElementAt(selected)].Count(); i++)
                {

                    fm7.getFileUrl.Items.Add(UrlDictionary[Links.ElementAt(selected)].ElementAt(i));

                }

            }
            else
            {
                // not doing anything here for now since it might conflict with later code
                // since everything in the links are supposed to be loaded and add into dictionary
                // so here is just to prevent the unexcepted error


            }



        }

        void getTitle_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            // when the name of the title is clicked

            //clear the uri box
            fm7.getFileUrl.Items.Clear();

            ListBox temp = (ListBox)sender;

            int selected = temp.SelectedIndex;
            fm7.getLinks.SelectedIndex = selected;

            //if the link has been read before add if not in else area read the links and load it on dictionary
            if (selected >= 0)
            {
                if (UrlDictionary.ContainsKey(Links.ElementAt(selected)))
                {
                    for (int i = 0; i < UrlDictionary[Links.ElementAt(selected)].Count(); i++)
                    {

                        fm7.getFileUrl.Items.Add(UrlDictionary[Links.ElementAt(selected)].ElementAt(i));

                    }

                }
                else
                {
                    // not doing anything here for now since it might conflict with later code
                    // since everything in the links are supposed to be loaded and add into dictionary
                    // so here is just to prevent the unexcepted error


                }

            }


        }

        void fm6_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //when fm6 gets disposed;

            if (current_dir_url == null || linkaddress == null)
            {

                //do nothing if one fo the two is not set


                // may modiefied  current_dir_url if you want to leave the directory out for online streaming instead of dled to local then play but for now it's all dled local
            }
            else { 
            
            //opens out the url menu that displays the list of links to dl or maybe past urls ?
            //loads content of the url and then save the url as key the list of links in content as values
              
                
                URL_menuLoadup();
            
            
            }
            fm6 = null;

        }

        void setConfirm_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if confirm button is clicked 
            linkaddress = fm6.getLink;
            current_dir_url = fm6.getDri;

            fm6.Dispose();
        }

        void setCacnel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //when cancel buttun is click on the add link form

            fm6.Dispose();

        }


        //write to XML file to save user changes to settings?



    }
}
