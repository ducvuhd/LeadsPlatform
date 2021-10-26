using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public class Const
    {
        public static Dictionary<int, string> SecondHand = new Dictionary<int, string>()
        {
            {(int)CommonEnum.SecondHandId.SeconHand, "Đã qua sử dụng" },
            {(int)CommonEnum.SecondHandId.New, "Mới"}
        };

        public static List<SecoundHand> SecoundHandList()
        {
            return new List<SecoundHand>() { 
                new SecoundHand{ SecoundHandId = (int)CommonEnum.SecondHandId.SeconHand, SecoundHandName = "Đã qua sử dụng"},
                new SecoundHand{ SecoundHandId = (int)CommonEnum.SecondHandId.New, SecoundHandName = "Mới"}
            };
        }
    }

    public class SecoundHand
    {
        public int SecoundHandId { get; set; }
        public string SecoundHandName { get; set; }
    }
}
