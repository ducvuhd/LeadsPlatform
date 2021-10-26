using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class ModelDAL : IModelDAL
    {
        public IEnumerable<APVN.LeadsPlatform.Entity.Models> ListAll()
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.Query<APVN.LeadsPlatform.Entity.Models>("PR_Models_ListAll", commandType: CommandType.StoredProcedure);
                // return new List<Makes>();
            }
        }
    }
}
