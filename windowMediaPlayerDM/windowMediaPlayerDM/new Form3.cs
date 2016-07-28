﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace windowMediaPlayerDM
{
    public partial class Neo_Comment_window : Form
    {
        //instead of creating the comment on the main thread
        // do it here
        // and move the comments here
        // (that's if every frame runs on a differnt thread)
        // if not then have to create a thread that moves comments here
        // to fix the cross thread problem

        List<Label> comment_storage;
        int move;
        Form1 fm1 = new Form1();
        int interval;
        BackgroundWorker bk201;
        bool playing;
        int move_distance;
        int _distance;
        public Neo_Comment_window()
        {
            InitializeComponent();

            move = -1;
            this.TransparencyKey = Color.AliceBlue;
            this.BackColor = Color.AliceBlue;

            playing = false;
            move_distance = 20;
            _distance = 20;
            this.Size = fm1.currentMediaWindowSize;
            this.Location = fm1.currentMediaWindowLocation;
            this.Show();
            this.TopMost = false;
            this.BringToFront();
            this.ShowInTaskbar = false;
            comment_storage = new List<Label>();
            userColor = Color.DarkGray;
            fullscreen = false;
            interval = 60;
            bk201 = new BackgroundWorker();
            bk201.WorkerSupportsCancellation = true;
            bk201.DoWork += new DoWorkEventHandler(bk201_DoWork);
            
        }

        void changeingSpeed() {
            int lnumber = this.comment_storage.Count * 8;
            if (lnumber > 180)
            {

                move_distance = (int)(_distance * 10);

            }
            else if (lnumber > 110)
            {

                move_distance = (int)(_distance * 4);

            }
            else if (lnumber > 80)
            {

                move_distance = (int)(_distance * 2.8);
            }
            else if (lnumber > 70)
            {

                move_distance = (int)(_distance * 2.5);
            }
            else if (lnumber > 60)
            {
                move_distance = (int)(_distance * 2.1);
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
            else if (lnumber > 20)
            {

                move_distance = (int)(_distance * 1.1);
            }
            else
            {

                move_distance = _distance;
            }
        
        }

        //get the move distance form main form
        public int setMoveDistance {
            get { return _distance; }
            set { _distance = value; }
        
        }

        void bk201_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            while (playing) {


                changeingSpeed();
                movecomment = move_distance;


                System.Threading.Thread.Sleep(interval);
            }
        }
        public bool work {

            get { return playing; }
            set {
                if (value == true)
                {

                    playing = true;
                    if (!bk201.IsBusy) {

                        bk201.RunWorkerAsync();
                    
                    }
                }
                else {
                    playing = false;
                    bk201.CancelAsync();
                
                }
            
            }
        
        }
        public int setInterval {

            get { return interval; }
            set { interval = value; }
        }
        public int movecomment {

            get {return move; }
            set{
                //move comments here
                if(value != -1){
                    List<Label> remove = new List<Label>();
                    for (int i = 0; i < comment_storage.Count; i++) {

                        moveLabel2(comment_storage.ElementAt(i), value);
                        if (comment_storage.ElementAt(i).IsDisposed) { 
                        remove.Add(comment_storage.ElementAt(i));
                        
                        }
                    
                    }
             
                        for (int i = 0; i < remove.Count; i++)
                        {

                            comment_storage.Remove(remove.ElementAt(i));
                        }
                    
                    remove.Clear();

            };
            }
        
        
        
        }
        delegate void moveLabel_thread(Label l, int distance);

        void moveLabel2(Label l, int distance) {

            if (this.InvokeRequired)
            {
                moveLabel_thread c = new moveLabel_thread(moveLabel2);

                this.Invoke(c, new object[] {l,distance });


            }
            else {

                moveLabel(l, distance);
            }
        
        
        }

        void moveLabel(Label l,int distance) {
            int x = l.Location.X;
            int xend = 0 - l.Size.Width;
            if (x > xend)
            {
                //move the comment by distance if it's before xend
                l.Location = new Point(x - distance, l.Location.Y);
            }
            else {

                l.Dispose();
            
            }
        }
        bool fullscreen;
        Color userColor;
        int ycurrent;
        public String createLabel{
            // add the comment to the list and the controls
            // returns the newest comment ( latest set comment if read
            set
            {

                Label dm = new Label();

                //

                dm.Text = value;
                dm.Name = value;
                dm.TabIndex = 3;
                dm.Visible = true;
                dm.AutoSize = true;

                dm.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Bold);
                dm.ForeColor = userColor;


                Random ypos = new Random();

                if (!fullscreen)
                {
                    if (40 < this.Size.Height - (80 + dm.Size.Height))
                    {
                        ycurrent = ypos.Next(40, this.Size.Height - (80 + dm.Size.Height));//fm3.ClientRectangle.Bottom
                    }
                    else
                    {

                        ycurrent = 40;
                    }
                }
                else
                {


                    ycurrent = ypos.Next(this.ClientRectangle.Top, this.ClientRectangle.Bottom - dm.Size.Height - 80);

                }

                dm.Location = new Point(ClientRectangle.Right, ycurrent);

                this.comment_storage.Add(dm);
                this.Controls.Add(dm);
                dm.Show();
                 }
            get { return this.comment_storage.Last().Text; }
        
        
        }
        //gets the comment storage of this form 
        public List<Label> setStorage {
            set { this.comment_storage = value; }
            get { return this.comment_storage; }
        
        }
        public Point setLocation{

            set { this.Location = value; }

            get { return this.ClientRectangle.Location ;}
        
        
        
        }
        public bool setfullscreen{
            get { return fullscreen; }
            set { fullscreen = value; }
    
    }
        public Size setSize{

            set { this.Size = value; }

            get{ return this.Size;}
    
    
         }
        public bool setTopMost {

            set { this.TopMost = value; }

            get { return this.TopMost; }
        
        
        }




    }


}
