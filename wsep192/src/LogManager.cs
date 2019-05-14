using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace src
{
    class LogManager
    {
        private static LogManager instance;

        private String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MarketLog.txt");


        private LogManager()
        {
            // Delete the file if it exists.
            if (!File.Exists(path))
            {
                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
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

            using (var streamWriter = new StreamWriter(path, true))
            {
                DateTime localDate = DateTime.Now;
                streamWriter.WriteLine(localDate.ToString() + ": " + str);
            }
        }

        public void OpenAnewLogFile()
        {
            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create the file.
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }


    class ErrorManager
    {
        private static ErrorManager instance;
        private String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MarketError.txt");

        private ErrorManager()
        {
            // Delete the file if it exists.
            if (!File.Exists(path))
            {
                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        public static ErrorManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ErrorManager();
                return instance;

            }
        }

        public void WriteToLog(String str)
        {
            using (var streamWriter = new StreamWriter(path, true))
            {
                DateTime localDate = DateTime.Now;
                streamWriter.WriteLine(localDate.ToString() + ": " + str);
            }
        }

        public void OpenAnewLogFile()
        {
            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create the file.
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}