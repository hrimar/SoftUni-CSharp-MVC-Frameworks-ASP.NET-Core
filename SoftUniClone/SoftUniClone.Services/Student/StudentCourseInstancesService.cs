using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Student.ViewModels;
using SoftUniClone.Services.Student.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;

namespace SoftUniClone.Services.Student
{
    public class StudentCourseInstancesService : BaseService, IStudentCourseInstancesService
    {
        private readonly UserManager<User> userManager;

        public StudentCourseInstancesService(SoftUniCloneDbContext dbContext,
            IMapper mapper, UserManager<User> usermanager)
            : base(dbContext, mapper)
        {
            this.userManager = usermanager;
        }

        public async Task<IEnumerable<CourseShortViewModel>> GetCourses()
        {
            var courses = await this.DbContext.Courses.ToListAsync();
            var model = this.Mapper.Map<IEnumerable<CourseShortViewModel>>(courses);
            return model;
        }

        public async Task<CourseInstanceViewModel> CourseDetails(int id)
        {
            // TODO: Add lecturer

            var courseInstance = await this.DbContext.CourseInstances
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);

            //var model = this.Mapper.Map<CourseDetailsViewModel>(course);
            var model = new CourseInstanceViewModel()
            {
                Id = id,
                Name = courseInstance.Name,
                Slug = courseInstance.Slug,
                Description = courseInstance.Description,
                StartDate = courseInstance.StartDate,
                EndDate = courseInstance.EndDate
            };
            return model;
        }

       
        public CourseInstanceViewModel CourseInstanceDetails(int id)
        {
            var courseName = this.DbContext.Courses.Find(id).Name;
            var instancesForThisCourse = this.DbContext.Courses
                        .Include(ci => ci.Instances)                 
                .FirstOrDefault(ci => ci.Name == courseName);       
              

            var coursesInstances = instancesForThisCourse.Instances
                .OrderByDescending(c => c.StartDate).ToList();

            CourseInstance lastInstance = null;
            if (coursesInstances.Count == 0)
            {
                var emtpyModel = new CourseInstanceViewModel()
                {                    
                    Name = courseName                    
                };
                return emtpyModel;
            }

            lastInstance = coursesInstances.First();
            var model = new CourseInstanceViewModel()
            {
                Id = id,
                Name = lastInstance.Name,
                Slug = lastInstance.Slug,
                Description = lastInstance.Description,
                StartDate = lastInstance.StartDate,
                EndDate = lastInstance.EndDate,
               // Lecturer = lastInstance.Lecturer.FullName
            };
            return model;
        }

        //public async Task<IActionResult> Enroll(int id)
        //{
        //  //  var currentUser = await this.userManager.GetUserAsync(this.User);
        //}
    }
}
