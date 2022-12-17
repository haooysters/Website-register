using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class Form
    {
        public int FormId { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
