using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Services.Student.Interfaces;

namespace SoftUniClone.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IStudentCourseInstancesService coursesService;
   
        public CoursesController(IStudentCourseInstancesService coursesService    )
        {
            this.coursesService = coursesService;          
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {            
            var modelCourses = await this.coursesService.GetCourses();

            return View(modelCourses);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            //// TODO: Add lecturer

            //var course = this.contex.Courses
            //    .Include(c => c.Instances)
            //    .FirstOrDefault(c => c.Id == id);

            //var model = this.mapper.Map<CourseDetailsViewModel>(course);
            //// or afret adding services:

            //var courseModel = await this.coursesService.CourseDetails(id);
            var courseInstanceModel = this.coursesService.CourseInstanceDetails(id);
            return View(courseInstanceModel);
        }
    }
}