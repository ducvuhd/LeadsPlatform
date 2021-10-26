using System;
using System.Collections.Generic;
using System.Text;
using APVN.LeadsPlatform.Entity;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface IModelBL
    {
        List<APVN.LeadsPlatform.Entity.Models> ListAll();
    }
}
