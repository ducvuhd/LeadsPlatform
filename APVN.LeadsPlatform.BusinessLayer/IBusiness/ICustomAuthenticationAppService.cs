using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface ICustomAuthenticationAppService
    {
        ActivatingUserModel CurrUser();
    }
}
