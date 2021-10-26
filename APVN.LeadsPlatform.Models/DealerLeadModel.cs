using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class DealerLeadModel : DealerLeadsAction
    {
        public string DealerRecievedDateName
        {
            get
            {
                return this.DealerRecievedDate.HasValue && this.DealerRecievedDate.Value > DateTime.MinValue ? this.DealerRecievedDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public string StatusName
        {
            get
            {
                return this.Status > 0 ? Utils.GetEnumDescription((DealerLeadActionStatus)this.Status) : "";
            }
        }
    }
}
