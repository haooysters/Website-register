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
    public class CartController : Controller
    {
        private readonly db_WebElerningContext _context;
        public INotyfService _notifyService { get; }
        public CartController(db_WebElerningContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        //Khởi tạo giỏ hàng
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
        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            ViewBag.Error = "";
            return View(Carts);
        }

        public IActionResult AddToCart(int id, int SoLuong)
        {            
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.CourseID == id);

            //Kiểm tra khóa học active ?
            var check = _context.Courses
                .Where(s => s.CourseId == id && s.Active == true).FirstOrDefault();
            if (check != null)
            {

                if (item == null)//chưa có
                {
                    var hangHoa = _context.Courses.SingleOrDefault(p => p.CourseId == id);
                    item = new CartItem
                    {
                        CourseID = id,
                        CourseName = hangHoa.CourseName,
                        Price = hangHoa.Price.Value,
                        SoLuong = SoLuong,
                        Thumb = hangHoa.Thumb
                    };
                    myCart.Add(item);
                }
                else
                {
                    item.SoLuong += SoLuong;
                }
                HttpContext.Session.Set("GioHang", myCart);
                _notifyService.Success("Thêm khóa học thành công !");
            }
            else
            {                
                _notifyService.Success("Khóa học tạm thời hết chổ !");
            }
         
            return RedirectToAction("Index");
        }

        //Xóa giỏ hàng
        public ActionResult Remove(int id)
        {


            List<CartItem> gioHang = Carts;
            CartItem item = gioHang.SingleOrDefault(p => p.CourseID == id);
            if (item != null)
            {
                gioHang.Remove(item);
            }
            //luu lai session
            HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
            //Xuat thong bao
            _notifyService.Error("Xóa khóa học thành công");

            return RedirectToAction("Index");

        }

        //Cập nhật giỏ hàng
        public IActionResult UpdateCart(int id, int? SoLuong)
        {
            //Lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");

            if (cart != null)
            {
                CartItem item = cart.SingleOrDefault(p => p.CourseID == id);
                if (item != null && SoLuong.HasValue) // da co -> cap nhat so luong
                {
                    item.SoLuong = SoLuong.Value;
                }
                //Luu lai session
                HttpContext.Session.Set<List<CartItem>>("GioHang", cart);

            }
            return RedirectToAction("Index");
        }


    }
}
