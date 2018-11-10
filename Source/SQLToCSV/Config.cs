using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLToCSV
{
    public class Config
    {
        public FTPInfo FTP { get; set; }
        public SQLInfo SQL { get; set; }
        public string FileName { get; set; }
        public double Timer { get; set; }

        public Config()
        {
            FTP = new FTPInfo();
            SQL = new SQLInfo();
            FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            Timer = 1;
        }

        public static Config Get()
        {
            Config config = new Config();

            /* read config */
            string configPath = Path.Combine(Environment.CurrentDirectory, Constants.CONFIG_FILENAME);
            config = FileFunction.LoadJsonFile<Config>(configPath);

            /* config error */
            if (config == null)
                Message.Show("Unable to read config file.", Constants.EMessage.ERROR);

            /* config success */
            Message.Show("Readed config file.", Constants.EMessage.INFO);

            return config;
        }
    }

    public class FTPInfo
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string FilePath { get; set; }

        public FTPInfo()
        {
            Host = "";
            User = "";
            Password = "";
            FilePath = "";
        }
    }

    public class SQLInfo
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Query { get; set; }

        public SQLInfo()
        {
            Server = "";
            Username = "";
            Password = "";
            Database = "";
            Query = "";
        }
    }
}
