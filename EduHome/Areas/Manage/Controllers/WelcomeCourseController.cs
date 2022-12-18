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
    public class WelcomeCourseController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public WelcomeCourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            WelcomeCourse welcomeCourse = await _context.WelcomeCourses.FirstOrDefaultAsync(s => s.IsDeleted == false);

            return View(welcomeCourse);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }


            WelcomeCourse welcomeCourse = await _context.WelcomeCourses.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);



            if (welcomeCourse == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(welcomeCourse);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {


            if (id == null) return BadRequest("Duzgun Id daxil edin");

            WelcomeCourse welcomeCourse = await _context.WelcomeCourses.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (welcomeCourse == null) return NotFound("Teacher tapilmadi");
            return View(welcomeCourse);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, WelcomeCourse welcomeCourse)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (id == null) return BadRequest("Id daxil edin");

            if (welcomeCourse.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            WelcomeCourse existewelcomeCourse = await _context.WelcomeCourses.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (existewelcomeCourse == null) return NotFound("Teacher tapilmadi");

            if (welcomeCourse.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!welcomeCourse.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!welcomeCourse.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existewelcomeCourse.Image, "assets", "img", "about");
            existewelcomeCourse.Image = welcomeCourse.ImageFile.CreateImage(_env, "assets", "img", "about");
            existewelcomeCourse.Description = welcomeCourse.Description;
            existewelcomeCourse.Title = welcomeCourse.Title;
            welcomeCourse.UpdateAt = DateTime.UtcNow.AddHours(4);
            welcomeCourse.IsDeleted = false;
            welcomeCourse.UpdateBy = "System";
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

            WelcomeCourse welcomeCourse = await _context.WelcomeCourses.FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (welcomeCourse == null) return NotFound("sehife tapilmadi");

            if (welcomeCourse.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            if (welcomeCourse.Image.Count() > 1)
            {
                welcomeCourse.IsDeleted = true;
                welcomeCourse.DeletedAt = DateTime.UtcNow.AddHours(4);
                welcomeCourse.DeletedBy = "System";
                await _context.SaveChangesAsync();

            }




            return View("Index");
        }
    }
}
