using Microsoft.AspNetCore.Mvc;
using WebFrontToBack.DAL;

namespace WebFrontToBack.Controllers
{
    public class RecentWorksController : Controller
    {

        private readonly AppDbContext _context;
        public RecentWorksController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
