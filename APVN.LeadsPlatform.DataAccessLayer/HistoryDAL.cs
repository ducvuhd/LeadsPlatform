using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class HistoryDAL : IHistoryDAL
    {
        public IEnumerable<HistoryModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RelationId", leadsId);
                dynamicParams.Add("Type", type);
                var lst = connection.Query<HistoryModel>("History_GetListByRelationIdAndTypeNoCount", dynamicParams, commandType: CommandType.StoredProcedure);
                return lst;
            }
        }
        public IEnumerable<HistoryModel> GetListByRelationIdAndType(HistoryIndexModel indexModel, out int totalRecord)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RelationId", indexModel.RelationId);
                dynamicParams.Add("Type", indexModel.Type);
                dynamicParams.Add("PageIndex", indexModel.PageIndex);
                dynamicParams.Add("PageSize", indexModel.PageSize);
                dynamicParams.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var lst = connection.Query<HistoryModel>("History_GetListByRelationIdAndType", dynamicParams, commandType: CommandType.StoredProcedure);
                totalRecord = dynamicParams.Get<int>("TotalRecord");
                return lst;
            }
        }
    }
}
