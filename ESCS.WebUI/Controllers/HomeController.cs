using ESCS.WebUI.Helper;
using ESCS.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ESCS.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                Sinavlar = JsonHelper.GetAllSinav()
            };
            return View(model);
        }

        [Authorize(Roles = "Ogretmen")]
        public IActionResult SinavHazirla()
        {
            JsonHelper.CreateSinavId();
            return View();
            //JsonHelper.WriteJsonToFile(new SinavModel());
        }

        [HttpPost]
        [Authorize(Roles = "Ogretmen")]
        public IActionResult SinavHazirla(SinavModel sinav)
        {
            if (!ModelState.IsValid)
                return View(sinav);
            sinav.Id = JsonHelper.CreateSinavId();
            JsonHelper.WriteJsonToFile(sinav);
            return RedirectToAction("Index");
        }
       
        public IActionResult SinavGoruntule(int id)
        {
            var sinav = JsonHelper.GetAllSinav().SingleOrDefault(s=> s.Id == id); 
            return View(sinav);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
