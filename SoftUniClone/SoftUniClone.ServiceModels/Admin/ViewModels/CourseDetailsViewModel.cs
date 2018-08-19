using System;
using System.Collections.Generic;

namespace SoftUniClone.ServiceModels.Admin.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CourseInstanceViewModel> Instances { get; set; }
    }
}
