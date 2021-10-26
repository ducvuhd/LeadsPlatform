using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;

namespace APVN.LeadsPlatform.Models
{
    public class AuthenticatedUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int EmployeeId { get; set; }
        public int CityId { get; set; }
        public int DepartmentId { get; set; }
        public List<SystemRole> Roles { get; set; }        
        public string Token { get; set; }
        public string RandomKey { get; set; }
        public long ExpiredRandomKey { get; set; }

        public bool HasPermission(SystemRole permission)
        {
            return Roles != null && this.Roles.Contains(permission);
        }

        public bool HasPermission(params SystemRole[] permission)
        {
            var ret = false;
            foreach (var per in permission)
            {
                ret = this.Roles.Contains(per);
                if (ret) break;
            }
            return ret;
        }
    }
}
