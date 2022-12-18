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
    public class BlogController : Controller
    {

        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<Blog> blogs = await _context.Blogs
                .Where(b => b.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();



            return View(blogs);
        }


        public async Task<IActionResult> Detail(int? id)
        {
            BlogVM blogVM = new BlogVM
            {
                Blog = await _context.Blogs
                .Include(b => b.CourseCategory)
                .Include(b => b.BlogTags)
                .ThenInclude(bt => bt.Tag)
                .FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id),
                CourseCategories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync(),
                Tags = _context.Tags.Include(t => t.BlogTags).ThenInclude(ev => ev.Blog).ToList(),
                Blogs = await _context.Blogs.Where(b => b.IsDeleted == false).ToListAsync(),
            };

            if (!await _context.Blogs.AnyAsync(b => b.Id == id))
            {
                return NotFound("Melumat yalnisdir");
            }

            return View(blogVM);
        }

        public async Task<IActionResult> CategoryFindBlog(int? id)
        {

            List<Blog> blogs = await _context.Blogs
                .Include(c => c.CourseCategory)
                .Where(c => c.IsDeleted == false && c.CourseCategoryId == id).ToListAsync();

            if (blogs == null)
            {
                return NotFound("Id tapilmadi");
            }

            return View(blogs);
        }

        public async Task<IActionResult> TagFindBlog(int? id)
        {

            List<Blog> blogs = await _context.Blogs
                .Include(c => c.BlogTags)
                .ThenInclude(ct => ct.Tag)
                .Where(c => c.IsDeleted == false && c.BlogTags.Any(ct => ct.TagId == id)).ToListAsync();

            if (blogs == null)
            {
                return NotFound("Id tapilmadi");
            }


            return View(blogs);
        }
    }
}
