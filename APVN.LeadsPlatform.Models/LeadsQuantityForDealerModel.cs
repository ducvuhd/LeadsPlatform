using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsQuantityForDealerModel
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string DealerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateStr
        {
            get
            {
                return this.StartDate.HasValue && this.StartDate.Value > DateTime.MinValue ? this.StartDate.Value.ToString("dd/MM/yyyyy") : "";
            }
        }
        public DateTime? EndDate { get; set; }
        public string EndDateStr
        {
            get
            {
                return this.EndDate.HasValue && this.EndDate.Value > DateTime.MinValue ? this.EndDate.Value.ToString("dd/MM/yyyyy") : "";
            }
        }
        public int AssignQuantity { get; set; }
        public int AssignQuantityActive { get; set; }
        public bool IsActive { get; set; }
    }
}
