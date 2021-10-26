using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface ILeadsBL
    {
        Response Insert(LeadsModel lead);
        Response Update(LeadsModel lead, LeadsModel oldLead);
        LeadsModel GetById(int id);
        bool CheckExistMobile(string mobile);
        Task<string> GetFormFilter();
        List<LeadsModel> GetList(LeadsFilterModel filterModel, out int totalRow);
        Response UpdateStatus(LeadsModel leadsModel, LeadsChangeStatusModel statusModel);
        Response TakeLead(int leadsId);
        Response AssigToLead(int leadsId, int assignToId);
        Response AssigLeadToDealer(AssigLeadToDealerModel assigLeadToDealer);
        Task<LeadsDetailModel> GetDetail(int id);
        Response ChangeIsActive(int leadsId, bool isActive);
        Response ImportLeadFromExcel(List<LeadsModel> lstModel);
        Response GetLeadsModelById(int id);
        Response UpdateLeadActionStatus(int status, int leadActionid);
    }
}
