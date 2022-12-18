using EduHome.Dal;
using EduHome.Extensions;
using EduHome.Helpers;
using EduHome.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 3);
            ViewBag.CurrentPage = page;


            IEnumerable<Teacher> teachers = await _context.Teachers
                .Include(t => t.TeacherPosition)
                .Include(t => t.TeacherSkills).ThenInclude(ts => ts.Skill)
                .Where(t => t.IsDeleted == false).Skip((page - 1) * 3).Take(5).ToListAsync();


            return View(teachers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.TeacherSkills = await _context.Skills.Where(s => s.IsDeleted == false).ToListAsync();
            ViewBag.TeacherPosition = await _context.TeacherPositions.Where(h => h.IsDeleted == false).ToListAsync();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            ViewBag.TeacherSkills = await _context.Skills.Where(s => s.IsDeleted == false).ToListAsync();
            ViewBag.TeacherPosition = await _context.TeacherPositions.Where(h => h.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid)
            {

                return View();
            }

            if (!await _context.Teachers.AnyAsync(t => t.IsDeleted == false && t.Id == teacher.TeacherPositionId))
            {
                ModelState.AddModelError("TeacherPositionId", "gelen position yalnisdir");
                return View(teacher);
            }


            if (await _context.Teachers.AnyAsync(c => c.Email.ToLower() == teacher.Email.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla email var");
                return View();
            };
            if (await _context.Teachers.AnyAsync(c => c.Phone.ToLower() == teacher.Phone.ToLower().Trim()))
            {
                ModelState.AddModelError("Phone", "Bu adla nomre var");
                return View();
            };
            if (await _context.Teachers.AnyAsync(c => c.Skype.ToLower() == teacher.Skype.ToLower().Trim()))
            {
                ModelState.AddModelError("", "Bu adla skype var");
                return View();
            };
            if (await _context.Teachers.AnyAsync(c => c.Fblink.ToLower() == teacher.Fblink.ToLower().Trim()))
            {
                ModelState.AddModelError("", "Bu adla fb var");
                return View();
            };

            if (teacher.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!teacher.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!teacher.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            teacher.Image = teacher.ImageFile.CreateImage(_env, "assets", "img", "teacher");
            teacher.CreatBy = "System";
            teacher.IsDeleted = false;
            teacher.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Teachers.AddAsync(teacher);
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

            Teacher teacher = await _context.Teachers
                .Include(t => t.TeacherPosition)
                .Include(t => t.TeacherSkills).ThenInclude(ts => ts.Skill)
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);


            if (teacher == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(teacher);
        }



        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.TeacherSkills = await _context.Skills.Where(s => s.IsDeleted == false).ToListAsync();
            ViewBag.TeacherPosition = await _context.TeacherPositions.Where(h => h.IsDeleted == false).ToListAsync();

            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Teacher teacher = await _context.Teachers
                .Include(t => t.TeacherSkills).ThenInclude(t => t.Skill)
                .Include(t => t.TeacherPosition)
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (teacher == null) return NotFound("Teacher tapilmadi");


            teacher.SkillIds = await _context.TeacherSkills.Where(pt => pt.TeacherId == id).Select(x => x.SkillId).ToListAsync();





            return View(teacher);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teacher teacher)
        {

            ViewBag.TeacherSkills = await _context.Skills.Where(s => s.IsDeleted == false).ToListAsync();
            ViewBag.TeacherPosition = await _context.TeacherPositions.Where(h => h.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid)
            {

                return View();
            }




            if (id == null) return BadRequest("Id daxil edin");

            if (teacher.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Teacher existedTeacher = await _context.Teachers
                .Include(t => t.TeacherSkills).ThenInclude(t => t.Skill)
                .Include(t => t.TeacherPosition)
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (existedTeacher == null) return NotFound("Teacher tapilmadi");

            bool isExist = _context.Teachers.Any(c => c.Phone.ToLower() == teacher.Phone.ToLower().Trim() && c.Email.ToLower().Trim() == teacher.Email.ToLower().Trim());
            if (isExist && !(existedTeacher.Phone.ToLower() == teacher.Phone.ToLower().Trim()))
            {
                ModelState.AddModelError("", "Bu adla Nomre var");
                return View();
            };
            if (isExist && !(existedTeacher.Email.ToLower() == teacher.Email.ToLower().Trim()))
            {
                ModelState.AddModelError("", "Bu adla email var");
                return View();
            };


            _context.TeacherSkills.RemoveRange(existedTeacher.TeacherSkills);

            List<TeacherSkill> teacherSkills = new List<TeacherSkill>();

            foreach (int skillId in teacher.SkillIds)
            {
                if (teacher.SkillIds.Where(t => t == skillId).Count() > 1)
                {
                    ModelState.AddModelError("SkillIds", "bir skill yalniz bir defe secilmelidir");
                    return View(teacher);

                }





                if (!await _context.Skills.AnyAsync(t => t.IsDeleted == false && t.Id == skillId))
                {

                    ModelState.AddModelError("SkillIds", "secilen skill yalnisdir");
                    return View(teacher);
                }

                TeacherSkill teacherSkill = new TeacherSkill
                {
                    UpdateAt = DateTime.UtcNow.AddHours(+4),
                    UpdateBy = "System",
                    IsDeleted = false,
                    SkillId = skillId

                };


                teacherSkills.Add(teacherSkill);
            }

            if (teacher.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!teacher.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!teacher.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedTeacher.Image, "assets", "img", "teacher");
            existedTeacher.Image = teacher.ImageFile.CreateImage(_env, "assets", "img", "teacher");
            existedTeacher.FullName = teacher.FullName.Trim();
            existedTeacher.Experience = existedTeacher.Experience;
            existedTeacher.Email = teacher.Email;
            existedTeacher.About = teacher.About;
            existedTeacher.Degree = teacher.Degree;
            existedTeacher.Faculty = teacher.Faculty;
            existedTeacher.Fblink = teacher.Fblink;
            existedTeacher.Hobby = teacher.Hobby;
            existedTeacher.Phone = teacher.Phone;
            existedTeacher.PinttLink = teacher.PinttLink;
            existedTeacher.VimeoLink = teacher.VimeoLink;
            existedTeacher.Skype = teacher.Skype;
            existedTeacher.TeacherPositionId = teacher.TeacherPositionId;
            existedTeacher.TeacherSkills = teacherSkills;
            teacher.UpdateAt = DateTime.UtcNow.AddHours(4);
            teacher.IsDeleted = false;
            teacher.UpdateBy = "System";
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

            Teacher teacher = await _context.Teachers
               .Include(t => t.TeacherSkills).ThenInclude(t => t.Skill)
               .Include(t => t.TeacherPosition)
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (teacher == null) return NotFound("Teacher tapilmadi");

            if (teacher.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            teacher.IsDeleted = true;
            teacher.DeletedAt = DateTime.UtcNow.AddHours(4);
            teacher.DeletedBy = "System";

            await _context.SaveChangesAsync();


            return View("Index");
        }

    }
}
