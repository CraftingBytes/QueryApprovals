using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryApprovals
{
    public static class DataTableExtensions
    {
        public static string ToFormattedTableString(this DataTable dt)
        {
            var nullString = "NULL";
            int[] maxColumnLengths = new int[dt.Columns.Count];

            for (int col = 0; col < maxColumnLengths.Length; col++)
            {
                maxColumnLengths[col] = dt.Rows.OfType<DataRow>().Select(dr => dr[col]?.ToString().Length ?? nullString.Length).Max();
            }
            var builder = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                for (int colIndex = 0; colIndex < maxColumnLengths.Length; colIndex++)
                {
                    if (colIndex > 0)
                    {
                        builder.Append("|");
                    }
                    var format = "{0,-" + maxColumnLengths[colIndex] + "}";
                    builder.Append(string.Format(format, row[colIndex]));
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
