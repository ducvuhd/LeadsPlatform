using APVN.LeadsPlatform.Utility.AttributeCustoms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{

    public class LeadsActionExtends
    {
        public int Id { get; set; }
        public int LeadsId { get; set; }
        public int LeadsActionId { get; set; }
        public int SecondHand { get; set; }
        public int CityId { get; set; }
        public int Type { get; set; }
        public int Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string OtherInformation { get; set; }
        [Description("Số tiền vay")]
        public long LoanMoney { get; set; }
        [Description("Thời gian vay(tháng)")]
        public int LoanTime { get; set; }
        [ReturnValueField(Field = "BankName")]
        [Description("Ngân hàng")]
        public int BankId { get; set; }
        public long BargainCurrentPrices { get; set; }
        public long BargainPrices { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class LeadsActionExtendsBase
    {
        public int Id { get; set; }
        public int LeadsId { get; set; }
        public int LeadsActionId { get; set; }
        public int DealerId { get; set; }
        public int SecondHand { get; set; }
        public int CityId { get; set; }
        public int Type { get; set; }
        public int Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string OtherInformation { get; set; }
        public long LoanMoney { get; set; }
        public int LoanTime { get; set; }
        public int BankId { get; set; }
        public long BargainCurrentPrices { get; set; }
        public long BargainPrices { get; set; }
        public bool IsDeleted { get; set; }
    }
}

