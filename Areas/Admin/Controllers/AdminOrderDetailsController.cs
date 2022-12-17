using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebDangKyKhoaHocOnline.Models;

namespace WebDangKyKhoaHocOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminOrderDetailsController : Controller
    {
        private readonly db_WebElerningContext _context;

        public INotyfService _notifyService { get; }

        public AdminOrderDetailsController(db_WebElerningContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminOrderDetails
    
        public IActionResult Index(int page = 1, int CourseID = 0)
        {
            var pageNumber = page;
            var pageSize = 5;

            List<OrderDetail> lsOrderDetails = new List<OrderDetail>();
            if (CourseID != 0)
            {
                lsOrderDetails = _context.OrderDetails.Include(o => o.Course).Include(o => o.Order)
                .AsNoTracking()
                .Where(x => x.CourseId == CourseID)
                .Include(x => x.Course)
                .OrderByDescending(x => x.OrderDetailId).ToList();
            }
            else
            {
                lsOrderDetails = _context.OrderDetails.Include(o => o.Course).Include(o => o.Order)
                .AsNoTracking()
                .Include(x => x.Course)
                .OrderByDescending(x => x.OrderDetailId).ToList();
            }
            

            PagedList<OrderDetail> models = new PagedList<OrderDetail>(lsOrderDetails.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentCauseID = CourseID;
            ViewBag.CurrentPage = pageNumber;

            ViewData["KhoaHoc"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["HocVien"] = new SelectList(_context.Orders, "OrderId", "StudentName");
            return View(models);
        }

        public IActionResult Filtter(int CourseID = 0)
        {
            var url = $"/Admin/AdminOrderDetails?CourseID={CourseID}";
            if (CourseID == 0)
            {
                url = $"/Admin/AdminOrderDetails";
            }
            return Json(new { status = "success", redirectUrl = url });
        }



        // GET: Admin/AdminOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Course)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: Admin/AdminOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: Admin/AdminOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,CourseId,Quantity,Price,Total")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", orderDetail.CourseId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: Admin/AdminOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            ViewData["HocVien"] = new SelectList(_context.Orders, "OrderId", "StudentName", orderDetail.OrderId);
            ViewData["KhoaHoc"] = new SelectList(_context.Courses, "CourseId", "CourseName", orderDetail.CourseId);
            return View(orderDetail);
        }

        // POST: Admin/AdminOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,CourseId,Quantity,Price,TotalMoney")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhoaHoc"] = new SelectList(_context.Courses, "CourseId", "CourseName", orderDetail.CourseId);
            ViewData["HocVien"] = new SelectList(_context.Orders, "OrderId", "StudentName", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: Admin/AdminOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Course)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: Admin/AdminOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            _notifyService.Error("Xóa đơn đăng ký thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        }
    }
}
