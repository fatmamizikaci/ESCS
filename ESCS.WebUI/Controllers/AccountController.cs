using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ESCS.WebUI.Models;

namespace ESCS.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool ValidateLogin(string userName, string password, ref Kullanici kullanici)
        {
            string kimlikDosyaYolu = "Data/Kimlik/kullanici.txt";
            string json = System.IO.File.ReadAllText(kimlikDosyaYolu);
            List<Kullanici> kullanicilar = JsonConvert.DeserializeObject<List<Kullanici>>(json);
            kullanici = kullanicilar.FirstOrDefault(s => s.KullaniciAdi == userName && s.Sifre == password);
            return kullanici != null;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Normally Identity handles sign in, but you can do it directly
            Kullanici kullanici = new Kullanici();
            if (ValidateLogin(userName, password, ref kullanici))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", userName),
                    new Claim("role", kullanici.Rol)
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return Redirect("/");
            }

            return View();
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
