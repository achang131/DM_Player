using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;
using System.IO;


namespace windowMediaPlayerDM
{
    class comment_move_engine
    {
        List<Label> comment_storage = new List<Label>();
        BackgroundWorker bk201 = new BackgroundWorker();
        bool _playing;
        int interval;

        public comment_move_engine(List<Label> comments) {

            comment_storage = comments;
            _playing = false;
            interval = 15;
            bk201.WorkerSupportsCancellation = true;
            bk201.DoWork += new DoWorkEventHandler(bk201_DoWork);
           

        
        }

        void bk201_DoWork(object sender, DoWorkEventArgs e)
        {
           // throw new NotImplementedException();
            while (_playing) {







                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(interval));
            
            }

        }




        public void Start() {
        
        
        
        }

        public void Stop() { 
        
        
        }
    }
}
