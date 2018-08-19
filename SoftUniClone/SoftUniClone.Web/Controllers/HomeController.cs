using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SoftUniClone.Web.Models;

namespace SoftUniClone.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IMemoryCache cache;

        //public HomeController(IMemoryCache cache)
        //{
        //    this.cache = cache;
        //}

        public IActionResult Index()
        {
            //// For INLINE CACHE instead of cache TAG HELPER can use also:
            //var dateTime = this.cache.GetOrCreate("CacheKey", entry => DateTime.Now);

            //// for removing this cache:
            //this.cache.Remove("CacheKey");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        
        //---

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return LocalRedirect(returnUrl);
        }

        //-

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IActionResult Cache()
        {
            // return View(); - here can not cache!

            return Json(new { date = DateTime.Now });
        }
    }
}
