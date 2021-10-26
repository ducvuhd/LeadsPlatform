using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class UserRoleDAL : IUserRoleDAL
    {
        public async Task<IEnumerable<UserRole>> GetRoleByUserId(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
                {
                    return await connection.QueryAsync<UserRole>("UserRole_GetRoleByPersonId", new
                    {
                        PersonId = userId,
                    }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
