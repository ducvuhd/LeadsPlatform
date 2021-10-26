using APVN.LeadsPlatform.Utility.Database.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace APVN.LeadsPlatform.Utility.Database
{
    public class Repository : DbContext, IRepository
    {
        #region Write to DB
        public void Command<T>(T entity, string storeName, params string[] ignore)
        {
            DynamicParameters parameters = SqlHelper.GetParam<T>(entity, ignore);
            _connection.Execute(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        public void CommandDataSet(string storeName, DataTable dataTable, string typeName)
        {
            var parameters = new
            {
                TableUDT = dataTable.AsTableValuedParameter(typeName)
            };
            _connection.Execute(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<TId> CommandDataSetGetListId<TId>(string storeName, DataTable dataTable, string typeName)
        {
            var parameters = new
            {
                TableUDT = dataTable.AsTableValuedParameter(typeName)
            };
            return _connection.Query<TId>(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        public TId CommandGetId<T, TId>(T entity, string storeName, params string[] ignore)
        {
            DynamicParameters parameters = SqlHelper.GetParam<T>(entity, ignore);
            return _connection.QuerySingleOrDefault<TId>(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        public void CommandSql(string sql)
        {
            _connection.Execute(sql, transaction: _transaction);
        }
        public IEnumerable<T> CommandSqlGetList<T>(string sql)
        {
            return _connection.Query<T>(sql, transaction: _transaction);
        }
        public TId CommandSqlGetId<TId>(string sql)
        {
            return _connection.QueryFirstOrDefault<TId>(sql, transaction: _transaction);
        }
        #endregion
        #region Read from DB
        public T GetById<T, TId>(TId id, string storeName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            return _connection.QuerySingleOrDefault<T>(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<T> List<T, TCondition>(TCondition condition, string storeName)
        {
            DynamicParameters parameters = SqlHelper.GetParam<TCondition>(condition, "");
            return _connection.Query<T>(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<T> ListTotalRecord<T, TCondition>(TCondition condition, string storeName, out int totalRecord, params string[] ignore)
        {
            if (ignore.Length == 0)
            {
                ignore[0] = "TotalRecord";
            }
            DynamicParameters parameters = SqlHelper.GetParam<TCondition>(condition, ignore);
            parameters.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            IEnumerable<T> lst = _connection.Query<T>(storeName, parameters, transaction: _transaction, commandType: CommandType.StoredProcedure);
            totalRecord = parameters.Get<int>("TotalRecord");
            return lst;
        }
        #endregion
    }
}
