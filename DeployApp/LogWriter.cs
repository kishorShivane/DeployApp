using System;
using System.IO;

namespace DeployApp
{
    public class LogWriter
    {
        private string dbfLogPath = System.Configuration.ConfigurationSettings.AppSettings["DBFLogPath"];
        private string SmartCardLogPath = System.Configuration.ConfigurationSettings.AppSettings["SmartCardLogPath"];
        public LogWriter()
        {
            
        }

        public void LogWriteSmartCard(string logMessage)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(SmartCardLogPath, true))
                {
                    Log(logMessage, tw);
                }
            }
            catch (Exception)
            {
            }
        }

        public void LogWriteDBF(string logMessage)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(dbfLogPath, true))
                {
                    Log(logMessage, tw);
                }
            }
            catch (Exception)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
            }
        }
    }
}
