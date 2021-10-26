using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsActionModel : LeadsAction
    {
        public LeadsActionModel()
        {
            this.ListLeadsActionExtends = new List<LeadsActionExtendsModel>();
        }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string TypeName
        {
            get
            {
                return this.Type > 0 ? Utils.GetBitwiseEnumDescription<LeadActionType>(this.Type).Value : "";
            }
        }
        public List<int> ListType
        {
            get
            {
                var lstReturn = new List<int>();
                if (this.Type > 0)
                {
                    var types = Utils.GetBitwiseEnumDescription<LeadActionType>(this.Type).Key;
                    if (!string.IsNullOrEmpty(types))
                    {
                        lstReturn = types.Split(',').ToList().Select(x => x.Trim().ToInt(0)).ToList();
                    }
                }
                return lstReturn;
            }
        }
        public string SourceName
        {
            get
            {
                return this.Source > 0 ? Utils.GetEnumDescription((LeadActionSource)this.Source) : "";
            }
        }
        public string PaymentStatusName
        {
            get
            {
                return this.PaymentStatus > 0 ? Utils.GetEnumDescription((LeadActionPaymentStatus)this.PaymentStatus) : "";
            }
        }
        public string CampaignName { get; set; }
        public string CreatedDateName
        {
            get
            {
                return this.CreatedDate.HasValue && this.CreatedDate.Value > DateTime.MinValue ? this.CreatedDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }
        public string StatusName
        {
            get
            {
                return this.PaymentStatus > 0 ? Utils.GetEnumDescription((LeadActionStatus)this.Status) : "";
            }
        }

        public string HadLoanName
        {
            get
            {
                string boxTypeStr = this.Type > 0 ? (Utils.HasState(this.Type, (int)LeadActionType.LoanCar) ? "Có" : "Không") : "Không";
                if (this.SaleSetType > 0 && Utils.HasState(this.SaleSetType, (int)LeadActionSaleSetType.LoanCar))
                {
                    boxTypeStr = "Có";
                }
                return boxTypeStr;
            }
        }
        public string HadTestDriveName
        {
            get
            {
                string boxTypeStr = this.Type > 0 ? (Utils.HasState(this.Type, (int)LeadActionType.TestDriver) ? "Có" : "Không") : "Không";
                if (this.SaleSetType > 0 && Utils.HasState(this.SaleSetType, (int)LeadActionSaleSetType.TestDriver))
                {
                    boxTypeStr = "Có";
                }
                return boxTypeStr;
            }
        }
        public string BargainName
        {
            get
            {
                string boxTypeStr = this.Type > 0 ? (Utils.HasState(this.Type, (int)LeadActionType.Bargain) ? "Có" : "Không") : "Không";
                if (this.SaleSetType > 0 && Utils.HasState(this.SaleSetType, (int)LeadActionSaleSetType.Bargain))
                {
                    boxTypeStr = "Có";
                }
                return boxTypeStr;
            }
        }
        public string ConsultantName
        {
            get
            {
                string boxTypeStr = this.Type > 0 ? (Utils.HasState(this.Type, (int)LeadActionType.Consultant) ? "Có" : "Không") : "Không";
                if (this.SaleSetType > 0 && Utils.HasState(this.SaleSetType, (int)LeadActionSaleSetType.Consultant))
                {
                    boxTypeStr = "Có";
                }
                return boxTypeStr;
            }
        }
        public string VoucherName
        {
            get
            {
                string boxTypeStr = this.Type > 0 ? (Utils.HasState(this.Type, (int)LeadActionType.Voucher) ? "Có" : "Không") : "Không";
                if (this.SaleSetType > 0 && Utils.HasState(this.SaleSetType, (int)LeadActionSaleSetType.Voucher))
                {
                    boxTypeStr = "Có";
                }
                return boxTypeStr;
            }
        }
        //public string OtherInformationName
        //{
        //    get
        //    {
        //        var info = this.OtherInformation != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(this.OtherInformation) : null;
        //        string rs = String.Empty;
        //        if (info != null)
        //        {
        //            if (info.ContainsKey("CurrentPrice"))
        //            {
        //                rs += "Giá hiện tại: " + info.Where(x => x.Key == "CurrentPrice")?.FirstOrDefault().Value;
        //            }
        //            if (info.ContainsKey("BargainPrice"))
        //            {
        //                rs += ", Giá đề xuất: " + info.Where(x => x.Key == "CurrentPrice")?.FirstOrDefault().Value;
        //            }
        //        }
        //        return rs;
        //    }
        //}
        public List<int> ListSaleSetType
        {
            get
            {
                var lstReturn = new List<int>();
                if (this.SaleSetType > 0)
                {
                    var types = Utils.GetBitwiseEnumDescription<LeadActionSaleSetType>(this.SaleSetType).Key;
                    if (!string.IsNullOrEmpty(types))
                    {
                        lstReturn = types.Split(',').ToList().Select(x => x.Trim().ToInt(0)).ToList();
                    }
                }
                return lstReturn;
            }
        }
        public string SaleSetTypeName
        {
            get
            {
                string name = "";
                if (this.SaleSetType > 0)
                {
                    name = Utils.GetBitwiseEnumDescription<LeadActionSaleSetType>(this.SaleSetType).Value;
                }
                return name;
            }
        }
        public string CityName { get; set; }
        public List<DealerLeadModel> DealerLeadList;
        public string ListLeadsActionExtendsStr { get; set; }
        public List<LeadsActionExtendsModel> ListLeadsActionExtends;
    }
}
