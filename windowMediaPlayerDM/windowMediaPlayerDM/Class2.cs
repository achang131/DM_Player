using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Windows.Forms;


//how to implement
//use dictionary to hold the url as key and list of the movie urls as value
//
namespace windowMediaPlayerDM
{
    class getWebVideo
    {
        String fullcontent;
        Uri websiteLink;

        String fileUrl;
        String dlfilepath;

        String filestorage_dir;
        List<Uri> Allfiles= new List<Uri>();

        String effetiveLink;

        List<string> titles = new List<string>();
        String title;
        public getWebVideo(Uri url) {

            websiteLink = url;

            //System.Net.WebClient webclient = new System.Net.WebClient();

            loadInfo(websiteLink);
          title=  getTitle();
          titles.Add(title);
            getAllUrls();
        
        }

         public getWebVideo(Uri url , String dirpath) {

            websiteLink = url;
            filestorage_dir=dirpath;

            //System.Net.WebClient webclient = new System.Net.WebClient();

            loadInfo(websiteLink);
           title= getTitle();
           titles.Add(title);
            getAllUrls();
        }

        public void changeOnlineUrl(Uri url){

            websiteLink = url;

            loadInfo(websiteLink);
           title= getTitle();
           titles.Add(title);
            getAllUrls();

        
        }

        public void changePath(string dir) {

             filestorage_dir = dir;
         }
        void loadInfo(Uri url) {
          
            
            HttpWebRequest wrequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse wresponse = (HttpWebResponse)wrequest.GetResponse();

            //if you get response from the http site
            if (wresponse.StatusCode == HttpStatusCode.OK) {

                Stream recives = wresponse.GetResponseStream();
                StreamReader readers = null;

                //see if the page is unicoded
                if (wresponse.CharacterSet == null)
                {

                    readers = new StreamReader(recives);
                }
                else { 
                //if unicoded

                    readers = new StreamReader(recives, Encoding.GetEncoding(wresponse.CharacterSet));
                
                }
                //could use read line to get an array of results?
                fullcontent = readers.ReadToEnd();

                wresponse.Close();
                readers.Close();

                


            }

        }
        String getTitle() {

            string result="";
            string conetent = fullcontent;
            string t = "<title>";
            int start = fullcontent.IndexOf(t);
            int end = fullcontent.LastIndexOf("</title>");

            result = fullcontent.Substring(start+t.Length,end-(start+t.Length));

            return result;
        }

         String getbaseUrl(String fcontent) {

            String result = "";
            String firsttarget = "var movie_url = ";
            String firsttemp = "var display_movie_url = ";
            int length = firsttarget.Length;
            int start = fcontent.IndexOf(firsttarget);
            int end = fcontent.IndexOf(firsttemp);
            

            String tempresult = fcontent.Substring(start, end - start);

            start = tempresult.IndexOf("'")+1;
            end = tempresult.LastIndexOf(";")-1;

            result = tempresult.Substring(start, end - start);

            //http://s3.amazonaws.com/ksr/assets/012/985/562/4b9df98bfba5c3714537627d3d8ec91a_original.mp3?
            //http%3A%2F%2Fs3.amazonaws.com%2Fksr%2Fassets%2F012%2F985%2F562%2F4b9df98bfba5c3714537627d3d8ec91a_original.mp3%3F

            //basic put the gabbish data back to it's orignal form so the uri can read it
            result = result.Replace("%2F","/");
            result = result.Replace("%3A", ":");
            result = result.Replace("%3F","?");

            Allfiles.Add(new Uri(result));


            //check if result here is online if not then go find other links in full content

            return result;
        
        }

