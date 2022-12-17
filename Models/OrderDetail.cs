using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? CourseId { get; set; }
        public int? Quantity { get; set; }
        public int? TotalMoney { get; set; }

        public virtual Course Course { get; set; }
        public virtual Order Order { get; set; }
    }
}
