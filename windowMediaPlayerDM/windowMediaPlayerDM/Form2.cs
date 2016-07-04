using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace windowMediaPlayerDM
{
    public partial class Form2 : Form
    {
        public LinkedList<String[]> DM_L = new LinkedList<string[]>();
        public LinkedList<String[]> Media_L = new LinkedList<string[]>();


        public Form2()
        {
            InitializeComponent();

           

            this.ClientSizeChanged += new EventHandler(Form2_SizeChanged);


            this.ShowInTaskbar = false;
           
            
        }



        // when the size of the window is changed the tab page will autofill the whole form
        void Form2_SizeChanged(object sender, EventArgs e)
        {
            tab_list.Size = new Size(ClientRectangle.Right, ClientRectangle.Bottom);
            Media_List_Box.Size = new Size(Media_List.ClientRectangle.Right,Media_List.ClientRectangle.Bottom);
            DM_List_Box.Size = new Size(DM_List.ClientRectangle.Right, DM_List.ClientRectangle.Bottom);
        }
        /*
        public void setDMList(LinkedList<String[]> s) {
            DM_L = s;
            foreach (string[] c in DM_L)
            {
                DM_List_Box.Items.Add(c[1]);

            }
        
        }
         */

        public LinkedList<String[]> setDMList {

            set
            {
                DM_List_Box.Items.Clear();
                DM_L = value;
                for (int i = 0; i < value.Count();i++ ) {

                    DM_List_Box.Items.Add(value.ElementAt(i)[1]);
                
                }
            
            }
            get { return DM_L; }
        
        
        
        }



        public LinkedList<String[]> setMediaList
        {

            set
            {
                Media_List_Box.Items.Clear();
                Media_L = value;
                for (int i = 0; i < value.Count(); i++)
                {

                    Media_List_Box.Items.Add(value.ElementAt(i)[1]);

                }

            }
            get { return Media_L; }



        }

        public LinkedList<String[]> getDMList() {

            return DM_L;
        
        }
        /*
        public void setMediaList(LinkedList<String[]> s)
        {
            Media_L = s;
            foreach (string[] c in Media_L) {
                Media_List_Box.Items.Add(c[1]);
            
            }

        }
         */
        public LinkedList<String[]> getMediaList()
        {

            return Media_L;

        }

        public void writeMediadir(string c) {
            Media_List_Box.Items.Add(c);
        }
        public void writeDMdir(string c){
            DM_List_Box.Items.Add(c);
        
        }

    }
}
