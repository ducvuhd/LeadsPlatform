using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class CampaignBL : ICampaignBL
    {
        private ICampaignDAL _campaignDAL;

        public CampaignBL(ICampaignDAL campaignDAL)
        {
            this._campaignDAL = campaignDAL;
        }

        public async Task<List<Entity.Campaign>> ListAllAsync()
        {
            return (List<Campaign>)await _campaignDAL.ListAllAsync();
        }
        public Response Insert(Campaign campaign)
        {
            try
            {
                int id = _campaignDAL.Insert(campaign);
                return new Response(SystemCode.Success, "Tạo Campain thành công", id);
            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);
            }

        }
        public Response Update(Campaign campaign)
        {
            try
            {
                _campaignDAL.Update(campaign);
                return new Response(SystemCode.Success, "Tạo lead thành công", null);
            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);

            }
        }
    }
}
