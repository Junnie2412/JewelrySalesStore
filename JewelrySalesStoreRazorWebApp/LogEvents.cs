
using System.IO;

namespace JewelrySalesStoreRazorWebApp
{
    public static class LogEvents
    {
        public static void LogToFile(string Title, string LogMessages, IWebHostEnvironment env)
        {
            bool exist = Directory.Exists(env.WebRootPath + "\\" + "LogFolder");
            if (!exist)
            {
                Directory.CreateDirectory(env.WebRootPath + "\\" + "LogFolder");
            }

            StreamWriter swlog;
            string logPath = "";
            
            string fileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
            logPath = Path.Combine(env.WebRootPath + "\\" + "LogFolder", fileName);

            if(!File.Exists(logPath))
            {
                swlog = new StreamWriter(logPath);
            }
            else
            {
                swlog = File.AppendText(logPath);
            }

            swlog.WriteLine("Log Entry");
            swlog.WriteLine("{0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            swlog.WriteLine("Message Title: {0}", Title);
            swlog.WriteLine("Log Message: {0}", LogMessages);
            swlog.WriteLine("-----------------------------------------");
            swlog.WriteLine("");
            swlog.Close();
        }
    }
}
