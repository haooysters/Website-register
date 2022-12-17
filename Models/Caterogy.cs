using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class Caterogy
    {
        public Caterogy()
        {
            Courses = new HashSet<Course>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
