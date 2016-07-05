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
    public partial class Comment_window : Form
    {

        Form1 fm1 = new Form1();
        public Comment_window()
        {
            InitializeComponent();


            this.TransparencyKey = Color.AliceBlue;
            this.BackColor = Color.AliceBlue;
            
            this.Size = fm1.currentMediaWindowSize;
            this.Location = fm1.currentMediaWindowLocation;
            this.Show();
            this.TopMost = false;
            this.BringToFront();
            this.ShowInTaskbar = false;
            
        }


        public Point setLocation{

            set { this.Location = value; }

            get { return this.ClientRectangle.Location ;}
        
        
        
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