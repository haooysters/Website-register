using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
