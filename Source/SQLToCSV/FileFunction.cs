using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLToCSV
{
    public class FileFunction
    {
        /* load config */
        public static T LoadJsonFile<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(new Config()));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return default(T);
        }

        /* create csv file */
        public static string WriteCSVFile(string csvText, string fileName)
        {
            try
            {
                /* create tmp csv file */
                string filePath = Path.Combine(Environment.CurrentDirectory, string.Format("{0}.csv", fileName));
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(csvText);
                }
                return filePath;
            }
            catch (Exception) { Message.Show("Unable to write CSV file.", Constants.EMessage.ERROR); }
            return null;
        }

        /* delete local csv file */
        public static bool DeleteCSVFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                return true;
            }
            catch (Exception) { }
            return false;
        }
    }
}
