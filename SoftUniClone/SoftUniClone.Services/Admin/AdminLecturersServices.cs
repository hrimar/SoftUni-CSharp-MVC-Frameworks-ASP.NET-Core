using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using SoftUniClone.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Admin
{
    public class AdminLecturersServices : BaseService, IAdminLecturersServices
    {
        private readonly UserManager<User> userManager;

        public AdminLecturersServices(SoftUniCloneDbContext dbContex, IMapper mapper,
            UserManager<User> userManager)
            : base(dbContex, mapper)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<LecturerShortViewModel>> GetAllLecturers()
        {
            var users = await this.userManager.GetUsersInRoleAsync("Lecturer"); // !!!!!!!!
            var lecturers = this.Mapper.Map<IEnumerable<LecturerShortViewModel>>(users);

            return lecturers;
        }
    }
}
