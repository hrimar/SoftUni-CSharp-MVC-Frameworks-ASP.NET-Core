using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Constants;

namespace SoftUniClone.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.AdminArea)]     
    [Authorize(Roles = WebConstants.AdminRole)]
    // for PAGES services.AddMvc().AddRazorPagesOptions(options =>{optins.ConventionsAuthorizeAreaFolder("", "");})
    public abstract class AdminController : Controller
    {        
    }
}