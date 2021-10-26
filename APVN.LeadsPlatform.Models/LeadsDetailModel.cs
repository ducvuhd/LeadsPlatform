using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsDetailModel
    {
        public LeadsModel LeadsModel = new LeadsModel();
        public List<NoteModel> LeadNoteList = new List<NoteModel>();
        public List<HistoryModel> LeadHistoryList = new List<HistoryModel>();

        public LeadsDetailModel()
        {
            this.LeadsModel = new LeadsModel();
            this.LeadNoteList = new List<NoteModel>();
            this.LeadHistoryList = new List<HistoryModel>();
        }
    }

    public class LeadsDetailExtendModel
    {
        public string CampName { get; set; }
        public string UserName { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string SecondHandStr { get; set; }
        public string LeadsBoxStr { get; set; }
        public string LeadsCityName { get; set; }
        public string LeadsActionSourceName { get; set; }
        public string DealersInfo { get; set; }
        public string BankName { get; set; }

        // nhieu feild qua
        public string HadLoanName { get; set; }
        public string HadTestDriveName { get; set; }
        public string RejectNoteStr { get; set; }
    }
}
