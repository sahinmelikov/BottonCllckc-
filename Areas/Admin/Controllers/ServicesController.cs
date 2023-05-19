using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebFrontToBack.Areas.Admin.ViewModels;
using WebFrontToBack.DAL;

namespace WebFrontToBack.Areas.Admin.Controllers;

[Area("Admin")]
public class ServicesController : Controller
{
    private readonly AppDbContext _context;
    public ServicesController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index() => View(
            await _context.Services
            .Where(s => !s.IsDeleted)
            .OrderByDescending(s => s.Id)
            .Take(8)
            .Include(s => s.Category)
            .Include(s => s.ServiceImages)
            .ToListAsync()
            );
    public async Task<IActionResult> Create()
    {
        CreateServicsVm createServiceVm = new CreateServiceVm()
        {
            Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync()
        };
        return View(createServiceVm);
    }


}
