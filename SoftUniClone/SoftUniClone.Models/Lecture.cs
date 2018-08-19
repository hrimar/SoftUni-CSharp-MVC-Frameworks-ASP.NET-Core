using SoftUniClone.ServiceModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftUniClone.Models
{
    public class Lecture
    {
        public Lecture()
        {
            this.Resources = new List<Resource>();
            this.HomeworkSubmitions = new List<HomeworkSubmition>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.NameNullMessage)]
        [StringLength(ValidationConstants.NameMaxLength, MinimumLength = ValidationConstants.NameMinLength)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        [Required(ErrorMessage = ValidationConstants.NameNullMessage)]
        [StringLength(ValidationConstants.NameMaxLength, MinimumLength = ValidationConstants.NameMinLength)]
        public string LectureHall { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int CourseId { get; set; }
        public CourseInstance Course { get; set; }

        public ICollection<Resource> Resources { get; set; }

       
        public ICollection<HomeworkSubmition> HomeworkSubmitions { get; set; }
    }
}