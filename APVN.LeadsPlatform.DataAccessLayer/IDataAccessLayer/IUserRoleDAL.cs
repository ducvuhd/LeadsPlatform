using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface IUserRoleDAL
    {
        Task<IEnumerable<UserRole>> GetRoleByUserId(int userId);
    }
}
