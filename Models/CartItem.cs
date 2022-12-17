using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKyKhoaHocOnline.Models
{
    public class CartItem
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Thumb { get; set; }
        public int Price { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * Price;
    }
}
