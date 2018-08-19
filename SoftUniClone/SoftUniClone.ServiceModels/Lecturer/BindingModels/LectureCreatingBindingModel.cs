using SoftUniClone.ServiceModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftUniClone.ServiceModels.Lecturer.BindingModels
{
   public class LectureCreatingBindingModel
    {
        public LectureCreatingBindingModel()
        {
            this.StartTime = DateTime.UtcNow;
            this.EndTime = this.StartTime.AddHours(4);
        }

        // TODO: Add validation!
        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(ValidationConstants.NameMaxLength, MinimumLength = ValidationConstants.NameMinLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string LectureHall { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; } 
    }
}
