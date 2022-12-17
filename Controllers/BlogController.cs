using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKyKhoaHocOnline.Controllers
{
    public class BlogController : Controller
    {
        [Route("blog.html", Name = "Blog")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
