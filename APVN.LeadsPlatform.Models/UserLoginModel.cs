using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Require input!")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Require input!")]
        [UIHint("stringPassword")]
        public string Password { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Remember account")]
        public bool Remember { get; set; }

        public string ReturnUrl { get; set; }
        [Required(ErrorMessage = "Require input!")]
        public string OTPPrivateKey { get; set; }
    }
}
