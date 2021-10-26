using Dapper;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class UsersDAL : IUsersDAL
    {
        public async Task<IEnumerable<Users>> GetAllInMonth(int month)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetAllInMonth", new
                {
                    Month = month
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetByDepartmentIdAsync(int departmentId, int month)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetByDepartmentIds", new
                {
                    DepartmentId = departmentId,
                    Month = month
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetGetUserActiveAsync()
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("PR_Users_GetUserActive", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Users> GetByIdAsync(int userId)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<Users>("Users_GetUserById", new
                {
                    UserId = userId
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Users> GetLoginAsync(string username, string password)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<Users>("Users_GetUserByUserNamePassword", new
                {
                    UserName = username,
                    PassWord = password
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetWorkingByCityAndMonthAsync(int cityId, int month)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetWorkingByCityAndMonth", new
                {
                    CityId = cityId,
                    Month = month
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetWorkingByDepartmentAndMonthAsync(int department, int month)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetWorkingByDepartmentAndMonth", new
                {
                    DepartmentId = department,
                    Month = month
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetAllInMonthAsync(int month)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetAllInMonthIsWorkShift", new
                {
                    Month = month
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetAllWorkingInMonthAsync(int month)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetAllWorkingInMonthIsWorkShift", new
                {
                    Month = month
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetByCityIdAsync(int cityId)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<Users>("Users_GetByCityId", new
                {
                    CityId = cityId
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<UserIndexModel> SearchAsync(UserIndexModel model)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("FilterKeyWord", model.FilterKeyword);
            dynamicParams.Add("PageIndex", model.PageIndex);
            dynamicParams.Add("PageSize", model.PageSize);
            dynamicParams.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                model.Result = await connection.QueryAsync<Users>("Users_GetByUserName", dynamicParams, commandType: CommandType.StoredProcedure);
                model.TotalRecord = dynamicParams.Get<int>("TotalRecord");
                return model;
            }
        }

        public async Task<Users> GetByUserNameAsync(string username)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<Users>("Users_GetUserByUserName", new
                {
                    UserName = username
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public Users GetByUserName(string username)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.QueryFirstOrDefault<Users>("Users_GetUserByUserName", new
                {
                    UserName = username
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public bool InsertPermission(PermissionSave model)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Execute("UserPermission_Create", new
                {
                    UserName = model.UserName,
                    CityId = model.CityId,
                    DepartmentId = model.DepartmentId,
                    RoleId = model.RoleId
                }, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public async Task<IEnumerable<UserPermission>> GetPermissionByUserNameAsync(string userName)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<UserPermission>("UserPermission_GetByUserName", new
                {
                    UserName = userName
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public bool DeletePermission(int id)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Execute("UserPermission_Delete", new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool UpdateRole(string userName, string role)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Execute("Users_UpdateRole", new
                {
                    UserName = userName,
                    Role = role
                }, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool ResetPassword(string userName, string password)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.ExecuteScalar<int>("Users_ResetPassword", new
                {
                    UserName = userName,
                    Password = password
                }, commandType: CommandType.StoredProcedure) > 0;
            }
        }
        public IEnumerable<Users> GetGetUserActive()
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<Users>("PR_Users_GetUserActive", commandType: CommandType.StoredProcedure);
            }
        }
        public void UpdateOTPPrivateKey(int userId, string otpPrivateKey)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                string sql = $"UPDATE dbo.Users SET OTPPrivateKey = N'{otpPrivateKey}' WHERE UserId = {userId}";
                connection.Query(sql);
            }
        }
    }
}