         void getAllUrls() {

             //var ary_spare_sources = {"spare":[{"lid":"1","src":"http%3A%2F%2Ffleetupload.com%2Fmp3embed-gv3o70fbhxv2.mp3%3F"},{"lid":"2","src":"http%3A%2F%2Fwww.indishare.com%2Fmp3embed-q52air4wrwze.mp3%3F"},{"lid":"3","src":"http%3A%2F%2Fwww.indishare.com%2Fmp3embed-65a4s05kdbq2.mp3%3F"},{"lid":"4","src":"http%3A%2F%2Fsolidshelf.com%2Fmp3embed-ihu5ny2zk2e1.mp3%3F"},{"lid":"5","src":"http%3A%2F%2Fs3-us-west-1.amazonaws.com%2Fspliceproduction%2Fpreviews%2Fa9007396-0639-2680-1624-e3364fa3ecd748a6721%2Fa0221a42-0abd-6d80-73f2-a9777c63b96a461da92.mp3%3F"}]};
             //{"lid":"1","src":"http%3A%2F%2Ffleetupload.com%2Fmp3embed-gv3o70fbhxv2.mp3%3F"},{"lid":"2","src":"http%3A%2F%2Fwww.indishare.com%2Fmp3embed-q52air4wrwze.mp3%3F"},{"lid":"3","src":"http%3A%2F%2Fwww.indishare.com%2Fmp3embed-65a4s05kdbq2.mp3%3F"},{"lid":"4","src":"http%3A%2F%2Fsolidshelf.com%2Fmp3embed-ihu5ny2zk2e1.mp3%3F"},{"lid":"5","src":"http%3A%2F%2Fs3-us-west-1.amazonaws.com%2Fspliceproduction%2Fpreviews%2Fa9007396-0639-2680-1624-e3364fa3ecd748a6721%2Fa0221a42-0abd-6d80-73f2-a9777c63b96a461da92.mp3%3F"}
             //
             // count "lid":"5
             // ,"src":
             //lid":"5"
             getbaseUrl(fullcontent);
           String  result = "";
           String  firsttarget = "var ary_spare_sources =";
           String  firsttemp = "var sendFailed =";
           int start = fullcontent.IndexOf(firsttarget);
           int end = fullcontent.IndexOf(firsttemp);
           String tempresult = fullcontent.Substring(start, end - start);


             start = tempresult.IndexOf("[");
             end = tempresult.IndexOf("]");
             tempresult = tempresult.Substring(start + 1, end - start);



             start = tempresult.LastIndexOf("lid\":");
             end = tempresult.LastIndexOf(",\"src\":");

             String Urltotalcount = tempresult.Substring(start, end - start);
             end = Urltotalcount.LastIndexOf("\"") - 1;
            Urltotalcount= Urltotalcount.Replace("\"", "");
            Urltotalcount= Urltotalcount.Replace("lid", "");
            Urltotalcount = Urltotalcount.Replace(":", "");
             int totalcount = Int32.Parse(Urltotalcount);


           //  throw new Exception("test view"){};

             for (int i = 0; i < totalcount; i++)
             {
                 start = tempresult.IndexOf("http");
                 end = tempresult.IndexOf("}");

                 //http%3A%2F%2Ffleetupload.com%2Fmp3embed-gv3o70fbhxv2.mp3%3F"
                 string urlt = tempresult.Substring(start, end - 1 - start);

                 urlt = urlt.Replace("%2F", "/");
                 urlt = urlt.Replace("%3A", ":");
                 urlt = urlt.Replace("%3F", "?");

                 Uri temp = new Uri(urlt);

                 Allfiles.Add(temp);


               //  throw new Exception("test view") { };

                 if (i != totalcount - 1)
                 {
                     tempresult = tempresult.Substring(end + 1, tempresult.Length - (end + 1));
                 }

             }
         
         }


        // other version of download file for form interaction

         public void downlaodFile(Uri filepath, WebClient wb)
         {
             //test download file code here
          //   WebClient wb = new WebClient();

         //    wb.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wb_DownloadFileCompleted);
         //    wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wb_DownloadProgressChanged);
             String fileU = filepath.AbsolutePath;

             int fstart = fileU.LastIndexOf("/") + 1;

             String filename = fileU.Substring(fstart, fileU.Length - fstart);

             FolderBrowserDialog fb = new FolderBrowserDialog();


             //if an actuall dir is set and it exist then it won't prompt up to ask user to set a dir

             DirectoryInfo ndir = new DirectoryInfo(filestorage_dir);



             if (!ndir.Exists)
             {

                 //selet a folder to hold the file

                 if (fb.ShowDialog() == DialogResult.OK)
                 {
                     FileInfo newfile = new FileInfo(fb.SelectedPath + "\\" + filename);

                     // if the file exists then stop download
                     if (!newfile.Exists)
                     {
                         wb.DownloadFileAsync(filepath, @newfile.FullName);


                         dlfilepath = newfile.FullName;
                     }
                     else
                     {


                         dlfilepath = newfile.FullName;
                     }
                 }
             }
             else
             {

                 FileInfo newfile = new FileInfo(ndir.FullName + "\\" + filename);

                 // if the file exists then stop download
                 if (!newfile.Exists)
                 {
                     wb.DownloadFileAsync(filepath, @newfile.FullName);


                     dlfilepath = newfile.FullName;

                 }
                 else
                 {

                     dlfilepath = newfile.FullName;
                 }
             }






             //throw a file not found exception if dlfilepath is null ?
             wb.Dispose();



         }


