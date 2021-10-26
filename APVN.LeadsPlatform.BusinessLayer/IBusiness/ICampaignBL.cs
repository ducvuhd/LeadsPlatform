using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface ICampaignBL
    {
        Task<List<Campaign>> ListAllAsync();
        Response Insert(Campaign campaign);
        Response Update(Campaign campaign);
    }
}
