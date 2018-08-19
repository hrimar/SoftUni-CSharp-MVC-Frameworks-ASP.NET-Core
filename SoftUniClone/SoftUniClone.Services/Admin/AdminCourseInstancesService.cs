using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Admin.ViewModels;
using SoftUniClone.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Admin
{
    public class AdminCourseInstancesService : BaseService, IAdminCourseInstancesService
    {
        public AdminCourseInstancesService(
            SoftUniCloneDbContext dbContext,
            IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<InstancesCreationBindingModel> PrepareInstanceForCreation(int courseId)
        {
            var course = await this.DbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                return null;
            }

            var model = new InstancesCreationBindingModel()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                CourseId = course.Id,
                Name = course.Name
            };

            return model;
        }

        public async Task<int> CreateInstance(InstancesCreationBindingModel model)
        {
            var instance = this.Mapper.Map<CourseInstance>(model);
            await this.DbContext.CourseInstances.AddAsync(instance);
            await this.DbContext.SaveChangesAsync();

            return instance.Id;
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

    }
}
