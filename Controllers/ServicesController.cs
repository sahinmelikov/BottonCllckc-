using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.dbServiceCount=await _context.Services.CountAsync();

          return View(await _context.Services
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .Take(8)
                .Include(s => s.ServiceImages)
                .ToListAsync());
        }
        //public async Task<IActionResult> LoadMore()
        //{
        //    return Json(await _context.Services
        //       .OrderByDescending(s => s.Id)
        //       .Where(s => !s.IsDeleted)
        //       .Take(8)
        //       //.Include(s => s.ServiceImages)
        //       .ToListAsync());
        //}
        public async Task<IActionResult> LoadMore(int skip=0,int take=8)
        {
            List<Service>services=await _context.Services
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .Skip(skip)
                .Take(take)
                .Include(s => s.ServiceImages)
                .ToListAsync();
            return PartialView("_ServicesPartialView", services);
        }


    }
}
