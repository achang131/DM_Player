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


namespace windowMediaPlayerDM
{
    public partial class Form1 : Form{
        string media_dir;
         string danmoku_dir;
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
        LinkedList<Label> remove_LinkedList = new LinkedList<Label>();
        int time_offset;
        int move_distance;
        int playedcomment;
        LinkedList<String[]> Media_LinkedList = new LinkedList<String[]>();
        LinkedList<String[]> DM_LinkedList = new LinkedList<String[]>();
        bool sommentswitch;
        int commentdestroy;
        
        int vpos;

        bool threading_mode;

        delegate void tmovelabel(Label l,int x, int y);

        delegate void exmovelabel(Label l);

        delegate void commentEnginecomp();

        Dictionary<int, String> comment2 = new Dictionary<int, string>();

        TaskFactory th1 = new TaskFactory();

        public DispatcherTimer newtimer = new DispatcherTimer();

    //    public DispatcherTimer newTimer2 = new DispatcherTimer();

        public System.Timers.Timer newTimer2 = new System.Timers.Timer();

      //  Form2 fm2;

        //



        int replacetimer1_interval;

        BackgroundWorker replacetimer1;

        bool _isPlaying;

        Comment_window fm3;

        bool multi_dm_mode;


        // Next try add setting (new window)  and Play/DM LinkedList(new window possible tabs ?)
        public Form1()
        {
            InitializeComponent();

            media = new OpenFileDialog();
            danmoku = new OpenFileDialog();
            danmoku.Filter = "xml |*.xml";

           

            playedcomment = 0;


            multi_dm_mode = false;
            if (multi_dm_mode)
            {

                setDMsToolStripMenuItem.Text = setDMsToolStripMenuItem.Text + " Multi On";

            }
            else {

                setDMsToolStripMenuItem.Text = setDMsToolStripMenuItem.Text + "Multi Off";
            }
          //  fm2 = new Form2();

            // controls the speed of the DM  default at 5? the lesser the faster 

            //comment settings

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

         //   newtimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            _isPlaying = true;

            newtimer.Tick += new EventHandler(newtimer_Tick);

        //    newTimer2.Tick += new EventHandler(newTimer2_Tick);

            newTimer2.Elapsed += new System.Timers.ElapsedEventHandler(newTimer2_Elapsed);

  //          newtimer.Interval= TimeSpan.FromMilliseconds(.1);

        //    newTimer2.Interval = TimeSpan.FromMilliseconds(.1);

            newTimer2.Interval = .1;

            //
          
            Media_Player.ClickEvent += new AxWMPLib._WMPOCXEvents_ClickEventHandler(Media_Player_ClickEvent);
            //initialize the y value to 40 so it will default start at 40
            ycurrent = 40;
            xstart = ClientRectangle.Right;
            Media_Player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Media_Player_PlayStateChange);


            this.ClientSizeChanged += new EventHandler(Form1_ClientSizeChanged);

            //timer1.interval default at 100 milli second
            time_counter = 0;

            Media_Player.windowlessVideo = true;

            Media_Player.stretchToFit = true;

            

           /*
            
            this.th1.StartNew(()=>{
                this.runEngine(2);
            
            });


           */

            replacetimer1 = new BackgroundWorker();
            replacetimer1.DoWork += new DoWorkEventHandler(replacetimer1_DoWork);
            

            this.vposChangingEvent += new PropertyChangingEventHandler(Form1_vposChangingEvent);

            Media_Player.SendToBack();



            this.LocationChanged += new EventHandler(Form1_LocationChanged);

            



            
        }

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {

            print(e.Delta.ToString());
            switch (e.Delta)
            {
                case 120:

                    Media_Player.settings.volume++;

                    break;


                case -120:

                    Media_Player.settings.volume--;

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


            while (_isPlaying)
            {

 //               moveComment_thread();
                commentEngine_thread();

                moveComment_thread();

                System.Threading.Thread.Sleep(replacetimer1_interval);

            }
        
        
        }


        void Form1_vposChangingEvent(object sender, PropertyChangingEventArgs e)
        {
        //    commentEngine();
        //    moveComment();
        }

        string positionChanging {
            get { return (Media_Player.Ctlcontrols.currentPositionString); }

            set {
                sendvposChangingEvent(value);
               Media_Player.Ctlcontrols.currentPosition = Int32.Parse(value);
            
            }
        
        
        }

        private void sendvposChangingEvent(string pos) {
            if (this.positionChanging !="00:00:00") {

                this.vposChangingEvent(this, new PropertyChangingEventArgs(pos));
            }
        
        }

        public event PropertyChangingEventHandler vposChangingEvent;

        void newTimer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (threading_mode)
            {
 //               moveComment_thread();
            }
           // commentEngine();

           // commentEngine_thread();
        }

        void newTimer2_Tick(object sender, EventArgs e)
        {
            if (threading_mode)
            {
                moveComment();
            }
        }

        void newtimer_Tick(object sender, EventArgs e)
        {
//            commentEngine();


//            moveComment();


//            moveComment_thread();

           // moveComment();

        }
        void runEngine(int a) {
            while (a != 1) {

                commentEngine();
            
            
            }
        
        
        }

