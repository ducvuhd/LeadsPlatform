using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface IDealerLeadBL
    {
        List<DealerLeadModel> GetDealerLeadByLeadsId(int leadsId);

        List<UserProfile> GetUserByListId(string userIdList);

        List<UserProfile> GetUserByName(string username);
        Response GetUserProfileByNamePaging(DealerIndexModel indexModel);
        List<DealerLeadModel> GetDealerLeadsActionById(int leadActionIdId);
    }
}
