using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MigrationValidator
{
    class DataRetrieval<T>
    {
        public List<T> GetData(string selectStatement)
        {
            var connection = new SqlConnection
                ("Data Source=VSQLDEV;Initial Catalog=PIM_DEV;Integrated Security=True");
            connection.Open();

            var items = connection.Query<T>(selectStatement).ToList();
            
            return items;
        }
    }
}
