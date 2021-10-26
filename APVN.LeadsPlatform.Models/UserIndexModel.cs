using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class UserIndexModel: Pager
    {
        public string FilterKeyword { get; set; }
        public IEnumerable<Users> Result { get; set; }
    }
}
