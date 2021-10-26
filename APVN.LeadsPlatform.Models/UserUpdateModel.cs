using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class UserUpdateModel
    {
        public int UserId { get; set; }                             
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class UserUpdateIndexModel : UserUpdateModel
    {
        public string Role { get; set; }
    }
}
