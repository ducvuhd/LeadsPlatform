using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Enums
{
    public enum LeadActionSource
    {
        [Description("Organic")]
        Organic = 1,
        [Description("Direct")]
        Direct = 2,
        [Description("Social")]
        Social = 3,
        [Description("FacebookAds")]
        FacebookAds = 4,
        [Description("GoogleAds")]
        GoogleAds = 5,
        [Description("Box thu lead")]
        LeadsCollection = 6
    }
    public enum LeadActionType
    {
        [Description("Tư vấn mua xe")]
        Consultant = 1 << 0,
        [Description("Lái thử")]
        TestDriver = 1 << 1,
        [Description("Trả giá")]
        Bargain = 1 << 2,
        [Description("Trả góp")]
        LoanCar = 1 << 3,
        [Description("Nhận voucher ưu đãi")]
        Voucher = 1 << 4,
        [Description("Lưu tin")]
        AutoSave = 1 << 5,
        [Description("Facebook Form")]
        FacebookForm = 1 << 6,
        [Description("Khác")]
        Other = 1 << 7
    }
    public enum LeadActionExtendsType
    {
        [Description("Tư vấn mua xe")]
        Consultant = 1 << 0,
        [Description("Lái thử")]
        TestDriver = 1 << 1,
        [Description("Trả giá")]
        Bargain = 1 << 2,
        [Description("Trả góp")]
        LoanCar = 1 << 3,
        [Description("Nhận voucher ưu đãi")]
        Voucher = 1 << 4,
        [Description("Lưu tin")]
        AutoSave = 1 << 5,
        [Description("Facebook Form")]
        FacebookForm = 1 << 6,
        [Description("Khác")]
        Other = 1 << 7
    }
    public enum LeadActionSaleSetType
    {
        [Description("Tư vấn mua xe")]
        Consultant = 1 << 0,
        [Description("Lái thử")]
        TestDriver = 1 << 1,
        [Description("Trả giá")]
        Bargain = 1 << 2,
        [Description("Trả góp")]
        LoanCar = 1 << 3,
        [Description("Nhận voucher ưu đãi")]
        Voucher = 1 << 4,
        //[Description("Lưu tin")]
        //AutoSave = 1 << 5,
        //[Description("Facebook Form")]
        //FacebookForm = 1 << 6,
        //[Description("Khác")]
        //Other = 1 << 7
    }
    public enum LeadStatus
    {
        [Description("Mới tạo")]
        New = 1,
        [Description("Đã duyệt")]
        Approved = 2,
        [Description("Không duyệt")]
        DisApproved = 3,

    }
    public enum LeadActionStatus
    {
        [Description("Mới tạo")]
        New = 1,
        [Description("Đã duyệt")]
        Approved = 2,
        [Description("Không duyệt")]
        DisApproved = 3,
        [Description("Đã khai thác")]
        Running = 4,
        /*[Description("Chưa liên hệ")]
        WatingContact = 2,
        [Description("Đã giao dịch thành công")]
        TransactionSuccess = 3,
        [Description("Chờ chốt")]
        WatingConfirm = 4,
        [Description("Từ chối")]
        Rejected = 5,
        [Description("Theo dõi")]
        Following = 6*/
    }
    public enum DealerLeadActionStatus
    {
        [Description("Chưa liên hệ")]
        WatingContact = 1,
        [Description("Đã bán")]
        Done = 2,
        [Description("Chờ chốt")]
        WatingConfirm = 3,
        [Description("Từ chối")]
        Rejected = 4,
        [Description("Theo dõi")]
        Following = 5
    }
    public enum LeadActionPaymentStatus
    {
        [Description("Tặng")]
        Free = 1,
        [Description("Trả phí")]
        Paid = 2,
    }

    public enum LeadsIsActive
    {
        [Description("Khóa")]
        InActive = 0,
        [Description("Mở")]
        Active = 1,
    }


    public enum Secondhand
    {
        [Description("Cũ")]
        Secondhand = 1,
        [Description("Mới")]
        New = 2,
        //[Description("Cả cũ và mới")]
        //Both = 3
    }
    public enum HistoryType
    {
        [Description("History bảng Leads")]
        Leads = 1
    }
    public enum NoteType
    {
        [Description("Note bảng Leads")]
        Lead = 1
    }
    public enum DealerLeadActionAssignType
    {
        [Description("Convert")]
        Convert = 1,
        [Description("Do CS gán")]
        SaleSet = 2,
        [Description("Dealer lấy")]
        DealerGet = 3,
    }
}
