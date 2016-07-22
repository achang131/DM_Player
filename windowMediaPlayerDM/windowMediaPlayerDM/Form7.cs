using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace windowMediaPlayerDM
{
    

    
    public partial class Url_menu : Form
    {

        List<Uri> urls;
        List<string> urlTitles;
        String current_dir_url;
        Uri linkaddress;
        add_link_form fm6;
        Dictionary<Uri, Uri> UrlDictionary;

        
        public Url_menu()
        {
            InitializeComponent();

            download_bar.Dispose();

        }

        public TextBox setLinkbox{

            set { Link_box = value; }
            get { return Link_box; }
    
         }
        public Button getloadURLbutton
        {
            set { set_link_button = value; }
            get { return set_link_button; }

        }

        public ProgressBar getProgressbar {

            set { download_bar = value; }
            get { return download_bar; }
        }

        public ListBox getLinks {

            set { Link_listbox = value; }
            get { return Link_listbox; }
        
        }
        public ListBox getFileUrl {
            set { url_listbox = value; }
            get { return url_listbox; }
        }
        public ListBox getTitle {

            set { Title_listbox = value; }
            get { return Title_listbox; }
        
        }

        public ToolStripStatusLabel getDownloadstatus1 {

            set { downlaod_label1 = value; }
            get { return downlaod_label1; }
        
        }
        public ToolStripStatusLabel getDownloadstatus2 {
            set { download_label2 = value; }
            get { return download_label2; }
        
        }
        public Button reload {

            set { reload_button = value; }
            get { return reload_button; }
        
        }
    }
}
