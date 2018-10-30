using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLToCSV
{
    public class Database
    {
        string connStr = "";
        bool isConnected = false;
        Config config = null;

        public Database(Config _config)
        {
            config = _config;

            connStr = string.Format("Data Source={0};Initial Catalog={1}; User Id={2}; Password={3}",
                config.SQL.Server,
                config.SQL.Database,
                config.SQL.Username,
                config.SQL.Password);
        }

        public bool IsConnected()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    conn.Close();
                    isConnected = true;
                    Message.Show("Checked connect to database.", Constants.EMessage.INFO);
                }
            }
            catch (SqlException) { Message.Show("Unable to connect to database.", Constants.EMessage.ERROR); }
            return isConnected;
        }

        public DataTable GetData()
        {
            DataTable dt = null;
            if (isConnected)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(config.SQL.Query, conn))
                        {
                            dt = new DataTable();
                            adapter.Fill(dt);
                        }
                        conn.Close();
                        Message.Show("Get data successful.", Constants.EMessage.INFO);
                    }
                }
                catch (Exception)
                {
                    Message.Show("Unable to get data.", Constants.EMessage.ERROR);
                    dt = null;
                }
            }
            return dt;
        }
    }
}
