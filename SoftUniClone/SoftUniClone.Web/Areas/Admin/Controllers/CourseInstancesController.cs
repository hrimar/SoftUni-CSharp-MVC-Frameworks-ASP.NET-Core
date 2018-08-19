using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Web.Extensions;
using SoftUniClone.Web.Helpers.Messages;
//using SoftUniClone.Web.Areas.Admin.Models.BindingModels;

namespace SoftUniClone.Web.Areas.Admin.Controllers
{
    public class CourseInstancesController : AdminController
    {       
        private readonly IAdminCourseInstancesService courseInstancesService;
        private readonly IAdminLecturersServices lecturersService;


        public CourseInstancesController(IAdminCourseInstancesService courseInstancesService, 
                                         IAdminLecturersServices lecturersServices)
        {
            this.courseInstancesService = courseInstancesService;
            this.lecturersService = lecturersServices;
        }


        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var model = await this.courseInstancesService.PrepareInstanceForCreation(courseId: id);
            if (model == null)
            {
                return NotFound();
            }

            var lecturers = await this.lecturersService.GetAllLecturers();

            this.ViewData["Lecturers"] = lecturers;

            //var course = this.contex.Courses.Find(id);
            //if (course == null)
            //{
            //    return NotFound();
            //}

            //this.ViewData["id"] = id;
            //// or if dont use ViewData can take the courseId from the BindingModel:
            //var model = new InstancesCreationBindingModel()
            //{
            //    CourseId = course.Id,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now.AddMonths(3)
            //};

            //this.ViewData["course"] = course.Name;
                       
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstancesCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //var instance = this.mapper.Map<CourseInstance>(model);
            //this.contex.CourseInstances.Add(instance);
            //this.contex.SaveChanges();
            //// TODO
            //// return RedirectToAction("Details", new { id = instance.Id ); 
            //return View();

            if (!ModelState.IsValid)
            {
                return View();
            }

            int instanceId = await this.courseInstancesService.CreateInstance(model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Course instance created succesfully."
            });

            return RedirectToAction("Details", new { id = instanceId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var courseInstanceModel = await this.courseInstancesService.CourseDetails(id);
            return View(courseInstanceModel);
        }
    }
}