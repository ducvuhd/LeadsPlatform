using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface ILeadsDAL
    {
        LeadsModel GetById(int id);
        IEnumerable<LeadsActionModel> GetListActionByLeadId(int leadsId);
        int CheckExistMobile(string mobile);
        IEnumerable<LeadsModel> GetList(LeadsFilterModel filterModel, out int totalRow);
        void UpdateStatus(LeadsChangeStatusModel statusModel);
        void GetLead(int leadsId, int assignToId);

        void ChangeIsActiveLead(int leadsId, bool isActive);
        LeadsModel GetLeadsModelById(int id);
        void UpdateLeadActionStatus(int status, int leadActionid);
    }
}
