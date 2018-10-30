using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SQLToCSV
{
    public class Server
    {
        Config config = null;

        public Server(Config _config) /* contructor */
        {
            config = _config;
        }

        /* check connect */
        public bool IsConnected()
        {
            if (config != null)
            {
                if (!string.IsNullOrEmpty(config.FTP.FilePath) && !string.IsNullOrEmpty(config.FTP.User) && !string.IsNullOrEmpty(config.FTP.Password))
                {
                    try
                    {
                        // create an FTP client
                        using (FtpClient client = new FtpClient(config.FTP.Host))
                        {
                            // create credentials
                            client.Credentials = new NetworkCredential(config.FTP.User, config.FTP.Password);

                            // begin connecting to the server
                            client.Connect();

                            Message.Show("Checked connect to FTP server.", Constants.EMessage.INFO);

                            client.Disconnect();
                        }
                        return true;
                    }
                    catch (Exception ex) { }
                }
                Message.Show("Unable to connect FTP server.", Constants.EMessage.ERROR);
            }

            return false;
        }

        public bool Upload(string localFilePath)
        {
            bool isUpload = false;
            try
            {
                using (FtpClient client = new FtpClient(config.FTP.Host))
                {
                    // create credentials
                    client.Credentials = new NetworkCredential(config.FTP.User, config.FTP.Password);

                    // begin connecting to FTP server
                    client.Connect();

                    //upload a file to another path on ftp server 
                    client.RetryAttempts = 3; //and retry 3 times before giving up
                    isUpload = client.UploadFile(localFilePath, Path.Combine(config.FTP.FilePath, Path.GetFileName(localFilePath)), FtpExists.Overwrite, true, FtpVerify.Retry);

                    Message.Show("Upload CSV file successful!", Constants.EMessage.SUCCESS);

                    client.Disconnect();
                }
            }
            catch (Exception ex) { Message.Show("Unable to upload CSV file to FPT folder.", Constants.EMessage.ERROR); }
            return isUpload;
        }
    }
}
