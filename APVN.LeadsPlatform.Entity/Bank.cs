using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    [Serializable]
    public partial class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnsignName { get { return Utils.UnicodeToKoDauAndGach(Name); } }
        public string Code { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> BankType { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }
        public string Logo { get; set; }
        public string Slogun { get; set; }
        public string Description { get; set; }

        public string BankAddress { get; set; }
    }
}
