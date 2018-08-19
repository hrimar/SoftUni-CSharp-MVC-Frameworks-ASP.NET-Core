using Microsoft.AspNetCore.Mvc;
using SoftUniClone.ServiceModels.Lecturer.BindingModels;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Lecturer.Interfaces
{
    public interface ILecturerCourseInstancesService
    {
        Task<InstanceEditingBindingModel> PrepareInstanceForEditing(int instanceId);

        Task EditInstance(int id, InstanceEditingBindingModel model, ClaimsPrincipal user);

        Task<bool> InstamnceExists(int instanceId);

        Task<CourseInstanceViewModel> CourseDetails(int id);

        Task AddLecture(int id, LectureCreatingBindingModel model);
    }
}
