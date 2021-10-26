using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class ActivatingUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<int> Roles { get; set; }
        public int GroupId { get; set; }
        public bool HasPermission(params SystemRole[] permission)
        {
            var ret = false;
            foreach (var per in permission)
            {
                ret = this.Roles.Contains((int)per);
                if (ret) break;
            }
            return ret;
        }
    }
}
