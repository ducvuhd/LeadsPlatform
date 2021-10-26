using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Entity
{
    [Serializable]
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public byte? CityID { get; set; }
        public string Yahoo { get; set; }
        public string Facebook { get; set; }
        public string Skype { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public short? Status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string EmailReciveInfo { get; set; }
        public string PhoneOfAutos { get; set; }
        public string PhoneOfAutos1 { get; set; }
        public string PhoneOfAutos2 { get; set; }
        public string BussinessReg { get; set; }
        public string IDCard { get; set; }
        public string TaxCode { get; set; }
        public string TransferMoneyPassword { get; set; }
        public bool IsPhoneVerified { get; set; }

        // Property of object orther
        public bool IsSalon { get; set; }
        public bool IsWarning { get; set; }
        public DateTime PasswordChangedDate { get; set; }
    }
}
