using EduHome.Dal;
using EduHome.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.CourseCategories.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<CourseCategory> categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCategory courseCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (courseCategory.Name == null)
            {
                ModelState.AddModelError("Name", "Category adi daxil edin");
                return View(courseCategory);

            }
            if (await _context.CourseCategories.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == courseCategory.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This name {courseCategory.Name} already exists");
                return View(courseCategory);

            }
            courseCategory.Name = courseCategory.Name.Trim();
            courseCategory.IsDeleted = false;
            courseCategory.CreatAt = DateTime.UtcNow.AddHours(4);
            courseCategory.CreatBy = "System";

            await _context.CourseCategories.AddAsync(courseCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            CourseCategory courseCategory = await _context.CourseCategories.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);


            if (courseCategory == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }
            return View(courseCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CourseCategory courseCategory)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            CourseCategory existedCategory = await _context.CourseCategories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (existedCategory == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            if (courseCategory.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            if (courseCategory.Name == null)
            {
                ModelState.AddModelError("Name", "Category adi daxil edin");
                return View(courseCategory);

            }

            bool isExist = _context.CourseCategories.Any(c => c.Name.ToLower() == courseCategory.Name.ToLower().Trim());
            if (isExist && !(existedCategory.Name.ToLower() == courseCategory.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Category var");
                return View();
            };

            existedCategory.Name = courseCategory.Name.Trim();
            courseCategory.UpdateAt = DateTime.UtcNow.AddHours(4);
            courseCategory.UpdateBy = "System";
            courseCategory.IsDeleted = false;


            await _context.SaveChangesAsync();


            return RedirectToAction("Index");



        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            CourseCategory courseCategory = await _context.CourseCategories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (courseCategory == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(courseCategory);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            CourseCategory courseCategory = await _context.CourseCategories
    .Include(c => c.Events)
    .Include(c => c.Blogs)
    .Include(c => c.Courses)
    .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (courseCategory == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (courseCategory.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }




            if (courseCategory.Blogs.Count() > 0 || courseCategory.Events.Count() > 0 || courseCategory.Courses.Count() > 0)
            {
                return BadRequest();
            }

            courseCategory.IsDeleted = true;
            courseCategory.DeletedAt = DateTime.UtcNow.AddHours(4);
            courseCategory.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { status = 200 });

        }


    }
}