        private void backgroundWorker1_RunWorkerCompleted(
    object sender,
    RunWorkerCompletedEventArgs e)
        {
            runEngine(2);
        }

        public int newTimer {

            get { return (int)(Media_Player.Ctlcontrols.currentPosition*100); }
            set{


                Media_Player.Ctlcontrols.currentPosition = value;
                if (Media_Player.Ctlcontrols.currentPosition != 1)
                {

                    commentEngine();
                
                }
            
            }
        
        
        }
        void changeSpeed(int s) {

            speed_control = s;
        
        }

        //load xml into a LinkedList and foreach addcomment ?
        void addComment(int time,string comment) { 


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

        void addComment(int currenttime, int time, string comment) {

            if (currenttime == time) {

                playedcomment++;
                createLabel(comment);
                
            }
        }

        void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            Media_Player.Size = new System.Drawing.Size(ClientRectangle.Width,ClientRectangle.Height-50);

            fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
            fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
            
        }

        // form1 ends

        void Media_Player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (Media_Player.playState.ToString().Equals("wmppsPlaying"))
            {
                timer1.Start();
                newtimer.Start();
                newTimer2.Start();
                _isPlaying = true;
                replacetimer1_start();

              //  while (Media_Player.playState.ToString().Equals("wmppsPlaying")) {

                  //  commentEngine();
                
                
                //}
               // newTimer;
            }else if(Media_Player.playState.ToString().Equals("wmppsStopped")){
                timer1.Stop();

                newtimer.Stop();

                newTimer2.Stop();
                playedcomment = 0;
                _isPlaying = false;
                replacetimer1_stop();

               // resetComment(comment_storage);
                resetComment();
            
            }
            else {
                timer1.Stop();


                newtimer.Stop();

                newTimer2.Stop();
                _isPlaying = false;
                replacetimer1_stop();
            }

