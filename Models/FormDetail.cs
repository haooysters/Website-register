using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class FormDetail
    {
        public int FormDetailId { get; set; }
        public int? FormId { get; set; }
        public int? CourseId { get; set; }
        public int? Total { get; set; }

        public virtual Course Course { get; set; }
        public virtual Form Form { get; set; }
    }
}
