using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysStock.Utility
{
    public class SystemUtility
    {
        public static string serverAddress = "localhost";
        public static string databaseName = "SysStockDB";
        public static string connectionString = $"Server={serverAddress};Database={databaseName};Trusted_Connection=True;";
    }
}
