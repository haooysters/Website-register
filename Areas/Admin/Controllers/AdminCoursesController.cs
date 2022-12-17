using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebDangKyKhoaHocOnline.Helpper;
using WebDangKyKhoaHocOnline.Models;

namespace WebDangKyKhoaHocOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCoursesController : Controller
    {
        private readonly db_WebElerningContext _context;

        public INotyfService _notifyService { get; }

        public AdminCoursesController(db_WebElerningContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminCourses
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            var pageNumber = page;
            var pageSize = 5;
            List<Course> lsCourses = new List<Course>();
            if (CatID != 0)
            {
                lsCourses = _context.Courses
                .AsNoTracking()
                .Where(x => x.CatId == CatID)
                .Include(x => x.Cat)
                .OrderByDescending(x => x.CourseId).ToList();
            }
            else
            {
                lsCourses = _context.Courses
                .AsNoTracking()
                .Include(x => x.Cat)
                .OrderByDescending(x => x.CourseId).ToList();
            }

            PagedList<Course> models = new PagedList<Course>(lsCourses.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentCateID = CatID;
            ViewBag.CurrentPage = pageNumber;

            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(models);
        }

        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/AdminCourses?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminCourses";
            }
            return Json(new { status = "success", redirectUrl = url });
        }


        // GET: Admin/AdminCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Cat)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/AdminCourses/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        // POST: Admin/AdminCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,ShortDesc,Description,CatId,Active,Price,Title,Thumb,CreateDate,ModifiedDate")] Course course, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                course.CourseName = Utilities.ToTitleCase(course.CourseName);
                if(fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(course.CourseName) + extension;
                    course.Thumb = await Utilities.UploadFile(fThumb, @"courses", image.ToLower());
                }
                if (string.IsNullOrEmpty(course.Thumb)) course.Thumb = "default.png";
                course.CreateDate = DateTime.Now;

 
                _context.Add(course);
                await _context.SaveChangesAsync();
                _notifyService.Success("Tạo mới thành công!");
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", course.CatId);
            return View(course);
        }

        // GET: Admin/AdminCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", course.CatId);
            return View(course);
        }

        // POST: Admin/AdminCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,ShortDesc,Description,CatId,Active,Price,Title,Thumb,CreateDate,ModifiedDate")] Course course, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {  
                        course.CourseName = Utilities.ToTitleCase(course.CourseName);
                        if (fThumb != null)
                        {
                            string extension = Path.GetExtension(fThumb.FileName);
                            string image = Utilities.SEOUrl(course.CourseName) + extension;
                            course.Thumb = await Utilities.UploadFile(fThumb, @"courses", image.ToLower());
                        }
                        if (string.IsNullOrEmpty(course.Thumb)) course.Thumb = "default.png";
                    
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        _notifyService.Success("Có xảy ra lỗi!");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", course.CatId);
            return View(course);
        }

        // GET: Admin/AdminCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Cat)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admin/AdminCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            _notifyService.Error("Xóa khóa học thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
