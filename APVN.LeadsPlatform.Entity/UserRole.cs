using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public int PersonId { get; set; }
        public int Type { get; set; }
        public bool IsInherit { get; set; }
    }
}
