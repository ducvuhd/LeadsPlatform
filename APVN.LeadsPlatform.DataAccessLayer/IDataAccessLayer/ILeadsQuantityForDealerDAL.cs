using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface ILeadsQuantityForDealerDAL
    {
        LeadsQuantityForDealerIndexModel GetListPaging(LeadsQuantityForDealerIndexModel indexModel);
        List<int> GetListDealerId(LeadsQuantityForDealerIndexModel indexModel);
        void Insert(LeadsQuantityForDealer entity);
        void CancelSetSchedule(int id);
        LeadsQuantityForDealer GetByDealerId(int dealerId);
    }
}
