using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class PermissionSave
    {
        public string UserName { get; set; }
        public int CityId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
}
