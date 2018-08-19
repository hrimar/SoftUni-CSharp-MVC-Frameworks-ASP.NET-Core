using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Student.ViewModels;
using SoftUniClone.ServiceModels.Student.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace SoftUniClone.Services.Student.Interfaces
{
    public interface IStudentCoursesService
    {
        IEnumerable<CourseDetailsViewModel> GetCourses(int page, int count);

        CourseDetailsViewModel GetCourse(int courseId);

        Task<int> CreateCourse(ServiceModels.Admin.BindingModels.CourseEditingBindingModel model);

        Task<CourseDetailsViewModel> UpdateCourse(ServiceModels.Admin.BindingModels.CourseEditingBindingModel model);

        Task<bool> DeleteCourse(int courseId);
    }
}
