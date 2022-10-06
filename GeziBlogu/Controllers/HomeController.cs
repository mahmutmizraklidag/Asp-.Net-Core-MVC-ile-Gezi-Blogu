using GeziBlogu.Data;
using GeziBlogu.Entities;
using GeziBlogu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GeziBlogu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;
        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel();
            model.Posts = await _context.Posts.Where(p =>  p.IsHome).ToListAsync();
            model.Sliders=await _context.Sliders.ToListAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("iletisim")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [Route("iletisim"),HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    TempData["Mesaj"] = "<div class='alert alert-success'>Mesajınız iletildi.Teşekkürler...</div>";
                    return RedirectToAction(nameof(ContactUs));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                    
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}