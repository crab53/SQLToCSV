using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLToCSV
{
    public class Message
    {
        public static void Show(string message, Constants.EMessage level)
        {
            if (!string.IsNullOrEmpty(message))
            {
                switch (level)
                {
                    case Constants.EMessage.INFO:
                        Console.WriteLine("### INFO{" + DateTime.Now.ToShortTimeString() + "}:\t" + message);
                        break;

                    case Constants.EMessage.SUCCESS:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("### SUCCESS{" + DateTime.Now.ToShortTimeString() + "}:\t" + message);
                        break;

                    case Constants.EMessage.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("### ERROR{" + DateTime.Now.ToShortTimeString() + "}:\t" + message);
                        break;
                }
                Console.ResetColor();
            }
        }
    }
}
