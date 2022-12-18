using EduHome.Dal;
using EduHome.Extensions;
using EduHome.Helpers;
using EduHome.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TestimionalController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public TestimionalController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Testimionals.Count() / 3);
            ViewBag.CurrentPage = page;
            IEnumerable<Testimional> testimionals = await _context.Testimionals.Where(s => s.IsDeleted == false).Skip((page - 1) * 3).Take(5).ToListAsync();

            return View(testimionals);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimional testimional)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }


            if (testimional.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!testimional.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!testimional.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            testimional.Image = testimional.ImageFile.CreateImage(_env, "assets", "img", "testimonial");
            testimional.CreatBy = "System";
            testimional.IsDeleted = false;
            testimional.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Testimionals.AddAsync(testimional);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");


        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Testimional testimional = await _context.Testimionals.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);


            if (testimional == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(testimional);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {


            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Testimional testimional = await _context.Testimionals.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (testimional == null) return NotFound("testimional tapilmadi");
            return View(testimional);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Testimional testimional)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (id == null) return BadRequest("Id daxil edin");

            if (testimional.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Testimional existedTestimional = await _context.Testimionals.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (existedTestimional == null) return NotFound("Teacher tapilmadi");

            if (existedTestimional.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!existedTestimional.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!existedTestimional.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedTestimional.Image, "assets", "img", "testimonial");
            existedTestimional.Image = testimional.ImageFile.CreateImage(_env, "assets", "img", "testimonial");
            existedTestimional.Description = testimional.Description;
            existedTestimional.FullName = testimional.FullName.Trim();
            existedTestimional.Position = testimional.Position;
            testimional.UpdateAt = DateTime.UtcNow.AddHours(4);
            testimional.IsDeleted = false;
            testimional.UpdateBy = "System";
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Testimional testimional = await _context.Testimionals.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (testimional == null) return NotFound("Teacher tapilmadi");

            if (testimional.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            testimional.IsDeleted = true;
            testimional.DeletedAt = DateTime.UtcNow.AddHours(4);
            testimional.DeletedBy = "System";
            await _context.SaveChangesAsync();

            return View("Index");
        }
    }
}



