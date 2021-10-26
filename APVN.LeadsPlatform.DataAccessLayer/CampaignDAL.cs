using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class CampaignDAL : ICampaignDAL
    {
        public async Task<IEnumerable<Campaign>> ListAllAsync()
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                var x = await connection.QueryAsync<Campaign>("PR_Campaign_GetAll", commandType: CommandType.StoredProcedure);
                return x;
                // return new List<Makes>();
            }
        }
        public int Insert(Campaign campaign)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Name", campaign.Name);
                dynamicParameters.Add("CreatedDate", campaign.CreatedDate);
                dynamicParameters.Add("ModifiedDate", campaign.ModifiedDate);
                dynamicParameters.Add("CreatedBy", campaign.CreatedBy);
                dynamicParameters.Add("LastModifiedBy", campaign.LastModifiedBy);
                return connection.QuerySingleOrDefault<int>("Campaign_Insert", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }
        public void Update(Campaign campaign)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", campaign.Id);
                dynamicParameters.Add("Name", campaign.Name);
                dynamicParameters.Add("CreatedDate", campaign.CreatedDate);
                dynamicParameters.Add("ModifiedDate", campaign.ModifiedDate);
                dynamicParameters.Add("CreatedBy", campaign.CreatedBy);
                dynamicParameters.Add("LastModifiedBy", campaign.LastModifiedBy);
                connection.Execute("Campaign_Update", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
