using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    [Serializable]
    public partial class Makes
    {
        public short MakeID { get; set; }
        public string MakeName { get; set; }
        public string ShortMakeName { get; set; }
        public byte Status { get; set; }
        public int? NumOfAuto { get; set; }
        public string Keywords { get; set; }
        public string Logo { get; set; }
        public string Avatar { get; set; }
        public int Type { get; set; }
    }
}
