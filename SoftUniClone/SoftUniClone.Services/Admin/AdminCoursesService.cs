using System;
using SoftUniClone.ServiceModels.Admin.ViewModels;
using SoftUniClone.Services.Admin.Interfaces;
using System.Collections.Generic;
using SoftUniClone.Data;
using System.Linq;
using AutoMapper;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SoftUniClone.ServiceModels.Validation;

namespace SoftUniClone.Services.Admin
{
    public class AdminCoursesService : BaseService, IAdminCoursesService
    {
        public AdminCoursesService(SoftUniCloneDbContext dbContex, IMapper mapper) 
            : base(dbContex, mapper)
        {  }


        public async Task<IEnumerable<CourseShortViewModel>> GetCourses()
        {
            var courses = await this.DbContext.Courses.ToListAsync();
            var model = this.Mapper.Map<IEnumerable<CourseShortViewModel>>(courses);
            return model;
        }

        public async Task<Course> AddCourse(CourseCreationBindingModel model)
        {

            Validator.EnsureNotNull(model, ValidationConstants.CourseNullMessage);
            Validator.EnsureStringNotNullOrEmpty(model.Name, ValidationConstants.CourseNameMessage);
            Validator.EnsureStringNotNullOrEmpty(model.Slug, ValidationConstants.CourseSlugMessage);

            var course =  this.Mapper.Map<Course>(model);
            await this.DbContext.Courses.AddAsync(course);
            await this.DbContext.SaveChangesAsync();

           return course;
        }

        public async Task<CourseDetailsViewModel> CourseDetails(int id)
        {
            // TODO: Add lecturer

            var course = await this.DbContext.Courses
                .Include(c => c.Instances)
                .FirstOrDefaultAsync(c => c.Id == id);

            CourseDetailsViewModel model = this.Mapper.Map<CourseDetailsViewModel>(course);
            return model;
        }
    }
}
