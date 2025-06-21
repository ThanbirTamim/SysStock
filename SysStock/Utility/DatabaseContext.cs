using System;
using System.Data;
using System.Data.SqlClient;

namespace SysStock.Utility
{
    public class DatabaseContext : IDisposable
    {
        private readonly SqlConnection _connection;

        public DatabaseContext()
        {
            _connection = new SqlConnection(SystemUtility.connectionString);
        }

        protected SqlConnection GetConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
