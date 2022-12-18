using EduHome.Dal;
using EduHome.Model;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = await _context.Teachers
                .Include(t => t.TeacherPosition)
                .Where(t => t.IsDeleted == false).ToListAsync();





            return View(teachers);
        }


        public async Task<IActionResult> Detail(int? id)
        {
            Teacher teacher = await _context.Teachers
            .Include(t => t.TeacherSkills).ThenInclude(ts => ts.Skill)
            .Include(t => t.TeacherPosition)
            .Include(t => t.TeacherSkills).ThenInclude(ts => ts.Skill)
            .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);


            if (!_context.Teachers.Any(t => t.Id == id))
            {
                return NotFound("Id yalnisdir");
            }
            return View(teacher);
        }
    }
}
