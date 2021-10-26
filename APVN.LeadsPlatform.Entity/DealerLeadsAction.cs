using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class DealerLeadsAction
    {
        public int DealerId { get; set; }

        [Description("LeadId")]
        public int LeadsId { get; set; }
        [Description("Box thu lead")]
        public int LeadActionId { get; set; }
        public string DealerName { get; set; }
        [Description("Ngày Dealer nhận lead")]
        public DateTime? DealerRecievedDate { get; set; }
        [Description("Mô tả nhu cầu")]
        public string DealerNote { get; set; }
        public int Status { get; set; }
        public string CustomerName { get; set; }
        public int AssignType { get; set; }
    }
}
