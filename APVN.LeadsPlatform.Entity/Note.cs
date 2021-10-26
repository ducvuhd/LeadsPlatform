using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class Note
    {
        public int Id { get; set; }
        public int RelationId { get; set; }
        public int Type { get; set; }
        public int CurrentRelationStatus { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public bool CurrentInActive { get; set; }
    }
}
