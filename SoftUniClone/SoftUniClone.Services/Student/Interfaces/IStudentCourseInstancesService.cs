using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Student.Interfaces
{
    public interface IStudentCourseInstancesService
    {
        Task<CourseInstanceViewModel> CourseDetails(int id);

       // Task<IActionResult> Enroll(int courseInstanceId);

        Task<IEnumerable<CourseShortViewModel>> GetCourses();
        CourseInstanceViewModel CourseInstanceDetails(int id);

        
    }
}
