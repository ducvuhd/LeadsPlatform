using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models.IgnoreEqualModels
{
    public class LeadsActionUpdateIgnore
    {
        public int Id { get; set; }
        public int LeadsId { get; set; }
        public int Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string OtherInformation { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
