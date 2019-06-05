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

        private String PresentionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MarketLog.txt");
        private static String path = @"../../../MarketLog.txt";


        private LogManager()
        {
            // Delete the file if it exists.
            if (!File.Exists(PresentionPath))
            {
                // Create the file.
                using (FileStream fs = File.Create(PresentionPath))
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
            try
            {
                DateTime localDate = DateTime.Now;
                System.IO.File.AppendAllText(path, localDate.ToString() + ": " + str + System.Environment.NewLine);
            }
            catch(Exception e)
            {
                using (var streamWriter = new StreamWriter(PresentionPath, true))
                {
                    DateTime localDate = DateTime.Now;
                    streamWriter.WriteLine(localDate.ToString() + ": " + str);
                }
            }

        }

        public void OpenAnewLogFile()
        {
            // Delete the file if it exists.
            if (File.Exists(PresentionPath))
            {
                File.Delete(PresentionPath);
            }

            // Create the file.
            using (FileStream fs = File.Create(PresentionPath))
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
        private String PresentionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MarketError.txt");
        private static String path = @"../../../MarketError.txt";

        private ErrorManager()
        {
            // Delete the file if it exists.
            if (!File.Exists(PresentionPath))
            {
                // Create the file.
                using (FileStream fs = File.Create(PresentionPath))
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
            try
            {
                DateTime localDate = DateTime.Now;
                System.IO.File.AppendAllText(path, localDate.ToString() + ": " + str + System.Environment.NewLine);
            }
            catch (Exception e)
            {
                using (var streamWriter = new StreamWriter(PresentionPath, true))
                {
                    DateTime localDate = DateTime.Now;
                    streamWriter.WriteLine(localDate.ToString() + ": " + str);
                }
            }
        }

        public void OpenAnewLogFile()
        {
            // Delete the file if it exists.
            if (File.Exists(PresentionPath))
            {
                File.Delete(PresentionPath);
            }

            // Create the file.
            using (FileStream fs = File.Create(PresentionPath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}