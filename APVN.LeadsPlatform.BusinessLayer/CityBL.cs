using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class CityBL : ICityBL
    {
        private ICityDAL _cityDAL;

        public CityBL(ICityDAL cityDAL)
        {
            this._cityDAL = cityDAL;
        }

        public List<City> ListAll()
        {
            return (List<City>)_cityDAL.ListAll();
        }
    }
}
