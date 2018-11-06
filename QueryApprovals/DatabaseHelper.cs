using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QueryApprovals
{
    public class DatabaseHelper
    {
        public static DataTable GetDataTable(string connectionString, string queryString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(queryString, connection))
                {
                    var da = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    

    }
}
