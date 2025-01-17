﻿using AspNetCoreHero.ToastNotification.Abstractions;
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
    public class SearchController : Controller
    {
        private readonly db_WebElerningContext _context;
        public INotyfService _notifyService { get; }
        public SearchController(db_WebElerningContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }


        [Route("search.html", Name = "Search")]
        public IActionResult KQSearch(string sKey)
        {

           

            // tim kiem theo ten khoa hoc
            var lsTKH = _context.Courses.Where(n => n.CourseName.Contains(sKey));

            if (lsTKH.Count() == 0)
            {
                _notifyService.Error("Không tìm thấy khóa học nào !");
                return RedirectToAction("Index", "Home");                         
            } 
            return View(lsTKH.OrderBy(n => n.CourseName));
        }
    }
}
