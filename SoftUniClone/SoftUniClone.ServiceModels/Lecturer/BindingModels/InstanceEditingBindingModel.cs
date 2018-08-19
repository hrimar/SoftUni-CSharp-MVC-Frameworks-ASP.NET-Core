using SoftUniClone.ServiceModels.Lecturer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftUniClone.ServiceModels.Lecturer.BindingModels
{
   public class InstanceEditingBindingModel
    {
       

        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<LectureShortViewModel> Lecturers { get; set; }

    }
}
