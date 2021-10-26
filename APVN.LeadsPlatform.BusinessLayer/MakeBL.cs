using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class MakeBL : IMakeBL
    {
        private IMakeDAL _makeDAL;

        public MakeBL(IMakeDAL makeDAL)
        {
            this._makeDAL = makeDAL;
        }

        public List<Makes> ListAll()
        {
            return (List<Makes>)_makeDAL.ListAll();
        }
    }
}
