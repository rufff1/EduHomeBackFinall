using EduHome.Dal;
using EduHome.Extensions;
using EduHome.Helpers;
using EduHome.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<Blog> blogs = await _context.Blogs
                .Include(b => b.CourseCategory)
                .Where(b => b.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Blog blog = await _context.Blogs
           .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag)
           .Include(b => b.CourseCategory)
           .FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (blog == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {


            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {

            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();


            if (!ModelState.IsValid)
            {

                return View();
            }

            if (!await _context.CourseCategories.AnyAsync(c => c.IsDeleted == false && c.Id == blog.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "gelen categoriya yalnisdir");
                return View(blog);
            }


            //bos list tuturug secilen taglari elave etmeye asagida elave edirik
            List<BlogTag> blogTags = new List<BlogTag>();

            foreach (int tagId in blog.TagIds)
            {
                if (blog.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(blog);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(blog);
                }

                BlogTag blogTag = new BlogTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };

                //taglari bos liste add etdik
                blogTags.Add(blogTag);
            }


            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!blog.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!blog.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            blog.Image = blog.ImageFile.CreateImage(_env, "assets", "img", "blog");
            blog.BlogTags = blogTags;
            blog.CreatBy = "System";
            blog.IsDeleted = false;
            blog.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();



            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Blog blog = await _context.Blogs
                .Include(t => t.BlogTags).ThenInclude(t => t.Tag)
                .Include(t => t.CourseCategory)
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (blog == null) return NotFound("Teacher tapilmadi");


            blog.TagIds = await _context.BlogTags.Where(bt => bt.BlogId == id).Select(x => x.BlogId).ToListAsync();




            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid)
            {

                return View();
            }

            Blog existedBlog = await _context.Blogs
              .Include(t => t.BlogTags).ThenInclude(t => t.Tag)
              .Include(t => t.CourseCategory)
              .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (blog.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            if (existedBlog == null)
            {
                return NotFound("Teacher tapilmadi");
            }

            if (id == null) return BadRequest("Id daxil edin");
            _context.BlogTags.RemoveRange(existedBlog.BlogTags);

            List<BlogTag> blogTags = new List<BlogTag>();

            foreach (int tagId in blog.TagIds)
            {
                if (blog.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(blog);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(blog);
                }

                BlogTag blogTag = new BlogTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                blogTags.Add(blogTag);
            }


            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!blog.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!blog.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            Helper.DeleteFile(_env, existedBlog.Image, "assets", "img", "blog");
            existedBlog.Image = blog.ImageFile.CreateImage(_env, "assets", "img", "blog");
            existedBlog.BlogTags = blogTags;
            existedBlog.Author = blog.Author;
            existedBlog.CourseCategoryId = blog.CourseCategoryId;
            existedBlog.Description = blog.Description;
            existedBlog.Name = blog.Name;
            existedBlog.QuatoDescrpt = blog.QuatoDescrpt;
            blog.UpdateAt = DateTime.UtcNow.AddHours(4);
            blog.UpdateBy = "System";
            blog.IsDeleted = false;

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


            Blog blog = await _context.Blogs
            .Include(t => t.BlogTags).ThenInclude(t => t.Tag)
            .Include(t => t.CourseCategory)
            .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (blog == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (blog.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            blog.IsDeleted = true;
            blog.DeletedAt = DateTime.UtcNow.AddHours(4);
            blog.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }

    }
}
