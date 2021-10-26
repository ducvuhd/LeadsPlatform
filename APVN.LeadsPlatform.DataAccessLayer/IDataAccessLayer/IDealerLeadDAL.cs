using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface IDealerLeadDAL
    {
        IEnumerable<DealerLeadModel> GetDealerLeadByLeadsId(int leadsId);

        IEnumerable<UserProfile> GetUserByListId(string userIdList);

        IEnumerable<UserProfile> GetUserByName(string username);
        DealerIndexModel GetUserProfileByNamePaging(DealerIndexModel indexModel, string dealerFromLead);

        IEnumerable<DealerLeadModel> GetDealerLeadsActionById(int leadActionIdId);
    }
}
