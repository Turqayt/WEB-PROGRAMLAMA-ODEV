using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IndirimKuponum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndirimKuponum.Controllers
{
    public class HomeController : Controller
    {
        private IndirimlerContext context = new IndirimlerContext();

        private readonly ILogger<HomeController> _logger;

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var Indirim = context.Indirim
               .Where(i => i.Onay == true && i.Anasayfa == true)
               .Select(i => new IndirimlerModel()
               {
                   Id = i.Id,
                   Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                   Aciklama = i.Aciklama,
                   EklenmeTarihi = i.EklenmeTarihi,
                   Anasayfa = i.Anasayfa,
                   Onay = i.Onay,
                   Resim = i.Resim
               }
           );
            return View(Indirim.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
