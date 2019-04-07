using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace src
{
    class LogManager
    {
        private static LogManager instance;

        private static String path = @"C:\Logs\MarketLog.txt";

        private LogManager()
        {
            System.IO.File.WriteAllText(path, "");
        }

        public static LogManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new LogManager();
                return instance;

            }
        }

        public void WriteToLog(String str)
        {
            DateTime localDate = DateTime.Now;
            System.IO.File.AppendAllText(path, localDate.ToString() + ": " + str + System.Environment.NewLine);
        }
        public void OpenAnewLogFile()
        {
            System.IO.File.WriteAllText(path, "");
        }

    }
}
