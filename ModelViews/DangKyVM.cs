using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKyKhoaHocOnline.ModelViews
{
    public class DangKyVM
    {
        [Key]
        public int OrderID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Họ và Tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        [Display(Name = "Điện thoại")]
        [MaxLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chọn ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Credit card number")]
        [Display(Name = "Credit card number")]
        [DataType(DataType.CreditCard)]
        public int CreditCard { get; set; }
    }
}
