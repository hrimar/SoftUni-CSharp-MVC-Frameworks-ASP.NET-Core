using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using SoftUniClone.Services.Lecturer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Lecturer
{
   public class LecturerCoursesService : BaseService, ILecturerCoursesService
    {
        public LecturerCoursesService(SoftUniCloneDbContext dbContex, IMapper mapper)
            : base(dbContex, mapper)
        { }


        public async Task<IEnumerable<CourseShortViewModel>> GetCourses()
        {
            var courses = await this.DbContext.Courses.ToListAsync();
            var model = this.Mapper.Map<IEnumerable<CourseShortViewModel>>(courses);
            return model;
        }

        
        public async Task<CourseDetailsViewModel> CourseDetails(int id)
        {
            // TODO: Add lecturer

            var course = await this.DbContext.Courses
                .Include(c => c.Instances)
                .FirstOrDefaultAsync(c => c.Id == id);

            ServiceModels.Lecturer.ViewModels.CourseDetailsViewModel model = this.Mapper.Map<CourseDetailsViewModel>(course);
            return model;
        }

       
    }
}
