using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Admin.Interfaces
{
   public interface IAdminCoursesService
    {
        Task<IEnumerable<CourseShortViewModel>> GetCourses();

        Task<Course> AddCourse(CourseCreationBindingModel model);

        Task<CourseDetailsViewModel> CourseDetails(int id);
    }
}    
