
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SoftUniClone.Data;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Services.Lecturer.Interfaces;
using SoftUniClone.Web.Helpers.Messages;
using System.Threading.Tasks;

namespace SoftUniClone.Web.Areas.Lecturer.Controllers
{
    public class CoursesController : LecturerController
    {       
        private readonly ILecturerCoursesService coursesService;
        private readonly IStringLocalizer<CoursesController> localizer;

        public CoursesController(ILecturerCoursesService coursesService, 
                                 IStringLocalizer<CoursesController> localizer)
        {            
            this.coursesService = coursesService;
            this.localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var modelCourses = await coursesService.GetCourses();

            return View(modelCourses);
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
            ServiceModels.Lecturer.ViewModels.CourseDetailsViewModel courseModel = await this.coursesService.CourseDetails(id);
            return View(courseModel);
        }
    }
}