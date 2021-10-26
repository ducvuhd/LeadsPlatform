using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface ILeadsQuantityForDealerBL
    {
        Response GetListPaging(LeadsQuantityForDealerIndexModel indexModel);
        Response Insert(LeadsQuantityForDealer entity);
        Response CancelSetSchedule(int id);
        LeadsQuantityForDealer GetByDealerId(int dealerId);
    }
}
