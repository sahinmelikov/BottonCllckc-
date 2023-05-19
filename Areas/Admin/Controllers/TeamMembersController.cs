using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.Areas.Admin.ViewModels;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using WebFrontToBack.Utilities.Constants;
using WebFrontToBack.Utilities.Extensions;

namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMembersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //Environment proyektin unvanini verir birbasha

        public TeamMembersController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<TeamMembers> members = await _context.TeamMembers.ToListAsync();
            return View(members);
            //return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeamMemberVM member)
        {
            if (!ModelState.IsValid)//-- InValid demek dogru deyilse demekdir.
            {
                return View();
            }
           
            if (!member.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{member.Photo.FileName} {Messages.FileTypeMustBeImage}");
                return View();
            }
            if (!member.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", $"{member.Photo.FileName} - file type must be size less than 200kb");
                return View();
            }

            //string root = _webHostEnvironment.WebRootPath;
            //string resultRoot = Path.Combine(root, "assets", "img");---------//shekilleri save elemeyim ucundur bu setir
            //string fileName=Guid.NewGuid().ToString()+member.Photo.fileName; -----  //bu ise yalniz ve yalniz bazaya elave etmeyim ucundur tek fileNameni elave edirem yeni
             //return Json(fileName);

            string root = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");
           string fileName = await member.Photo.SaveAsync(root);//shekillerin unvanini verdim
            //string root= _webHostEnvironment.WebRootPath;
            //string fileName = Guid.NewGuid().ToString() + member.Photo.fileName;
            //string resultPath=Path.Combine(root, "assets", "img",fileName);
            //using (FileStream filestream = new FileStream(resultPath, FileMode.Create))
            //{
            //    await member.Photo.CopyToAsync(filestream);
            //}
            TeamMembers teamMember = new TeamMembers()
            {
                FullName = member.FullName,
                ImagePath = fileName,
                Profession = member.Profession
            };
            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            TeamMembers member = await _context.TeamMembers.FindAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeamMembers member)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            TeamMembers result = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == member.Id);
            if (result is null)
            {
                TempData["Exists"] = "Bu Member bazada yoxdur";
                return RedirectToAction(nameof(Index));
            }
            result.FullName = member.FullName;
            result.Profession = member.Profession;
            result.ImagePath = member.ImagePath;
            _context.TeamMembers.Update(result);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {

            //TeamMember teamMember = await _context.TeamMembers.FindAsync(id);
            //if (teamMember == null) return NotFound();
            //string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", teamMember.ImagePath);
            //if (System.IO.File.Exists(imagePath))
            //{
            //    System.IO.File.Delete(imagePath);
            //}

            TeamMembers? member = _context.TeamMembers.Find(Id);
            if (member == null)
            {
                return NotFound();
            }
            _context.TeamMembers.Remove(member);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