            switch (e.newState) { 
                case 3:
                    commentEngine();
                    break;
            
                default:

                break;
            }
        }
        void createLabel(string comment) {
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
        public string External_Media_PlayerClick {

            set {
                
                 switch (value)
                 { 
                
                    case "play":
                        Media_Player.Ctlcontrols.play();
                    break;

                    case "stop":

                    Media_Player.Ctlcontrols.stop();
                    break;
                    case "pause":

                    Media_Player.Ctlcontrols.pause();
                    break;
                
                
                
                
                }
            
            }

            get { return Media_Player.playState.ToString(); }
        
        
        }
       public void Media_Player_ClickAction() {
            test_label.Text = Media_Player.playState.ToString();
            switch (Media_Player.playState.ToString())
            {

                case "wmppsStopped":
                    Media_Player.Ctlcontrols.play();
                    timer1.Start();

                    newtimer.Start();

                    newTimer2.Start();

                    _isPlaying = true;
                    replacetimer1_start();
                    break;

                case "wmppsPaused":
                    Media_Player.Ctlcontrols.play();
                    timer1.Start();


                    newtimer.Start();

                    newTimer2.Start();

                    _isPlaying = true;
                    replacetimer1_start();
                    break;

                case "wmppsPlaying":
                    Media_Player.Ctlcontrols.pause();
                    timer1.Stop();


                    newtimer.Stop();

                    newTimer2.Stop();

                    _isPlaying = false;
                    replacetimer1_stop();
                    break;
                case "wmppsReady":
                    Media_Player.Ctlcontrols.play();
                    timer1.Start();

                    newtimer.Start();

                    newTimer2.Start();

                    _isPlaying = true;
                    replacetimer1_start();
                    break;

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
                remove_LinkedList.AddLast(l);
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
                
                remove_LinkedList.AddLast(l);
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
                else
                {
                    //need to de comment this when the xml actually loads since it's going to add the comments on depending on the time counter real time


                    //  comment_storage.Remove(l);
                    remove_LinkedList.AddLast(l);
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
        
        private void setDMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fm3 != null)
            {
                fm3.Owner = this;

            }
            

            LinkedList<String[]> medias = setFile(media);

            bool multi_media;
            if (medias.Count > 1)
            {
                //multiple media file is selected;
                multi_media = true;
            }else{
            
            multi_media=false;
            }

            if (multi_media)
            {
                for (int i = 0; i < medias.Count(); i++)
                {
                    Media_Player.newPlaylist(medias.ElementAt(i)[1], medias.ElementAt(i)[0]);
                    Media_LinkedList.AddLast(medias.ElementAt(i));

                }
                media_dir = medias.ElementAt(0)[0];
                Media_status.Text = "Playlist Set";
                

            }else{
                if (medias != null)
                {
                    media_dir = medias.ElementAt(0)[0];
                    Media_status.Text = "Media Set";
                    Media_LinkedList.AddLast(medias.ElementAt(0));

                    if (fm3 != null) {

                        fm3.Owner = this;

                    }

                }
                else
                {
                    Media_status.Text = "No Media";
                }
                Media_Player.settings.autoStart=false;
                Media_Player.URL = media_dir;
                Media_Player.Ctlcontrols.stop();
                if (fm3 != null)
                {

                    fm3.Owner = this;
                }


            }
        }
        private void readXML(string danmoku_dir)
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
                        int vpos = Int32.Parse(temp_comment[0]);
                        if (comment2.ContainsKey(vpos))
                        {

                            string tempc = comment2[vpos] + Environment.NewLine + temp_comment[1];
                            playedcomment++;
                            comment2.Remove(vpos);
                            comment2.Add(vpos, tempc);


                        }
                        else
                        {
                            comment2.Add(vpos, temp_comment[1]);
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
        
        
        }
        private void setDMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (multi_dm_mode == false)
            {
                comment.Clear();
                comment2.Clear();


            }
            if (fm3 != null) {
                this.Owner = fm3;
            
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
                  DM_LinkedList.AddLast(danmokus.ElementAt(0));


                  //reader.name is the name of the element/attribute
                  //reader.value is the value of the attribute/text

                  readXML(danmoku_dir);

                  danmoku_dir = null;
                  Danmoku_status.Text = "DM Ready";

              }
              else {

                  for (int i = 0; i < danmokus.Count(); i++) {


                      danmoku_dir = danmokus.ElementAt(i)[0];
                      DM_LinkedList.AddLast(danmokus.ElementAt(i));
                      readXML(danmoku_dir);
                  
                  
                  }

                  danmoku_dir = null;
                  Danmoku_status.Text = "DM Ready";
              
              
              }
              if (fm3 == null)
              {
                  fm3 = new Comment_window();
                  fm3.setLocation = new Point(this.Location.X + 8, this.Location.Y + 59);
                  fm3.Size = new Size(Media_Player.ClientSize.Width, Media_Player.ClientSize.Height - 45);
                  fm3.MouseClick +=new MouseEventHandler(dm_MouseClick);
                  fm3.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
                  fm3.Owner = this;
              }
              else {
                  fm3.Owner = this;
              
              }
             

          }
          else {
              Danmoku_status.Text = "No DM";
              if (fm3 != null) {
                  fm3.Owner = this;
              }
          }
        }

        //modified from string[] to LinkedList so multiple files can be selected at once.

        private LinkedList<string[]> setFile(OpenFileDialog ofd){
            ofd.Multiselect = true;
        
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

                int end = ofd.FileNames.ElementAt(0).LastIndexOf("\\");
                string dir = ofd.FileNames.ElementAt(0).Substring(0, end);
                ofd.InitialDirectory = dir;
                LinkedList<String[]> temp = new LinkedList<string[]>();
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

        void print(string s) {

            debugtab.Text = s;
        
        }
        void print2(string s) {

            debugtab2.Text = s;
        }
        void makeComment() {

            time_counter++;
            //time_counter = (int)(Media_Player.Ctlcontrols.currentPosition * 100);
            int comment_time;
            try
            {
                 comment_time = (int)(Media_Player.Ctlcontrols.currentPosition * 100) + time_offset;
            }
            catch (Exception) { comment_time = 0; }
            print(comment_time.ToString() + "/" + time_counter.ToString());
            print2(playedcomment.ToString() + "/" + comment.Count.ToString());

            /*
            foreach(string[] c in comment){
                addComment(comment_time,Int32.Parse(c[0]), c[1]);
                        
            }
            */

            addComment(comment_time, comment2);

        
        }
        void moveComment() {


            if (time_counter % speed_control == 0)
            {
 /*               foreach (Label l in Controls.OfType<Label>())
                {

                    MoveLabel(l);


                }

*/
                for (int i = 0; i < fm3.Controls.OfType<Label>().Count(); i++) {


                    MoveLabel(fm3.Controls.OfType<Label>().ElementAt(i));
                
                
                
                }
                
                
                
                
                
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
                try
                {

/*
                    foreach (Label l in Controls.OfType<Label>())
                    {

                        MoveLabel_threadEX(l);


                    }

*/

                    for (int i = 0; i < fm3.Controls.OfType<Label>().Count();i++ )
                    {

                        MoveLabel_threadEX(fm3.Controls.OfType<Label>().ElementAt(i));



                    }




                    /*
                    foreach (Label l in remove_LinkedList)
                    {
                        Controls.Remove(l);
                        //comment_storage.Remove(l);
                    }

                    remove_LinkedList.Clear();
                     */
                }catch(InvalidOperationException){
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
                catch (Exception) { 
                
                
                }



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

        private void Media_DM_menu_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();

            if (fm3 != null)
            {
                fm2.Owner = fm3;
            }
            fm2.Disposed += new EventHandler(fm2_Disposed);


            if (DM_LinkedList != null)
            {

                fm2.setDMList(DM_LinkedList);
            }
            if (Media_LinkedList != null)
            {

                fm2.setMediaList(Media_LinkedList);
            }
            fm2.Show();
         
            
        }
        
        void fm2_Disposed(object sender, EventArgs e)
        {
            if (fm3 != null)
            {

                fm3.Owner = this;

                
            }
        }
       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void openMeidaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setDMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // loads multiple XML files here
            multi_dm_mode = true;
            setDMsToolStripMenuItem.Text = "Set DMs" + " Multi On";

        }
    }
}
