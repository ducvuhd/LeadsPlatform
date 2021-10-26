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
    public class MakeDAL : IMakeDAL
    {
        public IEnumerable<Makes> ListAll()
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<Makes>("PR_Makes_ListAll",commandType: CommandType.StoredProcedure);
                // return new List<Makes>();
            }
        }
    }
}
