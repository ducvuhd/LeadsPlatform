using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class NoteModel : Note
    {
        public string CurrentRelationStatusStr
        {
            get
            {
                return this.CurrentRelationStatus > 0 ? Utils.GetEnumDescription((LeadStatus)this.CurrentRelationStatus) : "";
            }
        }
        public string CreatedDateName
        {
            get
            {
                return this.CreatedDate.HasValue && this.CreatedDate.Value > DateTime.MinValue ? this.CreatedDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }
        public string LastModifiedDateName
        {
            get
            {
                return this.LastModifiedDate.HasValue && this.LastModifiedDate.Value > DateTime.MinValue  ? this.LastModifiedDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }
    }
}
