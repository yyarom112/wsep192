using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace src.Domain
{
    class LogManager
    {

        public static void WriteToHistory(String str)
        {
            DateTime localDate = DateTime.Now;
            System.IO.File.AppendAllText(@"C:\Logs\History.txt", localDate.ToString() + ": " + str + System.Environment.NewLine);
        }
        public static void OpenAnewHistory()
        {
            System.IO.File.WriteAllText(@"C:\Logs\History.txt", "");
        }
        public static void loadAllYourHistory()
        {
            string connectionString = @"Data Source=ise172.ise.bgu.ac.il;Initial Catalog=history;User ID=labuser;Password=wonsawheightfly";
            SqlConnection myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            String output = "";
            SqlCommand myCommand = new SqlCommand(
              "select * from history.dbo.items where buyer=53 or seller=53", myConnection);
            SqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                String str = reader.GetValue(0).ToString().Trim() + " : The user " + reader.GetValue(4).ToString().Trim() +
                    "sell for user " + reader.GetValue(5).ToString().Trim() + " a commodity " + reader.GetValue(1).ToString().Trim() + " in price " +
                    reader.GetValue(3).ToString().Trim() + " for" + reader.GetValue(2).ToString().Trim() + " funds." + System.Environment.NewLine;
                output = output + str;
            }
            reader.Close();
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                WriteToHistory(e.ToString());
            }
            System.IO.File.AppendAllText(@"C:\Logs\History.txt", output + System.Environment.NewLine);
        }
    }
}
