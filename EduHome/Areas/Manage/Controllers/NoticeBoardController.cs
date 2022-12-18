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
    public class NoticeBoardController : Controller
    {

        private readonly AppDbContext _context;

        public NoticeBoardController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.NoticeBoards.Count() / 3);

            ViewBag.CurrentPage = page;
            IEnumerable<NoticeBoard> noticeBoards = await _context.NoticeBoards.Where(n => n.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            if (noticeBoards == null)
            {
                return NotFound("Duzgun melumat daxil edin");
            }

            return View(noticeBoards);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoticeBoard noticeBoard)
        {

            if (!ModelState.IsValid)
            {

                return View();
            }

            if (noticeBoard == null)
            {
                ModelState.AddModelError("", "gelen obyekt yalnisdir");
                return View(noticeBoard);
            }


            noticeBoard.Description = noticeBoard.Description;
            noticeBoard.Date = noticeBoard.Date;
            noticeBoard.CreatBy = "System";
            noticeBoard.IsDeleted = false;
            noticeBoard.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.NoticeBoards.AddAsync(noticeBoard);
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
            NoticeBoard noticeBoard = await _context.NoticeBoards.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (noticeBoard == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }
            return View(noticeBoard);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, NoticeBoard noticeBoard)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            NoticeBoard existednoticeBoard = await _context.NoticeBoards.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (existednoticeBoard == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            noticeBoard.UpdateAt = DateTime.UtcNow.AddHours(4);
            noticeBoard.UpdateBy = "System";
            noticeBoard.IsDeleted = false;
            existednoticeBoard.Date = noticeBoard.Date;
            existednoticeBoard.Description = noticeBoard.Description;
            await _context.SaveChangesAsync();
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            NoticeBoard noticeBoard = await _context.NoticeBoards.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (noticeBoard == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            if (noticeBoard.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            noticeBoard.IsDeleted = true;
            noticeBoard.DeletedAt = DateTime.UtcNow.AddHours(4);
            noticeBoard.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }
            NoticeBoard noticeBoard = await _context.NoticeBoards.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (noticeBoard == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(noticeBoard);
        }
    }
}
