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
    public class LeadsDAL : ILeadsDAL
    {
        public LeadsModel GetById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
                {
                    return connection.QueryFirstOrDefault<LeadsModel>("Leads_GetById", new
                    {
                        Id = id
                    }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return new LeadsModel();
            }

        }
        public LeadsModel GetLeadsModelById(int id)
        {
            var leadModel = new LeadsModel();
            var model = new LeadGetByIdModel();
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                var result = connection.QueryMultiple("Leads_GetById_06_06_2021", new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);
                model.Leads = result.Read<LeadsModel>().FirstOrDefault();
                model.ListLeadActionModel = result.Read<LeadsActionModel>().ToList();
                model.ListLeadActionExtendsModel = result.Read<LeadsActionExtendsModel>().ToList();
                if (model.Leads != null)
                {
                    leadModel = model.Leads;
                    if (model.ListLeadActionModel != null && model.ListLeadActionModel.Count > 0)
                    {
                        leadModel.ListAction.AddRange(model.ListLeadActionModel);
                        if (model.ListLeadActionExtendsModel != null && model.ListLeadActionExtendsModel.Count > 0)
                        {
                            foreach (var item in leadModel.ListAction)
                            {
                                var lstExtends = model.ListLeadActionExtendsModel.FindAll(x => x.LeadsActionId == item.Id);
                                if (lstExtends != null && lstExtends.Count > 0)
                                {
                                    item.ListLeadsActionExtends.AddRange(lstExtends);
                                }
                            }
                        }
                    }
                }
                return leadModel;
            }
        }
        public int CheckExistMobile(string mobile)
        {

            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.QuerySingle<int>("LeadsPlatform_CheckExistMobile", new
                {
                    Mobile = mobile
                }, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<LeadsActionModel> GetListActionByLeadId(int leadsId)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<LeadsActionModel>("LeadsAction_GetListByLeadsId", new
                {
                    LeadsId = leadsId
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<LeadsModel> GetList(LeadsFilterModel filterModel, out int totalRow)
        {
            totalRow = 0;
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("KeyWord", filterModel.KeyWord.Replace(" ", "%"));
                dynamicParams.Add("DealerIds", filterModel.DealerIds);

                if (filterModel.CreatedDate != null && filterModel.CreatedDate.Length == 2 && filterModel.CreatedDate[0] != null)
                {
                    if (filterModel.CreatedDate[0] == filterModel.CreatedDate[1])
                    {
                        dynamicParams.Add("CreatedDateFrom", DateTime.Parse(filterModel.CreatedDateArr[0]).Date);
                        dynamicParams.Add("CreatedDateTo", DateTime.Parse(filterModel.CreatedDateArr[1]).Date.AddDays(1));
                    }
                    else
                    {
                        dynamicParams.Add("CreatedDateFrom", DateTime.Parse(filterModel.CreatedDateArr[0]));
                        dynamicParams.Add("CreatedDateTo", DateTime.Parse(filterModel.CreatedDateArr[1]).Date.AddDays(1));
                    }
                }
                else
                {
                    dynamicParams.Add("CreatedDateFrom", "");
                    dynamicParams.Add("CreatedDateTo", "");
                }

                if (filterModel.StartCareDate != null && filterModel.StartCareDate.Length == 2 && filterModel.StartCareDate[0] != null)
                {
                    if (filterModel.StartCareDate[0] == filterModel.StartCareDate[1])
                    {
                        dynamicParams.Add("StartCareDateFrom", DateTime.Parse(filterModel.StartCareDateArr[0]).Date);
                        dynamicParams.Add("StartCareDateTo", DateTime.Parse(filterModel.StartCareDateArr[1]).Date.AddDays(1));
                    }
                    else
                    {
                        dynamicParams.Add("StartCareDateFrom", DateTime.Parse(filterModel.StartCareDateArr[0]).Date);
                        dynamicParams.Add("StartCareDateTo", DateTime.Parse(filterModel.StartCareDateArr[1]).Date.AddDays(1));
                    }
                }
                else
                {
                    dynamicParams.Add("StartCareDateFrom", "");
                    dynamicParams.Add("StartCareDateTo", "");
                }

                dynamicParams.Add("Status", filterModel.Status == null ? -1 : filterModel.Status);
                dynamicParams.Add("MakeId", filterModel.MakeId == null ? 0 : filterModel.MakeId);
                dynamicParams.Add("ModelId", filterModel.ModelId == null ? 0 : filterModel.ModelId);
                dynamicParams.Add("SecondHand", filterModel.SecondHand == null ? 0 : filterModel.SecondHand);
                dynamicParams.Add("CityId", filterModel.CityId == null ? 0 : filterModel.CityId);
                dynamicParams.Add("AssignToId", filterModel.AssignToId == null ? 0 : filterModel.AssignToId);
                dynamicParams.Add("IsActive", filterModel.IsActive == null ? -1 : filterModel.IsActive);
                dynamicParams.Add("CampaignId", filterModel.CampaignId == null ? 0 : filterModel.CampaignId);
                dynamicParams.Add("SourceLead", filterModel.SourceLead == null ? 0 : filterModel.SourceLead);
                dynamicParams.Add("Source", filterModel.Source == null ? 0 : filterModel.Source);
                dynamicParams.Add("BankId", filterModel.BankId == null ? 0 : filterModel.BankId);
                dynamicParams.Add("PaymentStatus", filterModel.PaymentStatus == null ? 0 : filterModel.PaymentStatus);
                dynamicParams.Add("Type", filterModel.Type == null ? 0 : filterModel.Type);
                dynamicParams.Add("PageIndex", filterModel.PageIndex);
                dynamicParams.Add("PageSize", filterModel.PageSize);
                dynamicParams.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
                {
                    var result = connection.Query<LeadsModel>(
                        "PR_Leads_GetMainList_20210528",
                        dynamicParams,
                        commandType: CommandType.StoredProcedure);

                    totalRow = dynamicParams.Get<int>("TotalRecord");
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void UpdateStatus(LeadsChangeStatusModel statusModel)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                string sql = $@"UPDATE dbo.Leads SET Status = {statusModel.Status} WHERE Id = {statusModel.LeadsId}";
                connection.Execute(sql);
            }
        }
        public void GetLead(int leadsId, int assignToId)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                string sql = $@"UPDATE dbo.Leads SET AssignToId = {assignToId} WHERE Id = {leadsId}";
                connection.Execute(sql);
            }
        }

        public void ChangeIsActiveLead(int leadsId, bool isActive)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                string sql = $@"UPDATE dbo.Leads SET IsActive = '{isActive}' WHERE Id = {leadsId}";
                connection.Execute(sql);
            }
        }
        public void UpdateLeadActionStatus(int status, int leadActionid)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                string sql = $@"UPDATE dbo.LeadsAction SET Status = {status} WHERE Id = {leadActionid} AND (IsDeleted IS NULL OR IsDeleted = 0)";
                connection.Execute(sql);
            }
        }
    }
}
