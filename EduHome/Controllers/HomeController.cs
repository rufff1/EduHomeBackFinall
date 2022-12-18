using EduHome.Dal;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = await _context.Sliders.Where(s => s.IsDeleted == false).ToListAsync(),
                NoticeBoards = await _context.NoticeBoards.Where(n => n.IsDeleted == false).ToListAsync(),
                NoticeRights = await _context.NoticeRights.Where(n => n.IsDeleted == false).Take(3).ToListAsync(),
                whyEduHome = await _context.WhyEduHomes.FirstOrDefaultAsync(w => w.IsDeleted == false),
                Courses = await _context.Courses.Where(c => c.IsDeleted == false).Take(3).ToListAsync(),
                Events = await _context.Events.Where(c => c.IsDeleted == false).Take(8).ToListAsync(),
                testimional = await _context.Testimionals.FirstOrDefaultAsync(n => n.IsDeleted == false),
                Blogs = await _context.Blogs.Where(c => c.IsDeleted == false).Take(3).ToListAsync()
            };
            return View(homeVM);
        }
    }
}
