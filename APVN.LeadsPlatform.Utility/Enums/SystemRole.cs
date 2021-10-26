using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Enums
{
    public enum SystemRole
    {
        [Description("Sale")]
        Sales = 6,
        [Description("Trưởng phòng")]
        Lead = 7,
        [Description("Admin")]
        Admin = 8,
        [Description("Điều phối")]
        Coordinate = 9,
        [Description("Trưởng nhóm")]
        TeamLeader = 10,
        [Description("Marketing")]
        MKT = 11,
        [Description("Business Development")]
        BD = 12,
    }
}
