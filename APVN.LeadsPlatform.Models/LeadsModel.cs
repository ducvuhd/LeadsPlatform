using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.AttributeCustoms;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsModel : Leads
    {
        public LeadsModel()
        {
            this.ListAction = new List<LeadsActionModel>();
        }
        public List<LeadsActionModel> ListAction { get; set; }
        public string Note { get; set; }
        public string StartCareDateName
        {
            get
            {
                return this.StartCareDate.HasValue && this.StartCareDate.Value > DateTime.MinValue ? this.StartCareDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }
        public string CityName { get; set; }
        public string SourceLeadName
        {
            get
            {
                return this.SourceLead > 0 ? Utils.GetEnumDescription((LeadActionSource)this.SourceLead) : "";
            }
        }
        public string StatusName
        { 
            get
            {
                return this.Status > 0 ? Utils.GetEnumDescription((LeadStatus)this.Status) : "";
            }
        }
        public string ActiveName
        {
            get
            {
                return this.IsActive ? "Mở" : "Khóa";
            }
        }

        public string AssignToName { get; set; }
        public string CreatedDateName
        {
            get
            {
                return this.CreatedDate.HasValue && this.CreatedDate.Value > DateTime.MinValue ? this.CreatedDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public string LastModifiedDateName
        {
            get
            {
                return this.LastModifiedDate.HasValue && this.LastModifiedDate.Value > DateTime.MinValue ? this.LastModifiedDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public string StableIncomeName
        {
            get
            {
                return this.StableIncome.HasValue ? (this.StableIncome.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string GoodAddressName
        {
            get
            {
                return this.GoodAddress.HasValue ? (this.GoodAddress.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string IncomeName
        {
            get
            {
                return this.Income.HasValue && this.Income.Value > 0 ? this.Income.Value.ToString("#,###,###") : "";
            }
        }
        public string LookingOtherCarName
        {
            get
            {
                return this.LookingOtherCar.HasValue ? (this.LookingOtherCar.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string GoodActionDealerName
        {
            get
            {
                return this.GoodActionDealer.HasValue ? (this.GoodActionDealer.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string IsChangeCarName
        {
            get
            {
                return this.IsChangeCar.HasValue ? (this.IsChangeCar.Value ? "Đổi xe" : "Mua xe") : "Chưa xác định";
            }
        }
        public string ChangeCarTimeName
        {
            get
            {
                return this.ChangeCarTime > 0 ? this.ChangeCarTime.ToString() : "Chưa từng đổi xe";
            }
        }
        public string IsSoldCarName
        {
            get
            {
                return this.IsSoldCar.HasValue ? (this.IsSoldCar.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string HasWifeName
        {
            get
            {
                return this.HasWife.HasValue ? (this.HasWife.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string HasChildName
        {
            get
            {
                return this.HasChild.HasValue ? (this.HasChild.Value ? "Có" : "Không") : "Chưa xác định";
            }
        }
        public string FamilyIncomeName
        {
            get
            {
                return this.FamilyIncome.HasValue && this.FamilyIncome.Value > 0 ? this.FamilyIncome.Value.ToString("#,###,###") : "";
            }
        }
    }
}
