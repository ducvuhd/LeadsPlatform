using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class HistoryBL : IHistoryBL
    {
        private readonly IHistoryDAL _historyDAL;

        public HistoryBL(IHistoryDAL historyDAL)
        {
            _historyDAL = historyDAL;
        }

        public IEnumerable<HistoryModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type)
        {
            return _historyDAL.GetListByRelationIdAndTypeNoCount(leadsId, type);
        }
        public HistoryIndexModel GetListByRelationIdAndType(HistoryIndexModel indexModel)
        {
            int totalRecord = 0;
            var lst = _historyDAL.GetListByRelationIdAndType(indexModel, out totalRecord);
            indexModel.ListHistory = lst.ToList();
            indexModel.TotalRecord = totalRecord;
            return indexModel;
        }
    }
}
