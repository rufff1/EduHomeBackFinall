using EduHome.Dal;
using EduHome.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            Contact contact = await _context.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false);

            if (contact == null)
            {
                return NotFound("Yalnis melumat");
            }
            return View(contact);
        }
    }
}
