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
      //  LinkedList<Label> comment_storage = new LinkedList<Label>();
        XmlTextReader dm_comment;
        LinkedList<String[]> comment = new LinkedList<String[]>();
        LinkedList<TransparentLabel> remove_LinkedList = new LinkedList<TransparentLabel>();
        int time_offset;
        int move_distance;
        int playedcomment;
        LinkedList<String[]> Media_LinkedList = new LinkedList<String[]>();
        LinkedList<String[]> DM_LinkedList = new LinkedList<String[]>();
        bool sommentswitch;
        int commentdestroy;

        int vpos;

        bool threading_mode;

        delegate void tmovelabel(TransparentLabel l,int x, int y);

        delegate void exmovelabel(TransparentLabel l);

        delegate void commentEnginecomp();

        Dictionary<int, String> comment2 = new Dictionary<int, string>();

        TaskFactory th1 = new TaskFactory();

        public DispatcherTimer newtimer = new DispatcherTimer();

    //    public DispatcherTimer newTimer2 = new DispatcherTimer();

        public System.Timers.Timer newTimer2 = new System.Timers.Timer();

      //  Form2 fm2;

        //

        BackgroundWorker replacetimer1;

        bool _isPlaying;

        // Next try add setting (new window)  and Play/DM LinkedList(new window possible tabs ?)
        public Form1()
        {
            InitializeComponent();

            media = new OpenFileDialog();
            danmoku = new OpenFileDialog();
            danmoku.Filter = "xml |*.xml";

            playedcomment = 0;

          //  fm2 = new Form2();

            // controls the speed of the DM  default at 5? the lesser the faster 

            //comment settings

            speed_control = 1;

            time_offset = 0;

            move_distance = 6;

            timer1.Interval = 1;

            commentdestroy = -200;

            sommentswitch = true;

            threading_mode = false;

            vpos = -1;

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

           /*
            
            this.th1.StartNew(()=>{
                this.runEngine(2);
            
            });


           */

            replacetimer1 = new BackgroundWorker();
            replacetimer1.DoWork += new DoWorkEventHandler(replacetimer1_DoWork);
            

            this.vposChangingEvent += new PropertyChangingEventHandler(Form1_vposChangingEvent);

            Media_Player.SendToBack();









            
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

                System.Threading.Thread.Sleep(2);

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
            TransparentLabel dm = new TransparentLabel();
            
            //
            Random ypos = new Random();
            if (40 < ClientRectangle.Bottom - 80)
            {
                ycurrent = ypos.Next(40, ClientRectangle.Bottom - 80);
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
            dm.Font = new Font("Microsoft Sans Serif", 20);
            dm.ForeColor = Color.White;
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

            Controls.Add(dm);

           // safecontrol(dm);

           // comment_storage.Add(dm);
            dm.BringToFront();
            dm.Show();
        
        }

        void safecontrol(TransparentLabel dm) {

            if (this.InvokeRequired)
            {


            }
            else {

                this.Controls.Add(dm);
            }
        
        }

        void Media_Player_ClickAction() {
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
            TransparentLabel temp = (TransparentLabel)sender;
            temp.BringToFront();
            break;

            default:

            Media_Player_ClickAction();
            
            break;

        }

            
                        
        }
        void resetLabelPos(TransparentLabel l) {
            l.Location = new Point(ClientRectangle.Right, l.Location.Y);
        
        }
        void MoveLabel(TransparentLabel l) {
            
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

        void MoveLabel_thread(TransparentLabel l) {

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
        void MoveLabel_threadEX(TransparentLabel l){

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
            foreach (TransparentLabel l in Controls.OfType<TransparentLabel>()) {

                resetLabelPos(l);
            }
        
        
        }
        void resetComment(LinkedList<TransparentLabel> l) {

            foreach (TransparentLabel comment in l) {
                resetLabelPos(comment);
            
            }
        
        }
        void Media_Player_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            Media_Player_ClickAction();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void MediaPlayerClick(object sender, MouseEventArgs e) {
            test_label.Text = Media_Player.playState.ToString();
        
        }
        private void setDMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] medias = setFile(media);
           
            
           
            if (medias[0] != null)
            {
                media_dir = medias[0];
                Media_status.Text = "Media Set";
                Media_LinkedList.AddLast(medias);
                
            }
            else {
                Media_status.Text = "No Media";
            }
            Media_Player.URL = media_dir;
            Media_Player.Ctlcontrols.stop();
        }

        private void setDMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            comment.Clear();
            comment2.Clear();

            String[] danmokus = setFile(danmoku);
           
          
          
           String [] temp_comment = new String[2];


           if (danmokus[0] != null)
          {
              danmoku_dir = danmokus[0];
              Danmoku_status.Text = "DM Set";
              DM_LinkedList.AddLast(danmokus);
              dm_comment = new XmlTextReader(danmoku_dir);
              
              //reader.name is the name of the element/attribute
              //reader.value is the value of the attribute/text

              while (dm_comment.Read()) { 
              
              switch(dm_comment.NodeType){
              
                  case XmlNodeType.Element:



                      while(dm_comment.MoveToNextAttribute()){

                          //get the time value if the attribute is vpos
                          if (dm_comment.Name == "vpos") {
                              temp_comment[0] = dm_comment.Value;
                              
                          }
                      
                      }
                  break;
                  
                  case XmlNodeType.Text:
                  temp_comment[1] = dm_comment.Value;
                      comment.AddLast(temp_comment);
                      int vpos = Int32.Parse(temp_comment[0]);
                      if (comment2.ContainsKey(vpos)) {

                          string tempc = comment2[vpos]+Environment.NewLine+temp_comment[1];
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
                  if (dm_comment.Name == "</chat>") {

                      temp_comment = new String[2];
                  
                  }

                  break;
             
              
              
              }
              }
              danmoku_dir = null;
              Danmoku_status.Text = "DM Ready";

          }
          else {
              Danmoku_status.Text = "No DM";
          }
        }

        private string[] setFile(OpenFileDialog ofd){
        
        
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

                int end = ofd.FileName.LastIndexOf("\\");
                string dir = ofd.FileName.Substring(0, end);
                ofd.InitialDirectory = dir;
                String [] result = {ofd.FileName,ofd.SafeFileName};
                return result;

            
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

            int comment_time = (int)(Media_Player.Ctlcontrols.currentPosition * 100) + time_offset;

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
                for (int i = 0; i < Controls.OfType<TransparentLabel>().Count(); i++) {


                    MoveLabel(Controls.OfType<TransparentLabel>().ElementAt(i));
                
                
                
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

                    for (int i = 0; i < Controls.OfType<TransparentLabel>().Count();i++ )
                    {

                        MoveLabel_threadEX(Controls.OfType<TransparentLabel>().ElementAt(i));



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
            if (danmoku_dir != null)
            {

                fm2.setDMList(DM_LinkedList);
            }
            if (media_dir != null) {

                fm2.setMediaList(Media_LinkedList);
            }
            fm2.Show();
         
            
        }
       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
    }
}
