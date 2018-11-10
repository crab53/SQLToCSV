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
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            //write data
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }
    }
}
