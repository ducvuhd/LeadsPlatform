using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Database
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        public IDbConnection _connection;
        public DbFactory()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection"));
            }
        }

        public void Dispose()
        {
            if (!_disposed && _connection != null)
            {
                _disposed = true;
                _connection.Dispose();
            }
        }
    }
}
