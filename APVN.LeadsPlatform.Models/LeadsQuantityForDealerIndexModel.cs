using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsQuantityForDealerIndexModel : Pager
    {
        public string DealerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<LeadsQuantityForDealerModel> ListLeadsQuantityForDealer { get; set; }
    }
}
