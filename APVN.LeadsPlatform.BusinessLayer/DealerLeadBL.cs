using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class DealerLeadBL : IDealerLeadBL
    {
        private IDealerLeadDAL _dealerLeadDAL;

        public DealerLeadBL(IDealerLeadDAL dealerLeadDAL)
        {
            this._dealerLeadDAL = dealerLeadDAL;
        }

        public List<DealerLeadModel> GetDealerLeadByLeadsId(int leadsId)
        {
            return (List<DealerLeadModel>)_dealerLeadDAL.GetDealerLeadByLeadsId(leadsId);
        }

        public List<UserProfile> GetUserByListId(string userIdList)
        {
            return (List<UserProfile>)_dealerLeadDAL.GetUserByListId(userIdList);
        }

        public List<UserProfile> GetUserByName(string username)
        {
            return (List<UserProfile>)_dealerLeadDAL.GetUserByName(username);
        }
        public Response GetUserProfileByNamePaging(DealerIndexModel indexModel)
        {
            try
            {
                var dealerIndex = _dealerLeadDAL.GetUserProfileByNamePaging(indexModel,"");
                return new Response(SystemCode.Success, "", dealerIndex);
            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);
            }
        }

        public List<DealerLeadModel> GetDealerLeadsActionById(int leadActionIdId)
        {
            try
            {
                var dealerLeads = (List<DealerLeadModel>)_dealerLeadDAL.GetDealerLeadsActionById(leadActionIdId);
                if (dealerLeads != null && dealerLeads.Count > 0)
                {
                    dealerLeads.ForEach(x =>
                    {
                        // Check info member oto
                        var memberShip = _dealerLeadDAL.GetUserByListId(x.DealerId.ToString())?.FirstOrDefault();
                        x.DealerName = memberShip != null ? memberShip.DisplayName : "";
                    });
                }
                return dealerLeads;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
