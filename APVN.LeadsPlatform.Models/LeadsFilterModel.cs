using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsFilterModel : Pager
    {
        public string KeyWord { get; set; }
        public String DealerName { get; set; }
        public String DealerIds { get; set; }   // Danh sach id dearler 12,4,5
        public DateTime?[] CreatedDate { set; get; }
        public DateTime?[] StartCareDate { get; set; }
        public string[] CreatedDateArr { set; get; }
        public string[] StartCareDateArr { get; set; }
        public int? Status { get; set; }
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int? SecondHand { get; set; }
        public int? CityId { get; set; }
        public int? AssignToId { get; set; }
        public int? IsActive { get; set; }
        public int? CampaignId { get; set; }
        public int? SourceLead { get; set; }
        public int? Source { get; set; }
        public int? BankId { get; set; }
        public int? PaymentStatus { get; set; }
        public int? Type { get; set; }

    }
}
