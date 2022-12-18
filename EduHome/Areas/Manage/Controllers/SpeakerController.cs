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
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public SpeakerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Speakers.Count() / 3);
            ViewBag.CurrentPage = page;
            IEnumerable<Speaker> speakers = await _context.Speakers.Where(s => s.IsDeleted == false).Skip((page - 1) * 3).Take(5).ToListAsync();

            return View(speakers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speaker speaker)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }


            if (speaker.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!speaker.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!speaker.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            speaker.Image = speaker.ImageFile.CreateImage(_env, "assets", "img", "event");
            speaker.CreatBy = "System";
            speaker.IsDeleted = false;
            speaker.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Speakers.AddAsync(speaker);
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

            Speaker speaker = await _context.Speakers.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);


            if (speaker == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(speaker);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {


            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Speaker speaker = await _context.Speakers.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (speaker == null) return NotFound("speaker tapilmadi");
            return View(speaker);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Speaker speaker)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (id == null) return BadRequest("Id daxil edin");

            if (speaker.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Speaker existedSpeaker = await _context.Speakers.FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (existedSpeaker == null) return NotFound("Teacher tapilmadi");

            if (existedSpeaker.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!existedSpeaker.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!existedSpeaker.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedSpeaker.Image, "assets", "img", "event");
            existedSpeaker.Image = speaker.ImageFile.CreateImage(_env, "assets", "img", "event");
            existedSpeaker.FullName = speaker.FullName.Trim();
            existedSpeaker.Position = speaker.Position;
            speaker.UpdateAt = DateTime.UtcNow.AddHours(4);
            speaker.IsDeleted = false;
            speaker.UpdateBy = "System";
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

            Speaker speaker = await _context.Speakers
                .Include(s => s.EventSpeakers)
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (speaker == null) return NotFound("speaker tapilmadi");

            if (speaker.EventSpeakers.Count() > 0)
            {
                return Json(new { status = 400 });
            }

            if (speaker.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            speaker.IsDeleted = true;
            speaker.DeletedAt = DateTime.UtcNow.AddHours(4);
            speaker.DeletedBy = "System";
            await _context.SaveChangesAsync();

            return View("Index");
        }
    }
}
