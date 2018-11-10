using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SQLToCSV
{
    class Program
    {
        private static Timer timer;
        private static double delay;
        static void Main(string[] args)
        {
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1) return;

            Console.Title = "SQL to CSV";
            Console.WriteLine("###### SQL to CSV Started ######");

            /* first process */
            RunProcess();

            /* set timer */
            SetTimer();

            System.Threading.Thread.Sleep(-1);
        }

        private static void SetTimer()
        {
            // Create a timer with milisecond .
            timer = new Timer(delay * 60 * 60 * 1000);

            // Hook up the Elapsed event for the timer.
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /* event timer */

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            RunProcess();
            Console.WriteLine("");
        }

        private static void RunProcess()
        {
            Config config = Config.Get();
            delay = config?.Timer ?? 1.0;

            if (config != null)
            {
                /* config db */
                Database db = new Database(config);

                /* config server */
                Server server = new Server(config);

                if (db.IsConnected() && server.IsConnected()) /* check ftp server */
                {
                    DataTable dt = db.GetData();
                    if (dt != null)
                    {
                        string csvText = dt.ConvertDataTableToCSVText(); /* get data from sql server */
                        string localFileName = FileFunction.WriteCSVFile(csvText, config.FileName); /* create tmp csv file */
                        bool isUploaded = server.Upload(localFileName); /* upload csv file to ftp server */
                        if (isUploaded)
                            FileFunction.DeleteCSVFile(localFileName); /* delete tmp file */
                    }
                }
            }
        }
    }
}
