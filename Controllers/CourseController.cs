using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDangKyKhoaHocOnline.Models;

namespace WebDangKyKhoaHocOnline.Controllers
{
    public class CourseController : Controller
    {
        private readonly db_WebElerningContext _context;

        public CourseController(db_WebElerningContext context)
        {
             _context = context;
        }
        [Route("course.html", Name = "Course")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 6;
                var lsCourses = _context.Courses
                    .AsNoTracking()
                    .OrderByDescending(x => x.CourseId);

                //Phân trang danh sách các khóa học 
                PagedList<Course> models = new PagedList<Course>(lsCourses, pageNumber, pageSize);

                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }          
        }
        [Route("/{CourseName}-{id}.html", Name = "CourseDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var course = _context.Courses.Include(x => x.Cat).FirstOrDefault(x => x.CourseId == id);
                if (course == null)
                {
                    return RedirectToAction("Index");
                }
                return View(course);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
