using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Lecturer.BindingModels;
using SoftUniClone.Services.Lecturer.Interfaces;

namespace SoftUniClone.Web.Areas.Lecturer.Controllers
{
    public class LecturesController : LecturerController
    {
        private readonly ILecturerCourseInstancesService courseInstancesService;

        public LecturesController(ILecturerCourseInstancesService courseInstancesService)
        {
            this.courseInstancesService = courseInstancesService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            if (!await this.courseInstancesService.InstamnceExists(instanceId: id))
            {
                return NotFound();
            }

            var model = new LectureCreatingBindingModel() { CourseId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, LectureCreatingBindingModel model)
        {            
          await   this.courseInstancesService.AddLecture(id,  model);

            return Redirect($"/Lecturer/CourseInstances/Details/{id}");
           
        }
    }
}