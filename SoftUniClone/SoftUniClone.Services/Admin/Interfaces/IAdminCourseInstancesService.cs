using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Admin.Interfaces
{
    public interface IAdminCourseInstancesService
    {
       Task<InstancesCreationBindingModel> PrepareInstanceForCreation(int courseId);

        Task<int> CreateInstance(InstancesCreationBindingModel model);

        Task<CourseInstanceViewModel> CourseDetails(int id);
    }
}
