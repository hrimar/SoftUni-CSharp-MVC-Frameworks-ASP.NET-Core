using FDMC.Data;
using FDMC.Models;
using FDMC_MVC.App.Models.BindingModels;
using FDMC_MVC.App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FDMC_MVC.App.Controllers
{
    public class CatsController : BaseController // : Controller
    {
        public CatsController(FDMCDbContext context) : base(context)
        {
        }

        //public CatsController(FDMCDbContext context)
        //{
        //    this.Context = context;
        //}

        //public FDMCDbContext Context { get; private set; }


        [HttpGet]
        public IActionResult Details(int id) // ?? to Rout cats/id, not /cats/Details/id ???
        {
            var cat = this.Context.Cats.Find(id);
                      
            if (cat == null)
            {
                return NotFound();
            }

            var catModel = new CatDetailsViewModel() // When we return not Cat from DB, but ViewModel!
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };

            return View(model: catModel); // or return View(catModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Add(CreatingCatBindingModel model) // or public IActionResult Add(Cat model) with @model Cat !!!
        {
            var cat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };
            // when we take Cat from View, instead of AddCatBindingModel the above is not necesary!!!

            this.Context.Cats.Add(cat);
            this.Context.SaveChanges();

            //return View("/home/index");  - but not so good
            return RedirectToRoute("default", new { controller = "Cats", action = "Details", id = cat.Id }); // gives Id param to Details(int id)
                        // OR
            //return RedirectToAction("Details", new { id = cat.Id });
        }
    }
}
