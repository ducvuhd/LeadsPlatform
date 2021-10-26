using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsActionExtendsModel : LeadsActionExtends
    {
        public string SecondHandName
        {
            get
            {
                return this.SecondHand > 0 ? Utils.GetEnumDescription((Secondhand)this.SecondHand) : "";
            }
        }
        public string CityName { get; set; }
        public string TypeName
        {
            get
            {
                return this.Type > 0 ? Utils.GetBitwiseEnumDescription<LeadActionType>(this.Type).Value : "";
            }
        }
        public string BankName { get; set; }
        public string LoanMoneyName
        {
            get
            {
                return this.LoanMoney > 0 ? $"{this.LoanMoney.ToString("#,###,###")} VNĐ" : "";
            }
        }
        public string LoanTimeName
        {
            get
            {
                return this.LoanTime > 0 ? $"{this.LoanTime} tháng" : "";
            }
        }
        public string BargainCurrentPricesName
        {
            get
            {
                return this.BargainCurrentPrices > 0 ? $"{this.BargainCurrentPrices.ToString("#,###,###")} VNĐ" : "";
            }
        }
        public string BargainPricesName
        {
            get
            {
                return this.BargainPrices > 0 ? $"{this.BargainPrices.ToString("#,###,###")} VNĐ" : "";
            }
        }
    }
}
