using AutoMapper;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Admin.ViewModels;
using SoftUniClone.ServiceModels.Lecturer.BindingModels;
using SoftUniClone.ServiceModels.Lecturer.ViewModels;
//using SoftUniClone.Web.Areas.Admin.Models.BindingModels;
//using SoftUniClone.Web.Areas.Admin.Models.ViewModels;

namespace SoftUniClone.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // the bellow mappings are option and Mapper can works by default without them:
            this.CreateMap<User, ServiceModels.Admin.ViewModels.UserShortViewModel>();
            this.CreateMap<User, ServiceModels.Lecturer.ViewModels.UserShortViewModel>();
            this.CreateMap<User, ServiceModels.Student.ViewModels.UserShortViewModel>();

            this.CreateMap<User, ServiceModels.Admin.ViewModels.UserDetailsViewModel>();
            this.CreateMap<User, ServiceModels.Lecturer.ViewModels.UserDetailsViewModel>();
            this.CreateMap<User, ServiceModels.Student.ViewModels.UserDetailsViewModel>();

            this.CreateMap<CourseCreationBindingModel, Course>();
            this.CreateMap<Course, ServiceModels.Admin.ViewModels.CourseShortViewModel>();
            this.CreateMap<Course, ServiceModels.Lecturer.ViewModels.CourseShortViewModel>();
            this.CreateMap<Course, ServiceModels.Student.ViewModels.CourseShortViewModel>();

            this.CreateMap<Course, ServiceModels.Admin.ViewModels.CourseDetailsViewModel>();
            this.CreateMap<Course, ServiceModels.Lecturer.ViewModels.CourseDetailsViewModel>();
            this.CreateMap<Course, ServiceModels.Student.ViewModels.CourseDetailsViewModel>();

            this.CreateMap<InstancesCreationBindingModel, CourseInstance>();
            this.CreateMap<LectureCreatingBindingModel, Lecture>();

            // If the mane of the prop-s are different:
            this.CreateMap<User, LecturerShortViewModel>()
                .ForMember(lvm => lvm.Name, option => option.MapFrom(src =>src.UserName));

            this.CreateMap<CourseInstance, InstanceEditingBindingModel>(); //?? 
            this.CreateMap<Lecture, LectureShortViewModel>();

            this.CreateMap<CourseEditingBindingModel, Course>();

            this.CreateMap<User, ServiceModels.Lecturer.ViewModels.StudentsViewModel>();
            this.CreateMap<User, ServiceModels.Student.ViewModels.StudentsViewModel>();

            this.CreateMap<Lecture, ServiceModels.Lecturer.ViewModels.LectureViewModel>();
            this.CreateMap<Lecture, ServiceModels.Student.ViewModels.LectureViewModel>();
        }
    }
}
