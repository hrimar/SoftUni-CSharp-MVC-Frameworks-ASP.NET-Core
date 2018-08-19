
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SoftUniClone.Data;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Web.Extensions;
using SoftUniClone.Web.Filters;
//using SoftUniClone.Web.Areas.Admin.Models.BindingModels;
//using SoftUniClone.Web.Areas.Admin.Models.ViewModels;
using SoftUniClone.Web.Helpers.Messages;
using System.Threading.Tasks;

namespace SoftUniClone.Web.Areas.Admin.Controllers
{
    public class CoursesController : AdminController
    {       
        private readonly IAdminCoursesService coursesService;
        private readonly IStringLocalizer<CoursesController> localizer;

        public CoursesController(IAdminCoursesService coursesService, 
                                 IStringLocalizer<CoursesController> localizer)
        {            
            this.coursesService = coursesService;
            this.localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var title = this.localizer["All Courses"];
            var modelCourses = await this.coursesService.GetCourses();

            return View(modelCourses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // IN ORDER TO PREVENT OUR SITE FROM OUTSIDE REQUESTS with AJAX       
        [ValidateModelState] // Filter - при невалиден ModelState показва само текущ. view:
        public async Task<IActionResult> Create(CourseCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //var course = new CourseCreationBindingModel()
            //{
            //    Name = model.Name,
            //    Slug = model.Slug
            //};
            //// or
            ////var course = this.mapper.Map<Course>(model);
            ////this.contex.Courses.Add(course);
            ////contex.SaveChanges();
            // or afret adding services:
            var course = await this.coursesService.AddCourse(model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Course created succesfully."
            });

            return RedirectToAction("Details", new { id = course.Id});  
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //// TODO: Add lecturer

            //var course = this.contex.Courses
            //    .Include(c => c.Instances)
            //    .FirstOrDefault(c => c.Id == id);

            //var model = this.mapper.Map<CourseDetailsViewModel>(course);
            //// or afret adding services:
            var courseModel = await this.coursesService.CourseDetails(id);
            return View(courseModel);
        }
    }
}