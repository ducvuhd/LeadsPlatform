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
    public class CityDAL : ICityDAL
    {
        public IEnumerable<City> ListAll()
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<City>("PR_Cities_ListAll", commandType: CommandType.StoredProcedure);
                // return new List<Makes>();
            }
        }
    }
}
