using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Constants;

namespace SoftUniClone.Web.Areas.Lecturer.Controllers
{

    [Area(WebConstants.LecturerArea)]
    [Authorize(Roles = WebConstants.LecturerOrAdminRole)]
    // for PAGES services.AddMvc().AddRazorPagesOptions(options =>{optins.ConventionsAuthorizeAreaFolder("", "");})
    public abstract class LecturerController : Controller
    {        
    }
}