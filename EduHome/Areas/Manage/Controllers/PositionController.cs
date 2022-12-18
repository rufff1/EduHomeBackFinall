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
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.TeacherPositions.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<TeacherPosition> positions = await _context.TeacherPositions.Where(c => c.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            return View(positions);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherPosition teacherPosition)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (teacherPosition.Name == null)
            {
                ModelState.AddModelError("Name", "Poisition adi daxil edin");
                return View(teacherPosition);

            }
            if (await _context.TeacherPositions.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == teacherPosition.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This name {teacherPosition.Name} already exists");
                return View(teacherPosition);

            }
            teacherPosition.Name = teacherPosition.Name.Trim();
            teacherPosition.IsDeleted = false;
            teacherPosition.CreatAt = DateTime.UtcNow.AddHours(4);
            teacherPosition.CreatBy = "System";

            await _context.TeacherPositions.AddAsync(teacherPosition);
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

            TeacherPosition position = await _context.TeacherPositions.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);


            if (position == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }
            return View(position);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, TeacherPosition teacherPosition)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            TeacherPosition existedteacherPosition = await _context.TeacherPositions.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (existedteacherPosition == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            if (teacherPosition.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            if (teacherPosition.Name == null)
            {
                ModelState.AddModelError("Name", "position adi daxil edin");
                return View(teacherPosition);

            }

            bool isExist = _context.TeacherPositions.Any(c => c.Name.ToLower() == teacherPosition.Name.ToLower().Trim());
            if (isExist && !(existedteacherPosition.Name.ToLower() == teacherPosition.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Category var");
                return View();
            };

            existedteacherPosition.Name = teacherPosition.Name.Trim();
            teacherPosition.UpdateAt = DateTime.UtcNow.AddHours(4);
            teacherPosition.UpdateBy = "System";
            teacherPosition.IsDeleted = false;

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

            TeacherPosition teacherPosition = await _context.TeacherPositions.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (teacherPosition == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(teacherPosition);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            TeacherPosition teacherPosition = await _context.TeacherPositions
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (teacherPosition == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (teacherPosition.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }




            if (teacherPosition.Teachers.Count() > 0)
            {
                return Json(new { status = 400 });
            }



            teacherPosition.IsDeleted = true;
            teacherPosition.DeletedAt = DateTime.UtcNow.AddHours(4);
            teacherPosition.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }
    }
}
