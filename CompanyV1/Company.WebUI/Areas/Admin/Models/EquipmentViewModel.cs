using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.WebUI.Areas.Admin.Models
{
    public class EquipmentViewModel
    {
        public int EquipmentID { get; set; }

        [Display(Name = "Tên thiết bị")]
        [Required(ErrorMessage = "Tên thiết bị không được để trống")]
        [StringLength(100, ErrorMessage = "Tên thiết bị không được quá 50 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Tình trạng thiết bị")]
        public bool? Status { get; set; }

        [Display(Name = "Loại thiết bị")]
        public int? CategoryID { get; set; }
    }
}