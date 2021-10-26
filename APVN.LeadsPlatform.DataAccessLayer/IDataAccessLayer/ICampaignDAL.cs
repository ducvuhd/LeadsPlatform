using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface ICampaignDAL
    {
        Task<IEnumerable<Campaign>> ListAllAsync();
        int Insert(Campaign campaign);
        void Update(Campaign campaign);
    }
}
