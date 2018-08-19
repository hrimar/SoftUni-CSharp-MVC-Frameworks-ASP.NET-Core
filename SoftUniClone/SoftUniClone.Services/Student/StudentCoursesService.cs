using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Student.BindingModels;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Student.ViewModels;
using SoftUniClone.Services.Student.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Student
{
    public class StudentCoursesService : BaseService, IStudentCoursesService
    {
        public StudentCoursesService(SoftUniCloneDbContext dbContex, IMapper mapper)
            : base(dbContex, mapper)
        {
        }

        public IEnumerable<CourseDetailsViewModel> GetCourses(int page, int count)
        {
            // TODo: Validation: page, count > 0 and not to large
            var courses = this.DbContext.Courses
                .Include(c => c.Instances)
                .Skip((page - 1) * count)   // !!!
                .Take(count)                // !!!
                .OrderByDescending(c=>c.Id) // !!!
                .ToList();
            var courseModels = this.Mapper
                .Map<IEnumerable<CourseDetailsViewModel>>(courses);

            return courseModels;
        }



        public CourseDetailsViewModel GetCourse(int courseId)
        {
            var course = this.DbContext.Courses
                .Include(c => c.Instances)
                .SingleOrDefault(c => c.Id == courseId);
            var courseModel = this.Mapper
                .Map<CourseDetailsViewModel>(course);

            return courseModel;
        }


        // FOR DEMO PURPOSES ONLY:
        public async Task<int> CreateCourse(ServiceModels.Admin.BindingModels.CourseEditingBindingModel model)
        {
            var course = this.Mapper.Map<Course>(model);
            await this.DbContext.Courses.AddAsync(course);
            await this.DbContext.SaveChangesAsync();
            return course.Id;
        }

        public async  Task<CourseDetailsViewModel> UpdateCourse(ServiceModels.Admin.BindingModels.CourseEditingBindingModel model)
        {
            var course = this.DbContext.Courses
                .Include(c=>c.Instances)
                .SingleOrDefault(c => c.Id == model.Id);

            if (course == null)
            {
                return null;
            }

            course.Name = model.Name;
            course.Slug = model.Slug;
          await   this.DbContext.SaveChangesAsync();

            var courseModel = this.Mapper.Map<CourseDetailsViewModel>(course);
            return courseModel;
        }


        public async Task<bool> DeleteCourse(int id)
        {
            var course = this.DbContext.Courses
                 .Include(c => c.Instances)
                 .SingleOrDefault(c => c.Id == id);

            this.DbContext.CourseInstances.RemoveRange(course.Instances);

            this.DbContext.Courses.Remove(course);
            try
            {
                await this.DbContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
            return true;                
            }
            finally
            {
                this.DbContext.Dispose();
            }

        }

        
    }
}
