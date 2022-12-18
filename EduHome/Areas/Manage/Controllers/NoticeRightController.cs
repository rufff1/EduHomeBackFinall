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
    public class NoticeRightController : Controller
    {

        private readonly AppDbContext _context;

        public NoticeRightController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            NoticeRight noticeRight = await _context.NoticeRights.FirstOrDefaultAsync(b => b.IsDeleted == false);
            if (noticeRight == null)
            {
                return NotFound("Duzgun melumat daxil edin");
            }

            return View(noticeRight);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }
            NoticeRight noticeRight = await _context.NoticeRights.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (noticeRight == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }
            return View(noticeRight);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, NoticeRight noticeRight)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            NoticeRight existednoticeRight = await _context.NoticeRights.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (existednoticeRight == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            noticeRight.UpdateAt = DateTime.UtcNow.AddHours(4);
            noticeRight.UpdateBy = "System";
            noticeRight.IsDeleted = false;
            existednoticeRight.Title = noticeRight.Title;
            existednoticeRight.Description = noticeRight.Description;
            await _context.SaveChangesAsync();
            return View("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest("Id bos ola bilmez");
        //    }

        //    NoticeRight noticeRight = await _context.NoticeRights.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

        //    if (noticeRight == null)
        //    {
        //        return NotFound("Daxil edilen Id yalnisdir");
        //    }

        //    if (noticeRight.Id != id)
        //    {
        //        return BadRequest("Id bos ola bilmez");
        //    }


        //    noticeRight.IsDeleted = true;
        //    noticeRight.DeletedAt = DateTime.UtcNow.AddHours(4);
        //    noticeRight.DeletedBy = "System";

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("index");
        //}

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }
            NoticeRight noticeRight = await _context.NoticeRights.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (noticeRight == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(noticeRight);
        }
    }
}
