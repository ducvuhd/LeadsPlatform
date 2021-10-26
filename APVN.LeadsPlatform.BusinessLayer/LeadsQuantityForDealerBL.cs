using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class LeadsQuantityForDealerBL : ILeadsQuantityForDealerBL
    {
        private readonly ILeadsQuantityForDealerDAL _quantityForDealerDAL;
        private readonly IDealerLeadDAL _dealerLeadDAL;

        public LeadsQuantityForDealerBL(ILeadsQuantityForDealerDAL quantityForDealerDAL, IDealerLeadDAL dealerLeadDAL)
        {
            _quantityForDealerDAL = quantityForDealerDAL;
            _dealerLeadDAL = dealerLeadDAL;
        }

        public Response GetListPaging(LeadsQuantityForDealerIndexModel indexModel)
        {
            indexModel = _quantityForDealerDAL.GetListPaging(indexModel);
            if (indexModel.ListLeadsQuantityForDealer.Count < indexModel.PageSize
                && indexModel.StartDate == null && indexModel.EndDate == null)
            {
                List<int> lstDealerIdFromLead = _quantityForDealerDAL.GetListDealerId(indexModel);
                string dealerFromleads = lstDealerIdFromLead.Count > 0 ? string.Join(",", lstDealerIdFromLead) : "";
                DealerIndexModel indexDealer = new DealerIndexModel();
                indexDealer.PageIndex = indexModel.PageIndex;
                indexDealer.PageSize = indexModel.PageSize - indexModel.ListLeadsQuantityForDealer.Count;
                indexDealer.UserNameFilter = indexModel.DealerEmail;
                var dealerIndex = _dealerLeadDAL.GetUserProfileByNamePaging(indexDealer, dealerFromleads);
                foreach (var item in dealerIndex.ListDealer)
                {
                    LeadsQuantityForDealerModel model = new LeadsQuantityForDealerModel();
                    model.DealerEmail = item.Email;
                    model.DealerName = item.UserName;
                    model.DealerId = item.UserId;
                    indexModel.ListLeadsQuantityForDealer.Add(model);
                }
                indexModel.TotalRecord += indexDealer.TotalRecord;
            }
            return new Response(SystemCode.Success, "", indexModel);
        }

        public Response Insert(LeadsQuantityForDealer entity)
        {
            _quantityForDealerDAL.Insert(entity);
            return new Response(SystemCode.Success, "", null);
        }
        public Response CancelSetSchedule(int id)
        {
            _quantityForDealerDAL.CancelSetSchedule(id);
            return new Response(SystemCode.Success, "Hủy thành công", null);
        }
        public LeadsQuantityForDealer GetByDealerId(int dealerId)
        {
            try
            {
                return _quantityForDealerDAL.GetByDealerId(dealerId);
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
