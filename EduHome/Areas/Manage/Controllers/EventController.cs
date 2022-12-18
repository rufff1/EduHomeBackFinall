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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 3);

            ViewBag.CurrentPage = page;

            IEnumerable<Event> events = await _context.Events
                .Include(e => e.CourseCategory)
                .Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker)
                .Include(e => e.EventTags).ThenInclude(et => et.Tag)
                .Where(e => e.IsDeleted == false).Skip((page - 1) * 3).Take(3).ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();
            ViewBag.Speakers = await _context.Speakers.Where(s => s.IsDeleted == false).ToListAsync();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event eventt)
        {

            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();
            ViewBag.Speakers = await _context.Speakers.Where(s => s.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid)
            {

                return View();
            }

            if (eventt == null)
            {
                ModelState.AddModelError("", "gelen obyekt yalnisdir");
                return View(eventt);
            }

            if (!await _context.CourseCategories.AnyAsync(c => c.IsDeleted == false && c.Id == eventt.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "gelen categoriya yalnisdir");
                return View(eventt);
            }


            List<EventTag> eventTags = new List<EventTag>();

            foreach (int tagId in eventt.TagIds)
            {
                if (eventt.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(eventt);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(eventt);
                }

                EventTag eventTag = new EventTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                eventTags.Add(eventTag);
            }


            if (eventt.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!eventt.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!eventt.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            eventt.Image = eventt.ImageFile.CreateImage(_env, "assets", "img", "event");
            eventt.EventTags = eventTags;
            eventt.CreatBy = "System";
            eventt.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Events.AddAsync(eventt);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();
            ViewBag.Speakers = await _context.Speakers.Where(s => s.IsDeleted == false).ToListAsync();

            if (id == null) return BadRequest("Duzgun Id daxil edin");

            Event eventt = await _context.Events
                .Include(e => e.EventTags).ThenInclude(et => et.Tag)
                .Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker)
                 .Include(e => e.CourseCategory)
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            if (eventt == null) return NotFound("Event tapilmadi");


            eventt.TagIds = await _context.EventTags.Where(bt => bt.EventId == id).Select(x => x.EventId).ToListAsync();
            eventt.SpeakerIds = await _context.EventSpeakers.Where(es => es.EventId == id).Select(x => x.EventId).ToListAsync();



            return View(eventt);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Event eventt)
        {
            ViewBag.Categories = await _context.CourseCategories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();
            ViewBag.Speakers = await _context.Speakers.Where(s => s.IsDeleted == false).ToListAsync();



            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null) return BadRequest("Id daxil edin");

            Event existedEvent = await _context.Events
              .Include(e => e.EventTags).ThenInclude(et => et.Tag)
              .Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker)
               .Include(e => e.CourseCategory)
              .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            if (eventt.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }

            if (existedEvent == null)
            {
                return NotFound("Teacher tapilmadi");
            }


            _context.EventTags.RemoveRange(existedEvent.EventTags);

            List<EventTag> eventTags = new List<EventTag>();

            foreach (int tagId in eventt.TagIds)
            {
                if (eventt.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(eventt);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(eventt);
                }

                EventTag eventTag = new EventTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                eventTags.Add(eventTag);
            }

            _context.EventSpeakers.RemoveRange(existedEvent.EventSpeakers);

            List<EventSpeaker> eventSpeakers = new List<EventSpeaker>();

            foreach (int speakerId in eventt.SpeakerIds)
            {
                if (eventt.SpeakerIds.Where(t => t == speakerId).Count() > 1)
                {
                    ModelState.AddModelError("SpeakerIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(eventt);

                }

                if (!await _context.Speakers.AnyAsync(t => t.IsDeleted == false && t.Id == speakerId))
                {
                    ModelState.AddModelError("SpeakerIds", "secilen tag yalnisdir");
                    return View(eventt);
                }

                EventSpeaker eventSpeaker = new EventSpeaker
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    SpeakerId = speakerId

                };


                eventSpeakers.Add(eventSpeaker);
            }


            if (eventt.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!eventt.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!eventt.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedEvent.Image, "assets", "img", "event");
            existedEvent.Image = eventt.ImageFile.CreateImage(_env, "assets", "img", "event");
            existedEvent.EventTags = eventTags;
            existedEvent.EventSpeakers = eventSpeakers;
            existedEvent.CourseCategoryId = eventt.CourseCategoryId;
            existedEvent.MonthDay = eventt.MonthDay;
            existedEvent.Name = eventt.Name.Trim();
            existedEvent.StartTime = eventt.StartTime;
            eventt.UpdateAt = DateTime.UtcNow.AddHours(4);
            eventt.UpdateBy = "System";
            eventt.IsDeleted = false;
            existedEvent.Venue = eventt.Venue;

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


            Event eventt = await _context.Events
                .Include(e => e.EventTags).ThenInclude(et => et.Tag)
                .Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker)
                 .Include(e => e.CourseCategory)
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);


            if (eventt == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            return View(eventt);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Event eventt = await _context.Events
            .Include(e => e.EventTags).ThenInclude(et => et.Tag)
            .Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker)
             .Include(e => e.CourseCategory)
            .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            if (eventt == null) return NotFound("Event tapilmadi");



            if (eventt.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            eventt.IsDeleted = true;
            eventt.DeletedAt = DateTime.UtcNow.AddHours(4);
            eventt.DeletedBy = "System";


            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
