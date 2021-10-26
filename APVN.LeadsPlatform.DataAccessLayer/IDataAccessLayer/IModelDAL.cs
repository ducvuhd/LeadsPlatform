using System;
using System.Collections.Generic;
using APVN.LeadsPlatform.Entity;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface IModelDAL
    {
        IEnumerable<APVN.LeadsPlatform.Entity.Models> ListAll();
    }
}
