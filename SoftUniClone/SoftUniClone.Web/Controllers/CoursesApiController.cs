using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Constants;
using SoftUniClone.ServiceModels.Student.BindingModels;
using SoftUniClone.ServiceModels.Student.ViewModels;
using SoftUniClone.Services.Student.Interfaces;

namespace SoftUniClone.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = WebConstants.AdminRole)]
    public class CoursesApiController : ControllerBase
    {
        private const int DefaultPage = 1;
        private const int DefaultResultPerPage = 3;

        //private readonly SoftUniCloneDbContext dbContext;
        //private readonly IMapper mapper;

        //public CoursesApiController(SoftUniCloneDbContext dbContext,
        //    IMapper mapper)
        //{
        //    this.dbContext = dbContext;
        //    this.mapper = mapper;
        //}
        //or
        private readonly IStudentCoursesService coursesService;

        public CoursesApiController(IStudentCoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        //[HttpGet("")]
        //[AllowAnonymous]
        //public IActionResult GetAllCourses()
        //{
        //    //var courses = this.dbContext.Courses
        //    //    .Include(c=>c.Instances)
        //    //    .ToList();
        //    //var courseModels = this.mapper.Map<List<CourseDetailsViewModel>>(courses);
        //    //or
        //    var courseModels = this.coursesService.GetCourses();

        //    return Ok(courseModels);
        //}
        //// or
        //[HttpGet("")]
        //public ActionResult<List<CourseDetailsViewModel>> GetAllCourses()
        //{           
        //    var courseModels = this.coursesService.GetCourses().ToList();

        //    return (courseModels);
        //}
        //// or WITH PAGING RESULTS:
        [HttpGet("")]
        public ActionResult<CourseModel> GetAllCourses(int? page, int? count)
        {
            if (!page.HasValue)
            {
                page = DefaultPage;
            }

            if (!count.HasValue)
            {
                count = DefaultResultPerPage;
            }
            var courseModels = this.coursesService.GetCourses(page.Value, count.Value).ToList();

            return new CourseModel()
            {
                CurrentPage = page.Value,
                Count = count.Value,
                Courses = courseModels
            };
        }



        [HttpGet("{id}", Name = "Details")] // Name е име с което с ерегистрира
        public IActionResult GetCourse(int id)
        {
            //var course = this.dbContext.Courses
            //    .Include(c => c.Instances)
            //    .SingleOrDefault(c => c.Id == id);
            //var courseModel = this.mapper.Map<CourseDetailsViewModel>(course);
            //or
            var courseModel = this.coursesService.GetCourse(courseId: id);

            if (courseModel == null)
            {
                return NotFound(new { Message = "The course does not exist." });
            }

            return Ok(courseModel);
        }
        //// or
        //[HttpGet("{id}", Name = "Details")] // Name е име с което с ерегистрира
        //public ActionResult<CourseDetailsViewModel> GetCourse(int id)
        //{            
        //    var courseModel = this.coursesService.GetCourse(courseId: id);

        //    if (courseModel == null)
        //    {
        //        return NotFound(new { Message = "The course does not exist." });
        //    }

        //    return (courseModel);
        //}

        // FOR DEMO PURPOSES ONLY:
        [HttpPost("")]
        public async Task<IActionResult> CreateCourse([FromBody]ServiceModels.Admin.BindingModels.CourseEditingBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            int courseId = await this.coursesService.CreateCourse(model);

            return CreatedAtAction("Details", new { id = courseId });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDetailsViewModel>> EditCourse(
            int id, [FromBody]ServiceModels.Admin.BindingModels.CourseEditingBindingModel model)
        {
            if (!this.ModelState.IsValid) // chech the B.Model attributes!
            {
                return BadRequest(this.ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest(new { Message = "The ID to edit does not match." });
            }

            var course = await this.coursesService.UpdateCourse(model);
            if (course == null)
            {
                return NotFound(new { Message = "The course ID does not exist." });
            }

            return course;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            bool isDeleted = await this.coursesService.DeleteCourse(courseId: id);

            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new { message = $"Could not delete course {id}." });
            }
        }
    }
}