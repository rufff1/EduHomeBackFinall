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
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Tags.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<Tag> tags = await _context.Tags.Where(c => c.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (tag.Name == null)
            {
                ModelState.AddModelError("Name", "Tag adi daxil edin");
                return View(tag);

            }
            if (await _context.Tags.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == tag.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This name {tag.Name} already exists");
                return View(tag);

            }
            tag.Name = tag.Name.Trim();
            tag.IsDeleted = false;
            tag.CreatAt = DateTime.UtcNow.AddHours(4);
            tag.CreatBy = "System";

            await _context.Tags.AddAsync(tag);
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

            Tag tag = await _context.Tags.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);


            if (tag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }
            return View(tag);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Tag tag)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Tag existedTag = await _context.Tags.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (existedTag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            if (tag.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            if (tag.Name == null)
            {
                ModelState.AddModelError("Name", "tag adi daxil edin");
                return View(tag);

            }
            bool isExist = _context.Tags.Any(c => c.Name.ToLower() == tag.Name.ToLower().Trim());
            if (isExist && !(existedTag.Name.ToLower() == tag.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Category var");
                return View();
            };

            existedTag.Name = tag.Name.Trim();
            tag.UpdateAt = DateTime.UtcNow.AddHours(4);
            tag.UpdateBy = "System";
            tag.IsDeleted = false;

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

            Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (tag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(tag);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Tag tag = await _context.Tags
              .Include(c => c.BlogTags)
               .Include(c => c.CourseTags)
               .Include(c => c.EventTags)
    .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (tag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (tag.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }




            if (tag.BlogTags.Count() > 0 || tag.EventTags.Count() > 0 || tag.CourseTags.Count() > 0)
            {
                return Json(new { status = 400 });
            }

            tag.IsDeleted = true;
            tag.DeletedAt = DateTime.UtcNow.AddHours(4);
            tag.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }

    }
}
