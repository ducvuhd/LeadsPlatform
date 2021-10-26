using System;

namespace APVN.LeadsPlatform.Entity
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }
        public DateTime DOB { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int GroupId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Avatar { get; set; }
        public int BdsUserId { get; set; }
        public string BdsUserName { get; set; }
        public DateTime StartWork { get; set; }
        public string OTPPrivateKey { get; set; }
        public string Role { get; set; }

    }
}
