using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class BankBL : IBankBL
    {
        private IBankDAL _bankDAL;

        public BankBL(IBankDAL bankDAL)
        {
            this._bankDAL = bankDAL;
        }

        public List<Bank> ListAll()
        {
            return (List<Bank>)_bankDAL.ListAll();
        }
    }
}
