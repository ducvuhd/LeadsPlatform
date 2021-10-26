using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface IHistoryBL
    {
        IEnumerable<HistoryModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type);
        HistoryIndexModel GetListByRelationIdAndType(HistoryIndexModel indexModel);
    }
}
