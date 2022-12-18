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
    public class CourseController : Controller
    {

        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _context.Courses.Where(c => c.IsDeleted == false).ToListAsync();

            return View(courses);
        }

        public async Task<IActionResult> Search(int? id, string search)
        {


            IEnumerable<SearchVM> courses = await _context.Courses
                .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                .OrderByDescending(p => p.Id)
                .Take(3)
                   .Select(x => new SearchVM
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Image = x.Image
                   })
                   .ToListAsync();


            return View("_CSearchPartialView", courses);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            CourseVM courseVM = new CourseVM
            {
                Course = await _context.Courses
                .Include(c => c.CourseCategory)

                .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id),
                CourseCategories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync(),
                Tags = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).ToList(),
                Courses = await _context.Courses.Where(c => c.IsDeleted == false).ToListAsync(),
                Blogs = await _context.Blogs.Where(b => b.IsDeleted == false).ToListAsync()
            };

            if (!await _context.Courses.AnyAsync(c => c.Id == id))
            {
                return NotFound("Id tapilmadi");
            }
            return View(courseVM);
        }


        public async Task<IActionResult> CategoryFindCourse(int? id)
        {

            List<Course> courses = await _context.Courses
                .Include(c => c.CourseCategory)
                .Where(c => c.IsDeleted == false && c.CourseCategoryId == id).ToListAsync();

            if (courses == null)
            {
                return NotFound("Id tapilmadi");
            }

            return View(courses);
        }

        public async Task<IActionResult> TagFindCourse(int? id)
        {

            List<Course> courses = await _context.Courses
                .Include(c => c.CourseTags)
                .ThenInclude(ct => ct.Tag)
                .Where(c => c.IsDeleted == false && c.CourseTags.Any(ct => ct.TagId == id)).ToListAsync();

            if (courses == null)
            {
                return NotFound("Id tapilmadi");
            }


            return View(courses);
        }

    }
}
