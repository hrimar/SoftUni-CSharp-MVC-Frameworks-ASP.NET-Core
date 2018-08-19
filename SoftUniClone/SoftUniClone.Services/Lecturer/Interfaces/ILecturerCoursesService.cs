using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Lecturer.Interfaces
{
   public interface ILecturerCoursesService
    {
        Task<IEnumerable<CourseShortViewModel>> GetCourses();

        Task<CourseDetailsViewModel> CourseDetails(int id);

    }
}