        //make user select path if a directory is not set in default ?

         void downlaodFile(Uri filepath) {
             //test download file code here
             using (WebClient wb = new WebClient())
             {

                 wb.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wb_DownloadFileCompleted);
                 wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wb_DownloadProgressChanged);
                 String fileU = filepath.AbsolutePath;

                 int fstart = fileU.LastIndexOf("/") + 1;

                 String filename = fileU.Substring(fstart, fileU.Length - fstart);

                 FolderBrowserDialog fb = new FolderBrowserDialog();


                 //if an actuall dir is set and it exist then it won't prompt up to ask user to set a dir

                 DirectoryInfo ndir = new DirectoryInfo(filestorage_dir);



                 if (!ndir.Exists)
                 {

                     //selet a folder to hold the file

                     if (fb.ShowDialog() == DialogResult.OK)
                     {
                         FileInfo newfile = new FileInfo(fb.SelectedPath + "\\" + filename);

                         // if the file exists then stop download
                         if (!newfile.Exists)
                         {
                             wb.DownloadFileAsync(filepath, @newfile.FullName);


                             dlfilepath = newfile.FullName;
                         }
                         else
                         {


                             dlfilepath = newfile.FullName;
                         }
                     }
                 }
                 else
                 {

                     FileInfo newfile = new FileInfo(ndir.FullName + "\\" + filename);

                     // if the file exists then stop download
                     if (!newfile.Exists)
                     {
                         wb.DownloadFileAsync(filepath, @newfile.FullName);


                         dlfilepath = newfile.FullName;

                     }
                     else
                     {

                         dlfilepath = newfile.FullName;
                     }
                 }






                 //throw a file not found exception if dlfilepath is null ?
                 wb.Dispose();
             }
         
         
         }
         int percentage = 0;
          double byte_recieved = 0;
         void wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
         {
             //throw new NotImplementedException();
             


         }
         bool inloop = false;
         bool fileNotzero=false;
         void wb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
         {
             //do something here update file state ?


         }
        //get and dl the file then return the local file path to play offline

         public List<Uri> getDownlist {

             get {
                   
                 return Allfiles ;
             }

             set {}
         
         }
         Uri playing;

         public Uri playDownlist {

             get { return new Uri(dlfilepath); }
             set {
                 playing = value;
                 downlaodFile(value); }
         
         }
         public String dlurl {
             get {

                     string t = getbaseUrl(fullcontent);

                     downlaodFile(new Uri(t));

                 return dlfilepath; }
             set
             {
                 websiteLink = new Uri(value);
                 loadInfo(websiteLink);
             }
         
         
         }
         bool checkFileOnline(Uri url) {
             bool result = true ;
             HttpWebResponse response=null;
             var request = (HttpWebRequest)WebRequest.Create(url.LocalPath);
             //set the request to head
             request.Method = "HEAD";

             try
             {
                  response = (HttpWebResponse)request.GetResponse();

             }
             catch (WebException ex)
             {
                 result = false;
             }
             finally {

                 if (response != null) {

                     response.Close();

                     result = true;
                 }
             }



             return result;
         }

         public string dlnameUpdate {

             get
             {
                 
                 int end = dlfilepath.LastIndexOf("\\");
                 dlfilepath = dlfilepath.Substring(0, end);
                 dlfilepath = dlfilepath + "\\" + title;
                    return dlfilepath; }
             set { dlfilepath = value; } 
         
         }


        //get the file place online
         public String getbasicUrl
         {
             get{return getbaseUrl(fullcontent);}
             set { websiteLink = new Uri(value);
             loadInfo(websiteLink);
             }

         }

        /*        List<Uri> Allfiles= new List<Uri>();

        String effetiveLink;

        List<string> titles = new List<string>();
         */

         public List<Uri> getAllfiles {
             get { return Allfiles; }
             set { Allfiles = value; }
         
         
         }
         public List<string> getTitles {
             get { return titles; }
             set { titles = value; }
         
         }
    }
}
