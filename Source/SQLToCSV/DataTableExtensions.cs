using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLToCSV
{
    public static class DataTableExtensions
    {
        public static string ConvertDataTableToCSVText(this DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            //write column name
            sb.AppendLine(string.Join(",", dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList()));

            //write data
            foreach (DataRow row in dt.Rows)
                sb.AppendLine(string.Join(",", row.ItemArray.Select(field => field.ToString()).ToList()));

            return sb.ToString();
        }
    }
}
