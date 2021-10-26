using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class DealerIndexModel : Pager
    {
        public string UserNameFilter { get; set; }
        public List<DealerFromOtoModel> ListDealer { get; set; }
    }
}
