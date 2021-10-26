using APVN.LeadsPlatform.Utility.AttributeCustoms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    public class Leads
    {
        public int Id { get; set; }
        [Description("Tên")]
        public string FullName { get; set; }
        [Description("SĐT")]
        public string Mobile { get; set; }
        [Description("Email")]
        public string Email { get; set; }
        [ReturnValueField(Field = "StartCareDateName")]
        [Description("Ngày bắt đầu CS")]
        public DateTime? StartCareDate { get; set; }
        [Description("Thành phố")]
        public int CityId { get; set; }
        [ReturnValueField(Field = "SourceLeadName")]
        [Description("Nguồn")]
        public int SourceLead { get; set; }
        [ReturnValueField(Field = "StatusName")]
        [Description("Trạng thái")]
        public int Status { get; set; }
        [ReturnValueField(Field = "ActiveName")]
        [Description("Trạng thái khóa")]
        public bool IsActive { get; set; }
        [ReturnValueField(Field = "AssignToName")]
        [Description("NV Chăm sóc")]
        public int AssignToId { get; set; }
        [Description("Mô tả")]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string FullTextSearch { get; set; }
        [Description("Địa chỉ chi tiết")]
        public string Address { get; set; }
        [Description("Mô tả nhu cầu")]
        public string ExpectedDescription { get; set; }
        [Description("Phiên bản")]
        public string Version { get; set; }
        [Description("Năm")]
        public string Year { get; set; }
        [Description("Màu sắc")]
        public string Color { get; set; }
        [Description("Khoảng giá")]
        public string PriceRage { get; set; }
        [Description("TP lái thử xe")]
        public string CityTestDrive { get; set; }
        [ReturnValueField(Field = "StableIncomeName")]
        [Description("Thu nhập ổn định")]
        public bool? StableIncome { get; set; }
        [ReturnValueField(Field = "GoodAddressName")]
        [Description("Có hộ khẩu ở 5 TP TƯ")]
        public bool? GoodAddress { get; set; }
        [Description("Nghề nghiệp")]
        public string Job { get; set; }
        [ReturnValueField(Field = "IncomeName")]
        [Description("Thu nhập")]
        public long? Income { get; set; }
        [ReturnValueField(Field = "LookingOtherCarName")]
        [Description("Có đang tìm kiếm những con xe khác")]
        public bool? LookingOtherCar { get; set; }
        [Description("Xe đang tìm")]
        public string OtherCar { get; set; }
        [ReturnValueField(Field = "GoodActionDealerName")]
        [Description("Dealer có cởi mở")]
        public bool? GoodActionDealer { get; set; }
        [Description("Yếu tố quan trọng để khách hàng cân nhắc")]
        public string DealerConsider { get; set; }
        [ReturnValueField(Field = "IsChangeCarName")]
        [Description("Đổi xe hay mua xe")]
        public bool? IsChangeCar { get; set; }
        [Description("Thời gian đổi xe")]
        public int ChangeCarTime { get; set; }
        [Description("Xe hiện tại")]
        public string CurrentCar { get; set; }
        [ReturnValueField(Field = "IsSoldCarName")]
        [Description("Có nhu cầu bán xe")]
        public bool? IsSoldCar { get; set; }
        [Description("Lý do muốn đổi xe")]
        public string ReasonChangeCar { get; set; }
        [ReturnValueField(Field = "HasWifeName")]
        [Description("Có vợ(chồng)")]
        public bool? HasWife { get; set; }
        [ReturnValueField(Field = "HasChildName")]
        [Description("Có con")]
        public bool? HasChild { get; set; }
        [ReturnValueField(Field = "FamilyIncomeName")]
        [Description("Thu nhập tổng hộ gia đình")]
        public long? FamilyIncome { get; set; }
    }
    //Class này tạo ra mục đích để cop paste enties. Không thao tác
    public class BaseLeadEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? StartCareDate { get; set; }
        public int CityId { get; set; }
        public int SourceLead { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public int PaymentStatus { get; set; }
        public int AssignToId { get; set; }
        public int CampaignType { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string FullTextSearch { get; set; }
        public string Address { get; set; }
        public string ExpectedDescription { get; set; }
        public string Version { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string PriceRage { get; set; }
        public string CityTestDrive { get; set; }
        public bool? StableIncome { get; set; }
        public bool? GoodAddress { get; set; }
        public string Job { get; set; }
        public long? Income { get; set; }
        public bool? LookingOtherCar { get; set; }
        public string OtherCar { get; set; }
        public bool? GoodActionDealer { get; set; }
        public string DealerConsider { get; set; }
        public bool? IsChangeCar { get; set; }
        public DateTime? ChangeCarTime { get; set; }
        public string CurrentCar { get; set; }
        public bool? IsSoldCar { get; set; }
        public string ReasonChangeCar { get; set; }
        public bool? HasWife { get; set; }
        public bool? HasChild { get; set; }
        public long? FamilyIncome { get; set; }
    }
}
