using System;
using System.Collections.Generic;

#nullable disable

namespace WebDangKyKhoaHocOnline.Models
{
    public partial class Page
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string Contents { get; set; }
        public string Title { get; set; }
    }
}
