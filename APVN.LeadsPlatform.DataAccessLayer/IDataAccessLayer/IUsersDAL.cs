using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface IUsersDAL
    {
        Task<Users> GetByIdAsync(int userId);
        Task<IEnumerable<Users>> GetByDepartmentIdAsync(int departmentId, int month);
        Task<Users> GetLoginAsync(string username, string password);
        Task<Users> GetByUserNameAsync(string username);
        Users GetByUserName(string username);
        Task<IEnumerable<Users>> GetWorkingByDepartmentAndMonthAsync(int department, int month);
        Task<IEnumerable<Users>> GetWorkingByCityAndMonthAsync(int cityId, int month);
        Task<IEnumerable<Users>> GetByCityIdAsync(int cityId);
        Task<IEnumerable<Users>> GetAllWorkingInMonthAsync(int month);
        Task<IEnumerable<Users>> GetAllInMonthAsync(int month);
        Task<IEnumerable<UserPermission>> GetPermissionByUserNameAsync(string userName);
        Task<UserIndexModel> SearchAsync(UserIndexModel model);
        bool InsertPermission(PermissionSave model);
        bool DeletePermission(int id);
        bool UpdateRole(string userName, string role);
        bool ResetPassword(string userName, string password);
        Task<IEnumerable<Users>> GetGetUserActiveAsync();
        IEnumerable<Users> GetGetUserActive();
        void UpdateOTPPrivateKey(int userId, string otpPrivateKey);
    }
}
