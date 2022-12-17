using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhoaHocOnline.Models;

namespace WebDangKyKhoaHocOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminThongKeController : Controller
    {
        private readonly db_WebElerningContext _context;

        public AdminThongKeController(db_WebElerningContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminThongKe
        public async Task<IActionResult> Index()
        {
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDonDangKy = ThongKeDonDangKy();
            ViewBag.TongHocVien = ThongKeHocVien();
            var db_WebElerningContext = _context.OrderDetails.Include(o => o.Course).Include(o => o.Order);
            return View();
        }

        // GET: Admin/AdminThongKe/Details/5
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

        // GET: Admin/AdminThongKe/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: Admin/AdminThongKe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,CourseId,Quantity,TotalMoney")] OrderDetail orderDetail)
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

        // GET: Admin/AdminThongKe/Edit/5
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", orderDetail.CourseId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: Admin/AdminThongKe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,CourseId,Quantity,TotalMoney")] OrderDetail orderDetail)
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", orderDetail.CourseId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: Admin/AdminThongKe/Delete/5
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

        // POST: Admin/AdminThongKe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        }

        //Tổng doanh thu từ khi thành lập web
        public Decimal ThongKeDoanhThu()
        {
            decimal TongDoanhThu = _context.OrderDetails.Sum(n => n.TotalMoney).Value;

            return TongDoanhThu;
        }

        //Thống kê đơn đăng ký
        public Double ThongKeDonDangKy()
        {
            double ddk = _context.OrderDetails.Count();

            return ddk;
        }

        //Thống kê tổng số học viên 
        public Double ThongKeHocVien()
        {
            double hv = _context.Orders.Count();

            return hv;
        }
    }
}
