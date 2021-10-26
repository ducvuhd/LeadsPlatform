using Dapper;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class UserPermissionDAL : IUserPermissionDAL
    {
        public async Task<IEnumerable<UserPermission>> GetUserPermissionByUserNameAsync(string userName)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return await connection.QueryAsync<UserPermission>("UserPermission_GetByUserName", new
                {
                    UserName = userName
                }, commandType: CommandType.StoredProcedure);
            }
        }

        
    }
}
