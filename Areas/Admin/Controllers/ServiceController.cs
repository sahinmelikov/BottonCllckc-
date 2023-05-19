using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.Areas.Admin.ViewsModel;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using WebFrontToBack.ViewModel;

namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ServiceController : Controller
    {
        //(List<Service>, List<Category>) ServiceTuple = (services, categories);

        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        
        //Niye asinxrom elemiyende error verir??
        public async Task<IActionResult> Index()
        {
            //ICollection<Service> services = await _context.Services.ToListAsync();
            //return View(services);
            ServiceVM serviceVM = new ServiceVM()
            {
                Services = await _context.Services.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            return View(serviceVM);
        }
        [HttpGet]
        public  IActionResult Create() 
        {

            //Service Services = _context.Services.ToList();
                
      

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
           

            bool isExists = await _context.Categories.AnyAsync(c =>
            c.Name.ToLower().Trim() == service.Name.ToLower().Trim());

            if (isExists)
            {
                ModelState.AddModelError("Name", "Category name already exists");
                return View();
            }
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int Id)
        {
            Service? service = _context.Services.Find(Id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
        [HttpPost]
        public IActionResult Update(Service service)
        {
            Service? editedService = _context.Services.Find(service.Id);
            if (editedService == null)
            {
                return NotFound();
            }
            editedService.Name = service.Name;
            _context.Services.Update(editedService);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            Service? service = _context.Services.Find(Id);
            if (service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
