using APVN.LeadsPlatform.Utility.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        public UnitOfWork()
        {
            _id = Guid.NewGuid();
            _connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection"));
        }
        //public UnitOfWork(DbFactory dbFactory)
        //{
        //    _id = Guid.NewGuid();
        //    _connection = dbFactory._connection;
        //}


        Guid _id = Guid.Empty;
        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }
        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }

        public void Begin()
        {
            if (_transaction != null) _transaction.Commit();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null) _transaction.Dispose();
            _transaction = null;
        }
        public IDbConnection GetConnection()
        {
            return _connection;
        }
        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }
    }
}
