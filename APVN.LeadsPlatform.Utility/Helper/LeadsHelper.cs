using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Helper
{
    public class LeadsHelper
    {
        public static List<KeyValuePair<int, string>> LeadsIsActiveList()
        { 
            var lst = Utils.GetAllEnumValueAndDescription<LeadsIsActive>();
            lst.Insert(0, new KeyValuePair<int, string>(-1, "Tất cả"));
            return lst;
        }
    }
}
