using APVN.LeadsPlatform.Utility.AttributeCustoms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class LeadsAction
    {
        public int Id { get; set; }
        public int LeadsId { get; set; }
        [ReturnValueField(Field = "MakeName")]
        [Description("Hãng xe")]
        public int MakeId { get; set; }
        [ReturnValueField(Field = "ModelName")]
        [Description("Dòng xe")]
        public int ModelId { get; set; }
        [ReturnValueField(Field = "TypeName")]
        [Description("Box thu lead")]
        public int Type { get; set; }
        [ReturnValueField(Field = "SourceName")]
        [Description("Nguồn Box thu lead")]
        public int Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        [ReturnValueField(Field = "CampaignName")]
        [Description("Campaign")]
        public int CampaignId { get; set; }
        [ReturnValueField(Field = "PaymentStatusName")]
        [Description("TT thanh toán")]
        public int? PaymentStatus { get; set; }
        [ReturnValueField(Field = "StatusName")]
        [Description("Trạng thái")]
        public int Status { get; set; }
        [Description("CS Set tay Box thu lead")]
        public int SaleSetType { get; set; }
        public bool IsDeleted { get; set; }
    }
}










