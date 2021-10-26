using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class City
    {
        public byte CityID { get; set; }
        public string CityName { get; set; }
        public string ShortCityName { get; set; }
        public byte Status { get; set; }
        public int? NumOfAuto { get; set; }
        public string CityCode { get; set; }
        public byte? Type { get; set; }
        public byte? Area { get; set; }
        public byte? Zone { get; set; }
        public string Keywords { get; set; }
    }
}
