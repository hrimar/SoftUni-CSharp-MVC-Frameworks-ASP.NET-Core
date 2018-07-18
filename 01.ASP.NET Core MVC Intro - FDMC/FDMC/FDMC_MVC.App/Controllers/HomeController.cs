using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FDMC_MVC.App.Models;
using FDMC.Data;
using FDMC_MVC.App.Models.ViewModels;

namespace FDMC_MVC.App.Controllers
{
    public class HomeController : BaseController //: Controller
    {
        public HomeController(FDMCDbContext context) : base(context)
        {
        }

        //public HomeController(FDMCDbContext context)
        //{
        //    this.Context = context;
        //}

        //public FDMCDbContext Context { get; private set; }


        public IActionResult Index()
        {
            //var cats = this.Context.Cats.ToList();// when use CatConciseViewModel has to .Select(cat => new CatConciseViewModel {   }

            var cats = this.Context.Cats
                .Select(c => new CatConciseViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();

            return View(model: cats); // or    return View(cats);
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}


    }
}
