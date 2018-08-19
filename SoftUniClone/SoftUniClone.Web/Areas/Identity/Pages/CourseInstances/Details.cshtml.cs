using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using SoftUniClone.Services.Lecturer.Interfaces;
using SoftUniClone.Services.Student.Interfaces;

namespace SoftUniClone.Web.Areas.Identity.Pages.CourseInstances
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        //private readonly IStudentCourseInstancesService studentService;

        //public DetailsModel(IStudentCourseInstancesService coursesService)
        //{
        //    this.studentService = coursesService;
        //}

        private readonly ILecturerCourseInstancesService courseInstancesService;
        public DetailsModel(SoftUniCloneDbContext dbContex, IMapper mapper, ILecturerCourseInstancesService courseInstancesService)
        {
            this.DbContext = dbContex;
            this.Mapper = mapper;
            this.courseInstancesService = courseInstancesService;
        }

        public SoftUniCloneDbContext DbContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<StudentsViewModel> Students { get; set; }

        public ICollection<LectureViewModel> Lectures { get; set; }


        public async Task<IActionResult> OnGet(int id)
        {
            //// var courseInstanceModel = await this.studentService.CourseDetails(id); //- tuk ne stava taka
            //var courseInstance = await this.DbContext.CourseInstances
            //   .Include(c => c.Lectures)
            //   .Include(c => c.Students)
            //   .FirstOrDefaultAsync(c => c.Id == id);

            ////var model = this.Mapper.Map<CourseDetailsViewModel>(course); //- tuk ne stava taka
            //var lectures = this.Mapper.Map<ICollection<LectureViewModel>>(courseInstance.Lectures);
            //var students = this.Mapper.Map<ICollection<StudentsViewModel>>(courseInstance.Students);

            ////ICollection<StudentsViewModel> students = null;
            ////foreach (var student in courseInstance.Students)
            ////{
            ////    students.Add(new StudentsViewModel
            ////    {
            ////        Id = student.StudentId,
            ////        FullName = DbContext.Users.Find(student.StudentId).FullName
            ////    });
            ////}

            //this.Id = id;
            //this.Name = courseInstance.Name;
            //this.Slug = courseInstance.Slug;
            //this.Description = courseInstance.Description;
            //this.StartDate = courseInstance.StartDate;
            //this.EndDate = courseInstance.EndDate;
            //this.Students = students;
            //this.Lectures = lectures;

            CourseInstanceViewModel courseInstanceModel = await this.courseInstancesService.CourseDetails(id);
            this.Id = courseInstanceModel.Id;
            this.Name = courseInstanceModel.Name;
            this.Slug = courseInstanceModel.Slug;
            this.Description = courseInstanceModel.Description;
            this.StartDate = courseInstanceModel.StartDate;
            this.EndDate = courseInstanceModel.EndDate;
            this.Students = courseInstanceModel.Students;
            this.Lectures = courseInstanceModel.Lectures;

            return Page();
        }
    }
}