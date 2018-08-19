using SoftUniClone.ServiceModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftUniClone.Models
{
  public  class Course
    {
        public Course()
        {
            this.Instances = new List<CourseInstance>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.NameNullMessage)]
        [StringLength(ValidationConstants.NameMaxLength, MinimumLength = ValidationConstants.NameMinLength)]
        public string Name { get; set; }

        public string Slug { get; set; }

        public ICollection<CourseInstance> Instances { get; set; }
    }
}
