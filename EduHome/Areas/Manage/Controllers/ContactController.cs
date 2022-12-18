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
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public ContactController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            Contact contact = await _context.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false);
            if (contact == null)
            {
                return NotFound("Yalnis melumat daxil etdiniz");
            }

            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return BadRequest("Duzgun Id daxil edin");
            }

            Contact contact = await _context.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (contact == null)
            {
                return NotFound("Yalnis melumat daxil etdiniz");
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Contact contact)
        {
            if (id == null)
            {
                return BadRequest("Duzgun Id daxil edin");
            }

            Contact existedContact = await _context.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (existedContact == null)
            {
                return NotFound("Yalnis melumat daxil etdiniz");
            }

            if (contact.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }



            if (contact.LocFile == null)
            {
                ModelState.AddModelError("LocFile", "Image daxil edin");
                return View();
            }

            if (!contact.LocFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("LocFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!contact.LocFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("LocFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }


            if (contact.PhonFile == null)
            {
                ModelState.AddModelError("LocFile", "Image daxil edin");
                return View();
            }

            if (!contact.PhonFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("LocFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!contact.PhonFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("LocFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            if (contact.AdressFile == null)
            {
                ModelState.AddModelError("LocFile", "Image daxil edin");
                return View();
            }

            if (!contact.AdressFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("LocFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!contact.AdressFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("LocFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedContact.AdressIcon, "assets", "img", "contact");
            contact.AdressIcon = contact.AdressFile.CreateImage(_env, "assets", "img", "contact");

            Helper.DeleteFile(_env, existedContact.PhonIcon, "assets", "img", "contact");
            contact.PhonIcon = contact.PhonFile.CreateImage(_env, "assets", "img", "contact");

            Helper.DeleteFile(_env, existedContact.LocIcon, "assets", "img", "contact");
            contact.LocIcon = contact.LocFile.CreateImage(_env, "assets", "img", "contact");

            existedContact.Adress = contact.Adress;
            existedContact.AdressIcon = contact.AdressIcon;
            existedContact.Location = contact.Location;
            existedContact.LocIcon = contact.LocIcon;
            existedContact.Phone = contact.Phone;
            existedContact.PhonIcon = contact.PhonIcon;
            existedContact.LocLink = contact.LocLink;
            contact.UpdateAt = DateTime.UtcNow.AddHours(4);
            contact.UpdateBy = "System";
            contact.IsDeleted = false;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}
