using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface ICheckPermissionBL
    {
        bool HasPermission(ActivatingUserModel currUser, int permission);
        bool HasPermission(ActivatingUserModel currUser, params int[] permission);
        Task<List<Users>> GetUsersAsync(ActivatingUserModel currUser, int cityId, int departmentId, List<int> lstRole, bool isAdmin = false, bool isApproved = false, bool isGetLeave = false, int month = 0);
    }
}
