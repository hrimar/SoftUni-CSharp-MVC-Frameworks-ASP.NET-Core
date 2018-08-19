using Microsoft.AspNetCore.Identity;
using SoftUniClone.ServiceModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftUniClone.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.EnrolledCourses = new List<StudentsInCourses>();
            this.LectureCourses = new List<CourseInstance>();
            this.HomeworkSubmitions = new List<HomeworkSubmition>();
        }

        [Required(ErrorMessage = ValidationConstants.NameNullMessage)]
        [StringLength(ValidationConstants.NameMaxLength, MinimumLength = ValidationConstants.NameMinLength)]
        public string FullName { get; set; }

        public ICollection<StudentsInCourses> EnrolledCourses { get; set; }

        public ICollection<CourseInstance> LectureCourses { get; set; }

        public ICollection<HomeworkSubmition> HomeworkSubmitions { get; set; }
    }
}
