using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Admin.Interfaces
{
   public interface IAdminLecturersServices
    {
        Task<IEnumerable<LecturerShortViewModel>> GetAllLecturers();
    }
}
