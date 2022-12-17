using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class Course
    {
        public Course()
        {
            Forms = new HashSet<Form>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int? CatId { get; set; }
        public bool? Active { get; set; }
        public int? Price { get; set; }
        public string Thumb { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
