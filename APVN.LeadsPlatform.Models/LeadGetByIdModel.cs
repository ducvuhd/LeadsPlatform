using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadGetByIdModel
    {
        public LeadGetByIdModel()
        {
            this.Leads = new LeadsModel();
            this.ListLeadActionModel = new List<LeadsActionModel>();
            this.ListLeadActionExtendsModel = new List<LeadsActionExtendsModel>();
        }
        public LeadsModel Leads { get; set; }
        public List<LeadsActionModel> ListLeadActionModel { get; set; }
        public List<LeadsActionExtendsModel> ListLeadActionExtendsModel { get; set; }
    }
}
