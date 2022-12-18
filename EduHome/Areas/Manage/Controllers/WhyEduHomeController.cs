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
    public class WhyEduHomeController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public WhyEduHomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            WhyEduHome whyEduHomes = await _context.WhyEduHomes.FirstOrDefaultAsync(s => s.IsDeleted == false);

            return View(whyEduHomes);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            WhyEduHome whyEduHome = await _context.WhyEduHomes.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);



            if (whyEduHome == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(whyEduHome);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {


            if (id == null) return BadRequest("Duzgun Id daxil edin");

            WhyEduHome whyEduHome = await _context.WhyEduHomes.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (whyEduHome == null) return NotFound("Sehife tapilmadi");
            return View(whyEduHome);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, WhyEduHome whyEduHome)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (id == null) return BadRequest("Id daxil edin");

            if (whyEduHome.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            WhyEduHome existedwhyEduHome = await _context.WhyEduHomes.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (existedwhyEduHome == null) return NotFound("Teacher tapilmadi");

            if (whyEduHome.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!whyEduHome.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!whyEduHome.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedwhyEduHome.Image, "assets", "img", "choose");
            existedwhyEduHome.Image = whyEduHome.ImageFile.CreateImage(_env, "assets", "img", "choose");
            existedwhyEduHome.Description = whyEduHome.Description;
            existedwhyEduHome.Title = whyEduHome.Title;
            whyEduHome.UpdateAt = DateTime.UtcNow.AddHours(4);
            whyEduHome.IsDeleted = false;
            whyEduHome.UpdateBy = "System";
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

            WhyEduHome whyEduHome = await _context.WhyEduHomes.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (whyEduHome == null) return NotFound("Teacher tapilmadi");

            if (whyEduHome.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            if (whyEduHome.Image.Count() > 3)
            {
                whyEduHome.IsDeleted = true;
                whyEduHome.DeletedAt = DateTime.UtcNow.AddHours(4);
                whyEduHome.DeletedBy = "System";
                await _context.SaveChangesAsync();

            }




            return View("Index");
        }
    }
}
