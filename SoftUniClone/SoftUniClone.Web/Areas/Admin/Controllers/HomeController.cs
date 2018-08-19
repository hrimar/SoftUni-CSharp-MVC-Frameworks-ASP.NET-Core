using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.Models;

namespace SoftUniClone.Web.Areas.Admin.Controllers
{
  
    public class HomeController : AdminController
    {
        //private UserManager<User> userManager;

        //public HomeController(UserManager<User> userManager)
        //{
        //    this.userManager = userManager;
        //}


        public IActionResult Index()
        {
            return View();
        }
    }
}