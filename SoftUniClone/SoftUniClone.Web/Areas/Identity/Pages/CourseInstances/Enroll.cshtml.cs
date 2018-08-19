using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.Services.Student.Interfaces;

namespace SoftUniClone.Web.Areas.Identity.Pages.CourseInstances
{
    public class EnrollModel : PageModel
    {
        //private readonly IStudentCourseInstancesService studentService;

        //public EnrollModel(IStudentCourseInstancesService studentService)
        //{
        //    this.studentService = studentService;
        //}

        private readonly SoftUniCloneDbContext contex;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public EnrollModel(SoftUniCloneDbContext conte, IMapper mapper,
            UserManager<User> userManager)
        {
            this.contex = conte;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public CourseInstance CourseInstance { get; set; }


        public IActionResult OnGet(int id)
        {
            var currentUser = this.userManager.GetUserAsync(this.User).Result;
            this.CourseInstance = this.contex.CourseInstances.FirstOrDefault(ci => ci.Id == id);

            //  bool hasSuchUser = this.CourseInstance.Students.Any(ci => ci.StudentId == currentUser.Id);
            if(this.CourseInstance == null)
            {
                return RedirectToPage("NoInsyances");
            }
            bool hasSuchUser = this.contex.StudentsInCourses.Any(c => c.CourseId == CourseInstance.Id && c.StudentId == currentUser.Id);
            if (hasSuchUser)
            {
                return RedirectToPage("AlreadyEnrolled");
            }

            var studentInCourse = new StudentsInCourses()
            {
                StudentId = currentUser.Id,
                CourseId = this.CourseInstance.Id
            };
            this.contex.StudentsInCourses.Add(studentInCourse);
            this.CourseInstance.Students.Add(studentInCourse);
            this.contex.SaveChanges();

            return Page();
        }
    }
}