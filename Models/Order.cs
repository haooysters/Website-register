using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhone { get; set; }
        public DateTime? StudentBirthday { get; set; }
        public string StudentAddress { get; set; }
        public int? CreditCard { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
