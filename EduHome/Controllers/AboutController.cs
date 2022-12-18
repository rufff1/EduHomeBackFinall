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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                WelcomeCourse = await _context.WelcomeCourses.FirstOrDefaultAsync(w => w.IsDeleted == false),
                Teachers = await _context.Teachers.Where(t => t.IsDeleted == false)
                .Include(t => t.TeacherPosition)
                .Take(4).ToListAsync(),
                NoticeRights = await _context.NoticeRights.Where(n => n.IsDeleted == false).Take(3).ToListAsync(),
                testimional = await _context.Testimionals.FirstOrDefaultAsync(t => t.IsDeleted == false),
                NoticeBoards = await _context.NoticeBoards.Where(n => n.IsDeleted == false).ToListAsync()

            };
            return View(aboutVM);
        }
    }
}
