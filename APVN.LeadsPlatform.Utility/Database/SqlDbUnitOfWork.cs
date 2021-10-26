using APVN.LeadsPlatform.Utility.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DVDV.ICS.Utility.Database
{
    public class SqlDbUnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly SqlDbContext _writeContext;

        public SqlDbUnitOfWork()
        {
            _writeContext = new SqlDbContext(SqlDbContext.DBPosition.Master);
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            try
            {
                _writeContext.CommitTransaction();
                _writeContext.BeginTransaction();
            }
            catch (Exception e)
            {
                _writeContext.RollbackTransaction();
                throw e;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writeContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void BeginTransaction()
        {
            _writeContext.BeginTransaction();
        }

        public void Commit()
        {
            _writeContext.CommitTransaction();
        }

        public void Rollback()
        {
            _writeContext.RollbackTransaction();
        }

        public IDbContext GetDbContext()
        {
            return (IDbContext)_writeContext;
        }
    }
}
