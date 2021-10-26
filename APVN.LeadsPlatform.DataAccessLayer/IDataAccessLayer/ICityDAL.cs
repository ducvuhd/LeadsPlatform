using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface ICityDAL
    {
        IEnumerable<City> ListAll();
    }
}
