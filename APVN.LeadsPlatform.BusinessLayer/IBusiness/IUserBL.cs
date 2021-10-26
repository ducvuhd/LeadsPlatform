using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface IUserBL
    {
        Task<Users> GetByIdAsync(int userId);
        Task<Users> GetLoginAsync(string username, string password);
        Task<Users> GetByUserNameAsync(string username);
        Task<IEnumerable<Users>> GetByDepartmentIdAsync(int departmentId, int month);
        Task<UserIndexModel> GetListAsync(UserIndexModel filter);
        Response Save(PermissionSave model);
        Response DeletePermission(int id);
        Task<IEnumerable<UserPermission>> GetPermissionByUserName(string userName);
        bool ResetPassword(string userName, string password);
        Task<IEnumerable<Users>> GetGetUserActiveAsync();
        Task<Response> GetForEdit(int userId);
        Response Update(UserUpdateIndexModel model);
        void UpdateOTPPrivateKey(int userId, string otpPrivateKey);
    }
}
