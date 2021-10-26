using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class LeadsExtend
    {
        public int Id { get; set; }
        public int LeadsId { get; set; }
        public string Version { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string CityTestDrive { get; set; }
    }
}
