using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Dapper.SqlMapper;

namespace APVN.LeadsPlatform.Utility.Database.Interfaces
{
    public interface IRepository : IDbContext
    {
        #region Write to DB
        void Command<T>(T entity, string storeName, params string[] ignore);
        void CommandDataSet(string storeName, DataTable dataTable, string typeName);
        IEnumerable<TId> CommandDataSetGetListId<TId>(string storeName, DataTable dataTable, string typeName);
        TId CommandGetId<T, TId>(T entity, string storeName, params string[] ignore);
        void CommandSql(string sql);
        IEnumerable<T> CommandSqlGetList<T>(string sql);
        TId CommandSqlGetId<TId>(string sql);
        #endregion
        #region Read from DB
        T GetById<T, TId>(TId id, string storeName);
        IEnumerable<T> List<T, TCondition>(TCondition condition, string storeName);
        IEnumerable<T> ListTotalRecord<T, TCondition>(TCondition condition, string storeName, out int totalRecord, params string[] ignore);
        #endregion
    }
}
