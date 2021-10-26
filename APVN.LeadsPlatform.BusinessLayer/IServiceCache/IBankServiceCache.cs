using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.IServiceCache
{
    public interface IBankServiceCache
    {
        List<Bank> ListAll();
    }
}
