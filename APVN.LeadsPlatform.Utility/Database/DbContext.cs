using APVN.LeadsPlatform.Utility.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Database
{
    public class DbContext : IDbContext
    {
        internal protected IDbConnection _connection;
        internal protected IDbTransaction _transaction;
        public void SetWriteDbContext(IDbConnection writeContext, IDbTransaction transaction)
        {
            _connection = writeContext;
            _transaction = transaction;
        }
    }
}
