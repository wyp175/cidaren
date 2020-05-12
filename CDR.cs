using System;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using Fiddler;

namespace FiddlerUtility{

      public class Program
      {
            string Text;
            public void setText(string textData)
            {
                  // For this example, the data to be placed on the clipboard is a simple
                  // string.
                  Text = textData;
                  // Console.ReadKey();

            }

            public void SetTextData()
            {
                  // After this call, the data (string) is placed on the clipboard and tagged
                  // with a data format of "Text".
                  Clipboard.SetData(DataFormats.Text, (Object)Text);
            }
      }
       


      public static class CDR
      {
            public static string download_url = "http://cidaren.mcol.cc/dl/";

            static CDR(){
                  
            }

            public static bool setText(string textData){
                  Program obj = new Program();
                  Thread set = new Thread(obj.SetTextData);
                  obj.setText(textData);
                  set.SetApartmentState(ApartmentState.STA);
                  set.Start();
                  // set.Abort();
                  return true;
            }

            public static bool downloadFiles(string URL, string path, string name){
                  string locapath = CONFIG.GetPath(path)+name;
                  return DownloadFile(URL, locapath);
            }

            public static bool downloadUpdate(string version){
                  string locapath = CONFIG.GetPath("Scripts");
                  string url=download_url+"update.php?v="+version;
                  return DownloadFile(url, locapath);
            }


            ///<summary>
            /// 下载文件
            /// </summary>
            /// <param name="URL">下载文件地址</param>
            /// <param name="Filename">下载后另存为（全路径）</param>
            private static bool DownloadFile(string URL, string filename)
            {
                  try{

                        HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                        HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                        Stream st = myrp.GetResponseStream();
                        Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, (int)by.Length);
                        while (osize > 0)
                        {
                              so.Write(by, 0, osize);
                              osize = st.Read(by, 0, (int)by.Length);
                        }
                        so.Close();
                        st.Close();
                        myrp.Close();
                        Myrq.Abort();
                        return true;
                  }catch{
                        return false;
                  }
            }
      }

}