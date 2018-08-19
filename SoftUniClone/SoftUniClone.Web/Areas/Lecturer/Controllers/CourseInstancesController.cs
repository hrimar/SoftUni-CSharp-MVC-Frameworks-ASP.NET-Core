using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Lecturer.BindingModels;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using SoftUniClone.Services.Lecturer.Interfaces;

namespace SoftUniClone.Web.Areas.Lecturer.Controllers
{
    public class CourseInstancesController : LecturerController
    {
        private readonly ILecturerCourseInstancesService courseInstancesService;

        public CourseInstancesController(ILecturerCourseInstancesService courseInstancesService)
        {
            this.courseInstancesService = courseInstancesService;
        }

        

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.courseInstancesService.PrepareInstanceForEditing(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, InstanceEditingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: 1. Exception filter -> NotFound, Unauthorazed
            //       2. ModelState Validation filter
            await this.courseInstancesService.EditInstance(id, model, this.User);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            CourseInstanceViewModel courseInstanceModel = await this.courseInstancesService.CourseDetails(id);
            return View(courseInstanceModel);
        }
    }
}