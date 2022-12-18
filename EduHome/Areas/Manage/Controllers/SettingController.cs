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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Setting> setting = await _context.Settings.ToListAsync();
            return View(setting);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(c => c.Id == id);
            if (setting == null) return NotFound();
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Setting setting, int? id)
        {
            if (!ModelState.IsValid) return View(setting);

            if (id == null) return BadRequest("Id null ola bilmez !");

            var settingExists = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id && s.Key == setting.Key);

            if (settingExists == null) return NotFound(" sehife tapilmadi!");




            if (setting.Key == "Logo")
            {
                if (setting.File == null)
                {
                    if (!setting.File.CheckFileSize(2000))
                    {
                        ModelState.AddModelError("File", "sekil olcusu 2m cox olmamalidir");
                        return View();
                    }
                    if (!setting.File.CheckFileType("image/jpeg"))
                    {
                        ModelState.AddModelError("File", "image jpeg tipinnen fayl secin ! ");
                        return View();
                    }
                    Helper.DeleteFile(_env, setting.Value, "assets", "img", "Logo");
                    settingExists.Value = setting.File.CreateImage(_env, "assets", "img", "Logo");
                }
            }


            if (setting.Key == "FooterLogo")
            {
                if (setting.File == null)
                {
                    if (!setting.File.CheckFileSize(2000))
                    {
                        ModelState.AddModelError("File", "sekil olcusu 2m cox olmamalidir");
                        return View();
                    }
                    if (!setting.File.CheckFileType("image/jpeg"))
                    {
                        ModelState.AddModelError("File", "image jpeg tipinnen fayl secin !  ");
                        return View();
                    }
                    Helper.DeleteFile(_env, setting.Value, "assets", "img", "footer-logo");
                    settingExists.Value = setting.File.CreateImage(_env, "assets", "img", "footer-logo");
                }
            }



            settingExists.Value = setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
