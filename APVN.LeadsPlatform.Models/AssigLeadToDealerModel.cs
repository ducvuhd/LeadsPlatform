using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class AssigLeadToDealerModel
    {
        public int LeadsId { get; set; }
        public int DealerId { get; set; }
        public int LeadsActionId { get; set; }
        public string DealerName { get; set; }
        public string DealerNote { get; set; }
        public int LeadActionPaymentStatus { get; set; }
        public List<int> LstDealerId { get; set; }
        public List<string> LstDealerName { get; set; }
    }
}
