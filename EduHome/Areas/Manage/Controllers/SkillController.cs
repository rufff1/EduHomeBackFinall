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
    public class SkillController : Controller
    {

        private readonly AppDbContext _context;

        public SkillController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Skills.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<Skill> skills = await _context.Skills.Where(c => c.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            return View(skills);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (skill.Name == null)
            {
                ModelState.AddModelError("Name", "Skill adi daxil edin");
                return View(skill);

            }
            if (await _context.Skills.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == skill.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This name {skill.Name} already exists");
                return View(skill);

            }
            skill.Name = skill.Name.Trim();
            skill.IsDeleted = false;
            skill.CreatAt = DateTime.UtcNow.AddHours(4);
            skill.CreatBy = "System";

            await _context.Skills.AddAsync(skill);
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

            Skill skill = await _context.Skills.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);


            if (skill == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }
            return View(skill);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Skill skill)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Skill existedSkill = await _context.Skills.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (existedSkill == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            if (skill.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            if (skill.Name == null)
            {
                ModelState.AddModelError("Name", "skill adi daxil edin");
                return View(skill);

            }

            bool isExist = _context.Skills.Any(c => c.Name.ToLower() == skill.Name.ToLower().Trim());
            if (isExist && !(existedSkill.Name.ToLower() == skill.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Category var");
                return View();
            };

            existedSkill.Name = skill.Name.Trim();
            skill.UpdateAt = DateTime.UtcNow.AddHours(4);
            skill.UpdateBy = "System";
            skill.IsDeleted = false;

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
            Skill skill = await _context.Skills.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);


            if (skill == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            return View(skill);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Skill skill = await _context.Skills
                .Include(s => s.TeacherSkills)
                .FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);


            if (skill == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }
            if (skill.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }
            if (skill.TeacherSkills.Count() > 0)
            {
                return Json(new { status = 400 });
            }

            skill.IsDeleted = true;
            skill.DeletedAt = DateTime.UtcNow.AddHours(4);
            skill.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }



    }
}
