using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    

    public class LoginModal
    {
        

        [Required]
        [Display(Name = "Username")]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        public bool RememberMe { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword
        {
            get;
            set;
        }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword
        {
            get;
            set;
        }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword_CHG
        {
            get;
            set;
        }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword_CHG
        {
            get;
            set;
        }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword_CHG
        {
            get;
            set;
        }


        
        public string CaptchaText
        {
            get;
            set;
        }



        public string UserId { get; set; }
        //[Required]
        //public string UserName { get; set; }
        [Required]
        public string UserNameHindi { get; set; }
        [Required]
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public short yojana_code { get; set; }
        public byte[] UserImage { get; set; }
        public string Designation { get; set; }
    }
}