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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Event> events = await _context.Events.Where(e => e.IsDeleted == false).ToListAsync();


            return View(events);
        }


        public async Task<IActionResult> Detail(int? id)
        {
            EventVM eventVM = new EventVM
            {
                Event = await _context.Events
                .Include(e => e.CourseCategory)
                .Include(e => e.EventSpeakers)
                .ThenInclude(es => es.Speaker)
                .Include(e => e.EventTags)
                .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id),
                Blog = await _context.Blogs.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id),
                Blogs = await _context.Blogs.Where(b => b.IsDeleted == false).ToListAsync(),
                Tags = _context.Tags.Include(t => t.EventTags).ThenInclude(ev => ev.Event).ToList(),
                CourseCategories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync(),

            };

            return View(eventVM);

        }

        public async Task<IActionResult> CategoryFindEvent(int? id)
        {

            List<Event> events = await _context.Events
                .Include(c => c.CourseCategory)
                .Where(c => c.IsDeleted == false && c.CourseCategoryId == id).ToListAsync();

            if (events == null)
            {
                return NotFound("Id tapilmadi");
            }

            return View(events);
        }

        public async Task<IActionResult> TagFindEvent(int? id)
        {

            List<Event> events = await _context.Events
                .Include(c => c.EventTags)
                .ThenInclude(ct => ct.Tag)
                .Where(c => c.IsDeleted == false && c.EventTags.Any(ct => ct.TagId == id)).ToListAsync();

            if (events == null)
            {
                return NotFound("Id tapilmadi");
            }


            return View(events);
        }

        public async Task<IActionResult> Search(int? id, string search)
        {


            IEnumerable<SearchVM> events = await _context.Events
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


            return View("_ESearchPartialView", events);
        }


    }
}
