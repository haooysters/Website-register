using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDangKyKhoaHocOnline.Extension;
using WebDangKyKhoaHocOnline.Models;
using WebDangKyKhoaHocOnline.ModelViews;

namespace WebDangKyKhoaHocOnline.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly db_WebElerningContext _context;
        public INotyfService _notifyService { get; }
        public CheckoutController(db_WebElerningContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(DangKyVM muaHang)
        {
            //Lay ra gio hang de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");           
            try
            {
                if (ModelState.IsValid)
                {
                    //Khoi tao don hang
                    Order donhang = new Order {
                    StudentName = muaHang.FullName,
                    StudentEmail = muaHang.Email,
                    StudentPhone = muaHang.Phone,
                    StudentBirthday = muaHang.Birthday,
                    StudentAddress = muaHang.Address,  
                    CreditCard = muaHang.CreditCard,
                    OrderDate = DateTime.Now
                };                

                    _context.Add(donhang);
                    _context.SaveChanges();
                    //tao danh sach don hang

                    foreach (var item in cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = donhang.OrderId;
                        orderDetail.CourseId = item.CourseID;
                        orderDetail.Quantity = item.SoLuong;                       
                        orderDetail.TotalMoney = Convert.ToInt32(cart.Sum(x => x.ThanhTien));
                        _context.Add(orderDetail);
                    }
                    _context.SaveChanges();
                    //clear gio hang
                    HttpContext.Session.Remove("GioHang");
                    //Xuat thong bao
                    _notifyService.Success("Đã đăng ký thành công !");
                    //cap nhat thong tin khach hang
                    return RedirectToAction("Index", "Home");


                }
            }
            catch
            {
                ViewBag.GioHang = cart;
                return View(muaHang);                
            }
            ViewBag.GioHang = cart;
            return View(muaHang);
        }
    }
}
