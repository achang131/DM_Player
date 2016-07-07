using System;
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
      //  LinkedList<Label> comment_storage = new LinkedList<Label>();
        XmlTextReader dm_comment;
        LinkedList<String[]> comment = new LinkedList<String[]>();
      //  LinkedList<Label> remove_LinkedList = new LinkedList<Label>();
        int time_offset;
        int move_distance;
        int playedcomment;
        LinkedList<String[]> Media_LinkedList = new LinkedList<String[]>();
        LinkedList<String[]> DM_LinkedList = new LinkedList<String[]>();
        LinkedList<String[]> FullDM_LinkedList = new LinkedList<String[]>();

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
        // Next try add setting (new window)  and Play/DM LinkedList(new window possible tabs ?)

        int choose_player; //1 for Windows media player 2, for vlc player


        int current_volume;

        bool mousedown;

        int vlcTIme;

        bool autoMlist;

        int vVolume;

        public Form1()
        {
            InitializeComponent();

            media = new OpenFileDialog();
            danmoku = new OpenFileDialog();
            danmoku.Filter = "xml |*.xml";

           

         

          //  fm2 = new Form2();

            // controls the speed of the DM  default at 5? the lesser the faster 

            //comment settings


            CSettings();
          
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


            vlcPlayer.MouseWheel+=new MouseEventHandler(Form1_MouseWheel);
            vlcPlayer.MouseClick += new MouseEventHandler(vlcPlayer_MouseClick);
            vlcPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(vlcPlayer_Playing);
            vlcPlayer.Stopped += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerStoppedEventArgs>(vlcPlayer_Stopped);
            vlcPlayer.Paused += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs>(vlcPlayer_Paused);
            vlcPlayer.MediaChanged += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerMediaChangedEventArgs>(vlcPlayer_MediaChanged);
            vlcPlayer.TimeChanged += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs>(vlcPlayer_TimeChanged);
            vlcPlayer.Opening += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerOpeningEventArgs>(vlcPlayer_Opening);
                        


            VLC_track.MouseDown += new MouseEventHandler(VLC_track_MouseDown);
            VLC_track.MouseUp += new MouseEventHandler(VLC_track_MouseUp);
            VLC_track.ValueChanged += new EventHandler(VLC_track_ValueChanged);
            VLC_track.MouseMove += new MouseEventHandler(VLC_track_MouseMove);
            
            



            if (choose_player == 1)
            {

                vlcPlayer.Dispose();
            }
            else {

                Media_Player.Dispose();
            }
            


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
        void VLC_track_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                mousemoving = true;
            }
            else {

                mousemoving = false;
            }
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

        void changetime() {


            if ( mousedown == true && VLC_track.Value != time_counter)
            {
             //   timerStop();
                vlcPlayer.Time = VLC_track.Value * 10;
                
                if (VLC_track.Value * 10 >= vlcPlayer.Length) {

                    manual_checkend((int)vlcPlayer.Length,VLC_track.Value);
                }


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

                    vlc_display.Text = vlc_display.Text+ "end reached";
                    if (videoloop == false)
                    {
                        if (vlcPlayer.IsPlaying)
                        {
                            vlcPlayer.Stop();
                        }

                        vlcPlayer.Time = 0;
                        time_counter = 0;
                       

                    }
                    else
                    {
                        if (Media_LinkedList.Count > 1)
                        {
                            int currentinx = -2;
                            try
                            {
                                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                                FileInfo nfile = new FileInfo(nuri.LocalPath);
                                String currentv = nfile.Name;

                                for (int i = 0; i < Media_LinkedList.Count(); i++)
                            {

                                if (Media_LinkedList.ElementAt(i)[1].Equals(currentv))
                                {

                                    currentinx = i;

                                }

                            }
                            if (currentinx == Media_LinkedList.Count - 1)
                            {

                                currentinx = 0;
                            }
                            else
                            {
                                currentinx++;
                            }
                            FileInfo nv = new FileInfo(Media_LinkedList.ElementAt(currentinx)[0]);
                            if (vlcPlayer.IsPlaying)
                            {
                                vlcPlayer.Stop();
                            }
                            comment.Clear();
                            comment2.Clear();
                            time_counter = 0;
                            playedcomment = 0;
                            vlcPlayer.SetMedia(nv);
                            vlcPlayer.Play();
                            timerStart();

                            this.Text = "DM Player " + nv.Name;
                            }
                            catch (ArgumentException) { };

                        }
                        else
                        {
                            time_counter = 0;
                            vlcPlayer.Time = 0;
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


            timerStop();


            removeAllComments();
        }

        void vlcPlayer_Stopped(object sender, Vlc.DotNet.Core.VlcMediaPlayerStoppedEventArgs e)
        {
            test_label.Text = vlcPlayer.State.ToString();

            playedcomment = 0;

            time_counter = 0;

    

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

            _distance = move_distance;

            timer1.Interval = 1;

            commentdestroy = -100;

            sommentswitch = true;

            threading_mode = false;

            vpos = -1;

            _first_load = false;

            auto_base = 0;

            userColor = Color.DarkGray;

            replacetimer1_interval = 30;
            replacetimer3_interval = 29;
            replacetimer2_interval = 10;

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


            Media_Player.enableContextMenu = true;


            //


            choose_player = 2;

            switchPlayer(choose_player);

            current_volume = vlcPlayer.Audio.Volume;

            mousedown = false;

            vVolume = 50;


            //decide if it's going to load all the same media/xml file under the same directory into the playlist

            autoMlist = true;

            videoloop = true;

        
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
                    move_distance = (int)(_distance * 2.7);
                }
                else if (lnumber > 60)
                {
                    move_distance = (int)(_distance * 2.3);
                }
                else if (lnumber > 50)
                {
                    move_distance = _distance * 2;

                }
                else if (lnumber > 40)
                {

                    move_distance = (int)(_distance * 1.7);

                }
                else if (lnumber > 30)
                {

                    move_distance = (int)(_distance * 1.3);
                }
                else
                {

                    move_distance = _distance;
                }

            }

        }
        void replacetimer3_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!replacetimer3.CancellationPending)
            {

                changingSpeedontime();
                moveComment_thread();

                //commentEngine_thread();

                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(replacetimer3_interval));

            }
        }

        void replacetimer2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!replacetimer2.CancellationPending) {

               // moveComment_thread();

                commentEngine_thread();

                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(replacetimer2_interval));
            
            }
        }
        void replacetimer3_start()
        {
            if (!replacetimer3.IsBusy)
                replacetimer3.RunWorkerAsync();

        }
        void replacetimer3_stop()
        {

            try
            {
                replacetimer3.CancelAsync();
            }
            catch (Exception) { }
        }
        void replacetimer2_start() {
            if(!replacetimer2.IsBusy)
            replacetimer2.RunWorkerAsync();
        
        }
        void replacetimer2_stop() {

            try
            {
                replacetimer2.CancelAsync();
            }catch(Exception){}
        }

        void printvlc(String s) {
            this.vlc_display.Text = s;
        
        }
        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {

           
            switch (e.Delta)
            {
                case 120:

                    if (choose_player == 1)
                    {
                        Media_Player.settings.volume++;
                    }
                    else {
                        vVolume++;
                        vlcPlayer.Audio.Volume = vVolume;
                        //vlcPlayer.Audio.Volume++;
                        printvlc("Volume: "+vlcPlayer.Audio.Volume);
                    }

                    break;


                case -120:
                    if (choose_player == 1)
                    {
                        Media_Player.settings.volume--;
                    }
                    else
                    {
                        vVolume--;
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
            try
            {
                replacetimer1.CancelAsync();
            }
            catch (InvalidOperationException) { };
        
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





        //load xml into a LinkedList and foreach addcomment ?
        void addComment(int time,String comment) { 


        //time counter dependent
            if (time == time_counter) {

                createLabel(comment);
            
            }
        
        
        }
        void addComment(int currenttime,Dictionary<int,String> d) {

            if (currenttime!=vpos && d.ContainsKey(currenttime))
            {
                playedcomment++;
                createLabel(d[currenttime]);
                vpos = currenttime;
            }
        
        }

        void addComment(int currenttime, int time, String comment) {

            if (currenttime == time) {

                playedcomment++;
                createLabel(comment);
                
            }
        }



        void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            Media_Player.Size = new System.Drawing.Size(ClientRectangle.Width,ClientRectangle.Height-49);

            if (fm3 != null)
            {
                fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
            }
         

            vlcPlayer.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 94);
            vlcPlayer.Location = new Point(0, ClientRectangle.Top + 29);

            vlcPlay_button.Location = new Point(32,ClientRectangle.Bottom-42);

            vlcStop_button.Location = new Point(77, ClientRectangle.Bottom - 42);
            
            vlcSound_button.Location = new Point(ClientRectangle.Right-81, ClientRectangle.Bottom - 42);

            loop_button.Location = new Point(ClientRectangle.Right - 111, ClientRectangle.Bottom - 42);

            last_track.Location = new Point(145, ClientRectangle.Bottom - 42);
            next_track.Location = new Point(194, ClientRectangle.Bottom - 42);
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
            catch (NullReferenceException) { }
        }

        void cautoSet() {

            if (vpos_end > vpos_video * 1.3)
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

                VLC_track.Maximum = time;
                VLC_track.Minimum = 0;
                VLC_track.TickFrequency = 1;
            }


        }


            void timerStart(){
                
                _isPlaying = true;
                replacetimer1_start();

                replacetimer2_start();
            
                replacetimer3_start();
            
            
        }
        void timerStop(){
        
                  
            _isPlaying = false;
                
            replacetimer1_stop();

            
            replacetimer2_stop();

            replacetimer3_stop();
        
        
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
        void createLabel(String comment) {
            Label dm = new Label();
            
            //
            Random ypos = new Random();
            if (40 < fm3.ClientRectangle.Bottom - 80)
            {
                ycurrent = ypos.Next(40, fm3.ClientRectangle.Bottom-80);
            }
            else {

                ycurrent = 40;
            }
            dm.Location = new Point(ClientRectangle.Right, ycurrent);

            //
            /*
            if(ycurrent+20<ClientRectangle.Bottom-50){
            ycurrent+= 20;}
            else
            {
                ycurrent=40;
            };
            */
            //

            dm.Text = comment;
            dm.Name = comment;
            dm.TabIndex = 3;
            dm.Visible = true;
            dm.AutoSize = true;
            
            dm.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Bold);
            dm.ForeColor = userColor;
            dm.MouseClick += new MouseEventHandler(dm_MouseClick);
            
            
            dm.Size = new System.Drawing.Size(35, 15);
/*
            var position = this.PointToScreen(dm.Location);
            position = Media_Player.PointToClient(position);
            dm.Parent = Media_Player;
            dm.BackColor = Color.Transparent;
  */
 
  /*
             *        var pos = this.PointToScreen(label1.Location);
        pos = pictureBox1.PointToClient(pos);
        label1.Parent = pictureBox1;
        label1.Location = pos;
        label1.BackColor = Color.Transparent;
             */

            fm3.Controls.Add(dm);

           // safecontrol(dm);

           // comment_storage.Add(dm);
            dm.BringToFront();
            dm.Show();
        
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
            int xend = commentdestroy;
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
             //   remove_LinkedList.Add(l);
                //remove_LinkedList.AddLast(l);
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
                
                //remove_LinkedList.AddLast(l);
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
                   // remove_LinkedList.AddLast(l);
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
        void resetComment(LinkedList<Label> l) {

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
        void setMedia_Multi(LinkedList<String[]> medias) {
            switch(choose_player){
                case 1: 
                    
                    
                    for (int i = 0; i < medias.Count(); i++)
            {
                Media_Playlist.appendItem(Media_Player.newMedia(medias.ElementAt(i)[0]));
                Media_LinkedList.AddLast(medias.ElementAt(i));

            }
            media_dir = medias.ElementAt(0)[0];
            Media_status.Text = "Playlist Set";
            Media_Player.currentPlaylist = Media_Playlist;

             Media_Player.Ctlcontrols.stop();


                break;

                case 2:
                    for (int i = 0; i < medias.Count(); i++)
            {
                
                Media_LinkedList.AddLast(medias.ElementAt(i));

            }
            media_dir = medias.ElementAt(0)[0];
            Media_status.Text = "Playlist Set";

            FileInfo temp = new FileInfo(medias.ElementAt(0)[0]);
            vlcPlayer.SetMedia(temp);
            //onLoadUp();
            // need to manulally create a method to play the list of files in media_linklist after the first loaded video done playing
            
            break;
                
            
            }

            
        }

        void autoLoadMlist(String[] mediadirs) {

            Media_LinkedList.Clear();

        int end = mediadirs[0].LastIndexOf("\\");
        String temp_dir = mediadirs[0].Substring(0, end);

        DirectoryInfo fdir = new DirectoryInfo(temp_dir);

        end = mediadirs[1].LastIndexOf(".");

        String extension = mediadirs[1].Substring(end, mediadirs[1].Length-end);

        FileInfo[] file = fdir.GetFiles("*"+extension);

        for (int i = 0; i < file.Count(); i++) {

            String[] ml = {file[i].FullName,file[i].Name};
            
            Media_LinkedList.AddLast(ml);

            if (fm2 != null) {

                fm2.setMListBox.Items.Add(ml[1]);
            
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

                for (int l = 0; l < FullDM_LinkedList.Count(); l++) {
                  //  if (FullDM_LinkedList.Count > 0)
                 //   {
                        if (FullDM_LinkedList.ElementAt(l)[0].Equals(results[0]))
                        {
                            exists = true;

                        }
                //    }
                
                }


                    if (!exists)
                    {
                        FullDM_LinkedList.AddLast(results);

                        //if the list menu windows is left opened when video/comment is loaded through dropdown menu maynot be need ? 
                        if (fm2 != null)
                        {
                            fm2.setFullDMBox.Items.Add(results[1]);
                        }

                    }
            
            
            }

        
        
        
        
        }

        void setMedia_Single(LinkedList<String[]> medias)
        {

            if (medias != null)
            {
                media_dir = medias.ElementAt(0)[0];
                Media_status.Text = "Media Set";
                Media_LinkedList.AddLast(medias.ElementAt(0));



                if (fm3 != null)
                {

                    fm3.Owner = this;

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
            }
           // onLoadUp();
            break;
        }

            if (fm3 != null)
            {

                fm3.Owner = this;
            }
            


        }


        void PlayMedia(LinkedList<String[]> medias) {

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
                fm3.Owner = this;

            }


            LinkedList<String[]> medias = setFile(media);

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
                        comment.AddLast(temp_comment);
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
        private void setDMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            if (multi_dm_mode == false)
            {
                comment.Clear();
                comment2.Clear();
                _duplicates = 0;

                //clears all the dm that is current loaded
                DM_LinkedList.Clear();

            }
            if (fm3 != null) {
                try
                {
                     this.Owner = fm3;
                    //fm3.Owner = this;
                }
                catch (Exception) { }

                
            }


           LinkedList< String[]> danmokus = setFile(danmoku);

           if (danmokus != null)
           {
           if (danmokus.Count > 1) {

               multi_dm_mode = true;
               setDMsToolStripMenuItem.Text = "Set DMs" + " Multi On ";
           }
          
          
          

    
              if (danmokus.Count == 1)
              {
                  danmoku_dir = danmokus.ElementAt(0)[0];
                  Danmoku_status.Text = "DM Set";
                  if (!DM_LinkedList.Contains(danmokus.ElementAt(0)))
                  {
                      DM_LinkedList.AddLast(danmokus.ElementAt(0));

                  }
                  //reader.name is the name of the element/attribute
                  //reader.value is the value of the attribute/text

                  readXML(danmoku_dir);

                  danmoku_dir = null;
                  Danmoku_status.Text = "DM Ready";

              }
              else {

                  for (int i = 0; i < danmokus.Count(); i++) {


                      //add only if it's not in the list to prevent dulplicate from being loaded
                      if (!DM_LinkedList.Contains(danmokus.ElementAt(i)))
                      {
                          danmoku_dir = danmokus.ElementAt(i)[0];
                          DM_LinkedList.AddLast(danmokus.ElementAt(i));
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
              else {
                  fm3.Owner = this;
              
              }

              autoLoadDMlist(danmokus.ElementAt(0));

          }
          else {
              Danmoku_status.Text = "No DM";
              if (fm3 != null) {
                  fm3.Owner = this;
              }
          }
        }

        void commentWindowSetup() {

            fm3 = new Comment_window();
            fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
            fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
            fm3.MouseClick += new MouseEventHandler(dm_MouseClick);
            fm3.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            fm3.Owner = this;
        
        
        }
        //modified from String[] to LinkedList so multiple files can be selected at once.

        private LinkedList<String[]> setFile(OpenFileDialog ofd){
            ofd.Multiselect = true;
        
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

                int end = ofd.FileNames.ElementAt(0).LastIndexOf("\\");
                String dir = ofd.FileNames.ElementAt(0).Substring(0, end);
                ofd.InitialDirectory = dir;
                LinkedList<String[]> temp = new LinkedList<String[]>();
                for (int i = 0; i < ofd.FileNames.Count(); i++)
                {
                    String[] result = { ofd.FileNames.ElementAt(i), ofd.SafeFileNames.ElementAt(i) };
                    temp.AddLast(result);


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

            move_distance = s;
        
        }

        void print(String s) {

            debugtab.Text = s;
        
        }
        void print2(String s) {

            debugtab2.Text = s;
        }
        void makeComment() {

           


            //time_counter = (int)(Media_Player.Ctlcontrols.currentPosition * 100);
            int comment_time;

            time_counter++;
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

                       

                        if (auto_TimeMatch == false)
                        {



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
            int vlc_offset;

            /*
            if(auto_TimeMatch){

              vlc_offset = (int)(vlcPlayer.Time)/10+time_offset;

            }else{

                vlc_offset = (int)(vlcPlayer.Time) / 10 + auto_base + offset_auto;

            }

            //if the counter is too slow or too fast it'll bring the counter back to the current vlc time 
            if (time_counter > vlc_offset + 10 || time_counter < vlc_offset - 10)
            {
                if (auto_TimeMatch)
                {
                    time_counter = (int)(vlcPlayer.Time)/10+time_offset;
                }
                else {

                    time_counter = (int)(vlcPlayer.Time) / 10 + auto_base + offset_auto;
                }
            }
            */
            vlcTIme = (int)(vlcPlayer.Time / 10);
            print(showTime(((int)(vlcPlayer.Time)) ) + "/" + comment_time + "/" + time_counter);


            if (fm3 != null)
            {
                print2(showTime((int)vlcPlayer.Length)+"  L: " + fm3.Controls.OfType<Label>().Count().ToString() + " " + playedcomment + "/" + (comment.Count() + _duplicates));
            }
            else {

                print2(playedcomment + "/" + (comment.Count() + _duplicates));
            
            }
            /*
            foreach(String[] c in comment){
                addComment(comment_time,Int32.Parse(c[0]), c[1]);
                        
            }
            */



            addComment(comment_time, comment2);
            
            if (choose_player != 1)
            {

                try
                {
                    manual_checkend((int)vlcPlayer.Length/10, time_counter);
                }
                catch (NullReferenceException) { }
                }
        
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
                try
                {
                    for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++)
                    {


                        MoveLabel(fm3.Controls.OfType<Label>().ElementAt(i));



                    }
                }
                catch (Exception) { };
                
                
                
                
                /*
                foreach (Label l in remove_LinkedList)
                {
                    Controls.Remove(l);
                  //  comment_storage.Remove(l);
                }

                remove_LinkedList.Clear();
                  */
            }
        
        }
        void moveComment_thread() {
            if (time_counter % speed_control == 0)
            {

/*
                    foreach (Label l in Controls.OfType<Label>())
                    {

                        MoveLabel_threadEX(l);


                    }

*/
                    try
                    {
                        for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++)
                        {

                            MoveLabel_threadEX(fm3.Controls.OfType<Label>().ElementAt(i));



                        }
                    }
                    catch (Exception) { }


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
      void  removebyDel_DM(LinkedList<String[]> dm,ListBox mbox,bool readxml){



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


            if (DM_LinkedList != null)
            {
                fm2.setDMList = DM_LinkedList;
                //   fm2.setDMList(DM_LinkedList);
            }
            if (Media_LinkedList != null)
            {
                fm2.setMediaList = Media_LinkedList;
                //fm2.setMediaList(Media_LinkedList);
            }
            if (FullDM_LinkedList != null)
            {
                fm2.setFullDMList = FullDM_LinkedList;
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
                    removebyDel_DM(FullDM_LinkedList, mbox,false);
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



                    removebyDel_DM(DM_LinkedList,mbox,true);

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
            // set del key action here if del is pressed with an item selected then remove the selected item from the Media Linkedlist and media list box

            //also probably add a function to play the media selected if enter key is pressed instead of double clicking it ?

            ListBox mbox = (ListBox)sender;



            switch (e.KeyValue) { 
            
            
                case 13:
                    //when enter key is let go
                    runSelectedVido(sender,e);
                    break;


                case 46:
                    //when del key is let go
        
                        for (int i = 0; i < Media_LinkedList.Count(); i++)
                        {
                            foreach (object l in mbox.SelectedItems)
                            {
                                if (Media_LinkedList.ElementAt(i)[1].Equals(l.ToString()))
                                {

                                    Media_LinkedList.Remove(Media_LinkedList.ElementAt(i));
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
            for (int i = 0; i < DM_LinkedList.Count(); i++) {

                
                if (DM_LinkedList.ElementAt(i)[1].Equals(cbox.SelectedItem.ToString())) {

                    has = true;
                
                }
            
            
            }
            if (!has) {

                for (int i = 0; i < FullDM_LinkedList.Count(); i++) {

                    if (cbox.SelectedItem.ToString().Equals(FullDM_LinkedList.ElementAt(i)[1])) {

                        DM_LinkedList.AddLast(FullDM_LinkedList.ElementAt(i));
                        fm2.setDMListBox.Items.Add(cbox.SelectedItem.ToString());
                        readXML(FullDM_LinkedList.ElementAt(i)[0]);
                    
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
                for (int i = 0; i < DM_LinkedList.Count(); i++)
                {

                    if (DM_LinkedList.ElementAt(i)[1].Equals(dmbox.SelectedItem.ToString()))
                    {

                        DM_LinkedList.Remove(DM_LinkedList.ElementAt(i));
                    }
                    else
                    {

                        readXML(DM_LinkedList.ElementAt(i)[0]);
                    }

                }
                dmbox.Items.Remove(dmbox.SelectedItem);

            }
            catch (NullReferenceException) { };
        
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

            for (int i = 0; i < Media_LinkedList.Count(); i++)
            {

                if (Media_LinkedList.ElementAt(i)[1].Equals(mtemp.SelectedItem.ToString()))
                {

                    result = Media_LinkedList.ElementAt(i)[0];

                }


            }
            if (result != "")
            {
                FileInfo newMedia = new FileInfo(result);
                String current = vlcPlayer.GetCurrentMedia().Mrl;
                vlcPlayer.SetMedia(newMedia);
                vlcPlayer.Play();

                //unload the loaded dm files, to avoid using the wrong dm on different media files?

                if (!current.Equals(vlcPlayer.GetCurrentMedia().Mrl))
                {
                    comment2.Clear();
                    comment.Clear();
                    DM_LinkedList.Clear();
                    _duplicates = 0;

                    if (fm2 != null)
                    {
                        fm2.setDMListBox.Items.Clear();

                    }

                }

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

                fm3.Owner = this;

                
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
                    catch (NullReferenceException) { }
                
                
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

            if (comment2.Count > 0 && Media_LinkedList.Count > 0)
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
                vlcSound_button.BackgroundImage = Properties.Resources.mute;
                vlcPlayer.Audio.IsMute = true;

            }
            else
            {

                vlcSound_button.BackgroundImage = Properties.Resources.sound;

                vlcPlayer.Audio.IsMute = false;
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
            if (Media_LinkedList.Count > 1) {
                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                FileInfo nfile = new FileInfo(nuri.LocalPath);
                String current = nfile.Name;
                int currentindex = -2;

                for (int i = 0; i < Media_LinkedList.Count; i++) {

                    if (Media_LinkedList.ElementAt(i)[1].Equals(current)) {
                        currentindex = i;
                    
                    }
                
                
                }

                if (currentindex + 1 < Media_LinkedList.Count)
                {

                    currentindex++;

                }
                else {
                    currentindex = 0;
                
                }

                FileInfo nfile2 = new FileInfo(Media_LinkedList.ElementAt(currentindex)[0]);
                if (vlcPlayer.IsPlaying) {
                    vlcPlayer.Stop();
                }
                vlcPlayer.SetMedia(nfile2);
                comment2.Clear();
                comment.Clear();
                playedcomment = 0;
                _duplicates = 0;
                time_counter = 0;
                setVLCname(nfile2);
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
                for (int i = 0; i < Media_LinkedList.Count; i++) {
                    if (Media_LinkedList.ElementAt(i)[1].Equals(filename)) { 
                    
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
            if (Media_LinkedList.Count > 1)
            {
                Uri nuri = new Uri(vlcPlayer.GetCurrentMedia().Mrl);
                FileInfo nfile = new FileInfo(nuri.LocalPath);
                String current = nfile.Name;
                int currentindex = -2;

                for (int i = 0; i < Media_LinkedList.Count; i++)
                {

                    if (Media_LinkedList.ElementAt(i)[1].Equals(current))
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
                    currentindex = Media_LinkedList.Count-1;

                }

                FileInfo nfile2 = new FileInfo(Media_LinkedList.ElementAt(currentindex)[0]);
                if (vlcPlayer.IsPlaying)
                {
                    vlcPlayer.Stop();
                }
                vlcPlayer.SetMedia(nfile2);
                comment2.Clear();
                comment.Clear();
                playedcomment = 0;
                _duplicates = 0;
                time_counter = 0;
                setVLCname(nfile2);
                timerStop();
                vlcPlayer.Play();
              

            }
        }


        //write to XML file to save user changes to settings?



    }
}
