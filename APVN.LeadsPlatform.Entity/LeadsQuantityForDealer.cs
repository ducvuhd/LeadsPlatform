using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class LeadsQuantityForDealer
    {
		public int Id { get; set; }
		public int DealerId { get; set; }
		public string DealerName { get; set; }
		public string DealerEmail { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int AssignQuantity { get; set; }
		public int AssignQuantityActive { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastModifedDate { get; set; }
		public string LastModifiedBy { get; set; }
	}
}
