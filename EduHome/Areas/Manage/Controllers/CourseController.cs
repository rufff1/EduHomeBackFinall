using EduHome.Dal;
using EduHome.Extensions;
using EduHome.Helpers;
using EduHome.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 3);

            IEnumerable<Course> courses = await _context.Courses
                .Include(c => c.CourseCategory)

                .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
                .Where(c => c.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();



            return View(courses);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();



            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();



            if (!await _context.Courses.AnyAsync(t => t.IsDeleted == false && t.Id == course.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "gelen category yalnisdir");
                return View(course);
            }



            if (await _context.Courses.AnyAsync(c => c.IsDeleted == false && c.Name.Trim() == course.Name.Trim()))
            {
                ModelState.AddModelError("Name", $"This name {course.Name} already exists");
                return View(course);

            }


            List<CourseTag> courseTags = new List<CourseTag>();

            foreach (int tagId in course.TagIds)
            {
                if (course.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(course);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(course);
                }

                CourseTag courseTag = new CourseTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                courseTags.Add(courseTag);
            }



            if (course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!course.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!course.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            course.Image = course.ImageFile.CreateImage(_env, "assets", "img", "course");
            course.Name = course.Name.Trim();
            course.CourseTags = courseTags;
            course.CreatBy = "System";
            course.IsDeleted = false;
            course.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");


        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();



            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Course course = await _context.Courses
            .Include(c => c.CourseCategory)

            .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (!await _context.Courses.AnyAsync(t => t.IsDeleted == false && t.Id == course.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "gelen category yalnisdir");
                return View(course);
            }
            if (course == null) return NotFound("Teacher tapilmadi");

            course.TagIds = await _context.CourseTags.Where(bt => bt.CourseId == id).Select(x => x.CourseId).ToListAsync();

            return View(course);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course)
        {

            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();



            if (id == null) return BadRequest("Duzgun Id daxil edin");
            if (id != course.Id) return BadRequest();

            Course existedCourse = await _context.Courses
            .Include(c => c.CourseCategory)

            .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (!await _context.Courses.AnyAsync(t => t.IsDeleted == false && t.Id == course.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "gelen category yalnisdir");
                return View(course);
            }

            if (existedCourse == null) return NotFound("Teacher tapilmadi");

            bool isExist = _context.Courses.Any(c => c.Name.ToLower() == course.Name.ToLower().Trim());
            if (isExist && !(existedCourse.Name.ToLower() == course.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Category var");
                return View();
            };

            _context.CourseTags.RemoveRange(existedCourse.CourseTags);

            List<CourseTag> courseTags = new List<CourseTag>();

            foreach (int tagId in course.TagIds)
            {
                if (course.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(course);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(course);
                }

                CourseTag courseTag = new CourseTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                courseTags.Add(courseTag);
            }


            if (course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!course.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!course.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedCourse.Image, "assets", "img", "course");
            existedCourse.Image = course.ImageFile.CreateImage(_env, "assets", "img", "course");
            existedCourse.CourseTags = courseTags;
            course.UpdateAt = DateTime.UtcNow.AddHours(4);
            course.UpdateBy = "System";
            course.IsDeleted = false;
            existedCourse.About = course.About;
            existedCourse.CERTIFICATION = course.CERTIFICATION;
            existedCourse.CourseCategoryId = course.CourseCategoryId;
            existedCourse.Description = course.Description;
            existedCourse.HowToApply = course.HowToApply;
            existedCourse.LeaveReply = course.LeaveReply;
            existedCourse.Name = course.Name.Trim();


            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Course course = await _context.Courses
            .Include(c => c.CourseCategory)

            .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (course == null) return NotFound("Teacher tapilmadi");



            return View(course);


        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }



            Course course = await _context.Courses
            .Include(c => c.CourseCategory)

            .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (course == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (course.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            course.IsDeleted = true;
            course.DeletedAt = DateTime.UtcNow.AddHours(4);
            course.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

    }
}
