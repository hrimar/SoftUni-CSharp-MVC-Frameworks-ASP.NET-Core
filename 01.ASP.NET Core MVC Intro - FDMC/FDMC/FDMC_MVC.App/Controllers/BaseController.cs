namespace FDMC_MVC.App.Controllers
{
    using FDMC.Data;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        public BaseController(FDMCDbContext context)
        {
            this.Context = context;
        }

        public FDMCDbContext Context { get; private set; }

    }
}
