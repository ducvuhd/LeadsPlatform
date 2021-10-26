using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    [Serializable]
    public partial class Models
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string ShortModelName { get; set; }
        public short MakeID { get; set; }
        public byte Status { get; set; }
        public short? ModelOrder { get; set; }
        public int? NumOfAuto { get; set; }
        public string Keywords { get; set; }
        public byte? Type { get; set; }
    }
}
