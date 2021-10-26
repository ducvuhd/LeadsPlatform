using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class LeadsQuantityForDealerDAL : ILeadsQuantityForDealerDAL
    {
        public LeadsQuantityForDealerIndexModel GetListPaging(LeadsQuantityForDealerIndexModel indexModel)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("DealerEmail", indexModel.DealerEmail);
            dynamicParams.Add("StartDate", indexModel.StartDate);
            dynamicParams.Add("EndDate", indexModel.EndDate);
            dynamicParams.Add("PageIndex", indexModel.PageIndex);
            dynamicParams.Add("PageSize", indexModel.PageSize);
            dynamicParams.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                indexModel.ListLeadsQuantityForDealer = connection.Query<LeadsQuantityForDealerModel>(
                    "LeadsQuantityForDealer_GetListPaging",
                    dynamicParams,
                    commandType: CommandType.StoredProcedure).ToList();

                indexModel.TotalRecord = dynamicParams.Get<int>("TotalRecord");
                return indexModel;
            }
        }
        public List<int> GetListDealerId(LeadsQuantityForDealerIndexModel indexModel)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("DealerEmail", indexModel.DealerEmail);
            dynamicParams.Add("StartDate", indexModel.StartDate);
            dynamicParams.Add("EndDate", indexModel.EndDate);
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<int>(
                    "LeadsQuantityForDealer_GetListDealerId",
                    dynamicParams,
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Insert(LeadsQuantityForDealer entity)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("DealerId", entity.DealerId);
            dynamicParams.Add("DealerName", entity.DealerName);
            dynamicParams.Add("DealerEmail", entity.DealerEmail);
            dynamicParams.Add("StartDate", entity.StartDate);
            dynamicParams.Add("EndDate", entity.EndDate);
            dynamicParams.Add("AssignQuantity", entity.AssignQuantity);
            dynamicParams.Add("AssignQuantityActive", entity.AssignQuantityActive);
            dynamicParams.Add("IsActive", entity.IsActive);
            dynamicParams.Add("CreatedBy", entity.CreatedBy);
            dynamicParams.Add("CreatedDate", entity.CreatedDate);
            dynamicParams.Add("LastModifedDate", entity.LastModifedDate);
            dynamicParams.Add("LastModifiedBy", entity.LastModifiedBy);

            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                connection.Query("LeadsQuantityForDealer_Insert", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }
        public void CancelSetSchedule(int id)
        {
            string sql = $"UPDATE dbo.LeadsQuantityForDealer SET IsActive = 0 WHERE Id = {id}";
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                connection.Query(sql);
            }
        }

        public LeadsQuantityForDealer GetByDealerId(int dealerId)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("DealerId", dealerId);

            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.QueryFirstOrDefault<LeadsQuantityForDealer>("LeadsQuantityForDealer_GetByDealerId", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
