﻿using GeziBlogu.Data;
using GeziBlogu.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GeziBlogu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.IsAdmin && u.IsActive && u.Username == model.Username && u.password == model.password);
                    if (user is null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız! Kullanıcı Adı veya Şifrenizi Kontrol Ediniz");
                    }
                    else
                    {
                        var userClaim = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, model.Username),
                        };
                        var userIdentity=new ClaimsIdentity(userClaim,"Login");
                        ClaimsPrincipal claimsPrincipal= new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        return RedirectToAction("Index", "Default");
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
