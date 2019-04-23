using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.WebUI.Areas.Admin.Models
{
    public class LoginViewModel
    {
        public int UserID { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(50, ErrorMessage = "Tài khoản đã quá mức 50 ký tự")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(200, ErrorMessage = "Mật khẩu đã quá mức 200 ký tự")]
        public string Password { get; set; }
    }
}