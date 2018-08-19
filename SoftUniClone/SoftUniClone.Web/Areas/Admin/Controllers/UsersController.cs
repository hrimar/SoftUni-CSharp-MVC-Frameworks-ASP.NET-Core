using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.ViewModels;
//using SoftUniClone.Web.Areas.Admin.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftUniClone.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly SoftUniCloneDbContext contex;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UsersController(SoftUniCloneDbContext conte, IMapper mapper,
            UserManager<User> userManager)
        {
            this.contex = conte;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var users = this.contex.Users
                .Where(u=>u.Id != currentUser.Id) // in order not to redact yourself
                .ToList();

            // TODO: Fill in "Is Lecturer" -> whether user is in role "Lecturer"?
            // TODO: Is admin
            // If lecturer -> hide button "Make lekturer"
            // If administrator -> only details
            var model = this.mapper.Map<IEnumerable<UserShortViewModel>>(users);

            return View(model);
        }


        public async Task<IActionResult> Details(string id)
        {
            // TODO: If administrator -> disable editing

            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (id == currentUser.Id)
            {
                return Unauthorized();
            }


            var user = await this.contex.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);

            var model = this.mapper.Map<UserDetailsViewModel>(user);
            model.Roles = roles; // !!!

            return View(model);
        }
    }
}