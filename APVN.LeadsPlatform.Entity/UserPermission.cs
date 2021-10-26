using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class UserPermission
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CityId { get; set; }
        public int groupId { get; set; }
        public int RoleId { get; set; }
        public int AllowanceId { get; set; }
    }
}
