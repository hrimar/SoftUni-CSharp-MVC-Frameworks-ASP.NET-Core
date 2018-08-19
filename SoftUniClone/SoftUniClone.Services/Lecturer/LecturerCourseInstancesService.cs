using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Lecturer.BindingModels;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using SoftUniClone.Services.Exceptions;
using SoftUniClone.Services.Lecturer.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Lecturer
{
    public class LecturerCourseInstancesService : BaseService, ILecturerCourseInstancesService
    {
        private readonly UserManager<User> userManager;

        public LecturerCourseInstancesService(SoftUniCloneDbContext dbContex,
            IMapper mapper, UserManager<User> userManager)
            : base(dbContex, mapper)
        {
            this.userManager = userManager;
        }


        public async Task<InstanceEditingBindingModel> PrepareInstanceForEditing(int instanceId)
        {
            var instance = await this.DbContext.CourseInstances
               .Include(l => l.Lectures)
               .FirstOrDefaultAsync(i => i.Id == instanceId);

            if (instance == null)
            {
                //return null; or
                throw new NotFoundException();
            }

            var instanceModel = this.Mapper.Map<InstanceEditingBindingModel>(instance);
            return instanceModel;
        }

        public async Task EditInstance(int instanceId, InstanceEditingBindingModel model, ClaimsPrincipal user)
        {
            var instance = await this.DbContext.CourseInstances.FindAsync(instanceId);

            if (instance == null)
            {
                throw new NotFoundException();
            }

            var userFromDb = await this.userManager.GetUserAsync(user);
            bool canEditInstance = await this.userManager.IsInRoleAsync(userFromDb, "Administrator") ||
                instance.LecurerId == userFromDb.Id; // !!!
            if (canEditInstance)
            {
                instance.Description = model.Description; // Here is better not to use Mapper!
            }

            //if (instance.LecurerId != this.userManager.GetUserId(user)
            //    && !user.IsInRole("Administrator"))        // !!!

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<bool> InstamnceExists(int instanceId)
        {
            var instance = await this.DbContext.CourseInstances.FindAsync(instanceId);

            return instance != null;
        }

        public async Task<CourseInstanceViewModel> CourseDetails(int id)
        {
            // TODO: Add lecturer

            var courseInstance = await this.DbContext.CourseInstances
                .Include(c => c.Lectures)
                .Include(c=>c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            var lectures = this.Mapper.Map<ICollection<LectureViewModel>>(courseInstance.Lectures);
            var students = this.Mapper.Map< ICollection<StudentsViewModel>>(courseInstance.Students);
            //var model = this.Mapper.Map<CourseDetailsViewModel>(course);
            var model = new CourseInstanceViewModel()
            {
                Id = id,
                Name = courseInstance.Name,
                Slug = courseInstance.Slug,
                Description = courseInstance.Description,
                StartDate = courseInstance.StartDate,
                EndDate = courseInstance.EndDate,
                Lectures = lectures,
                Students = students
            };
            return model;
        }

        public async  Task AddLecture(int id, LectureCreatingBindingModel model)
        {
            var courseInstance = await this.DbContext.CourseInstances.FindAsync(id);
            var lecture = this.Mapper.Map<Lecture>(model);
            courseInstance.Lectures.Add(lecture);
                     
            await this.DbContext.SaveChangesAsync();
        }
    }
}
