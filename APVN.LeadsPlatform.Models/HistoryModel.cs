using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class HistoryModel : History
    {
        public string CreatedDateName
        {
            get
            {
                return this.CreatedDate.ToString("dd/MM/yyyy");
            }
        }
    }
}
