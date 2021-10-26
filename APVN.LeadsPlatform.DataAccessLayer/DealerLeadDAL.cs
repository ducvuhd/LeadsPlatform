using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Database;
using APVN.LeadsPlatform.Utility.Enums;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class DealerLeadDAL : IDealerLeadDAL
    {
        public IEnumerable<DealerLeadModel> GetDealerLeadByLeadsId(int leadsId)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<DealerLeadModel>("DealerLeadsAction_GetByLeadsId", new
                {
                    LeadsId = leadsId
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<UserProfile> GetUserByListId(string userIdList)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("OtoConnection")))
            {
                return connection.Query<UserProfile>("pr_UserProfile_GetUserByListId_LEADS", new
                {
                    UserIdList = userIdList
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<UserProfile> GetUserByName(string username)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("OtoConnection")))
            {
                return connection.Query<UserProfile>("pr_UserProfile_GetUserByName_LEADS", new
                {
                    Username = username
                }, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Láy thông tìn dealer là thông tin của bảng UserProfile trong DB 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public DealerIndexModel GetUserProfileByNamePaging(DealerIndexModel indexModel, string dealerFromLead)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("OtoConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserName", indexModel.UserNameFilter);
                parameters.Add("DealerFromLead", dealerFromLead);
                parameters.Add("PageIndex", indexModel.PageIndex);
                parameters.Add("PageSize", indexModel.PageSize);
                parameters.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                indexModel.ListDealer = connection.Query<DealerFromOtoModel>("UserProfile_SearchByEmailPaging", parameters, commandType: CommandType.StoredProcedure).ToList();
                indexModel.TotalRecord = parameters.Get<int>("TotalRecord");
                return indexModel;
            }
        }

        public IEnumerable<DealerLeadModel> GetDealerLeadsActionById(int leadActionIdId)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<DealerLeadModel>("DealerLeadsAction_GetByLeadsActionId", new
                {
                    LeadsActionId = leadActionIdId
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
