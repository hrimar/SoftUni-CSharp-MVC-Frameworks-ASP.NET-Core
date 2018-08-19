using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SoftUniClone.ServiceModels.Admin.BindingModels
{
    public class InstancesCreationBindingModel
    {
       [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        // [Range(typeof(DateTime), "2018-01-01", "2020-01-01")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Lecurer")]
        public string LecurerId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
