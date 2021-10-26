using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface IHistoryDAL
    {
        IEnumerable<HistoryModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type);
        IEnumerable<HistoryModel> GetListByRelationIdAndType(HistoryIndexModel indexModel, out int totalRecord);
    }
}
