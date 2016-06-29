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
       
        public Form2()
        {
            InitializeComponent();

           

            this.ClientSizeChanged += new EventHandler(Form2_SizeChanged);
           
            
        }



        // when the size of the window is changed the tab page will autofill the whole form
        void Form2_SizeChanged(object sender, EventArgs e)
        {
            tab_list.Size = new Size(ClientRectangle.Right, ClientRectangle.Bottom);
            Media_List_Box.Size = new Size(Media_List.ClientRectangle.Right,Media_List.ClientRectangle.Bottom);
            DM_List_Box.Size = new Size(DM_List.ClientRectangle.Right, DM_List.ClientRectangle.Bottom);
        }

        public void writeDMdir(string c){
            DM_List_Box.Items.Add(c);
        
        }

    }
}
