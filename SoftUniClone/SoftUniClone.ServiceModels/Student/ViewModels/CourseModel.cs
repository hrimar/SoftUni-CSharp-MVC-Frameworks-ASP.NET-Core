using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.ServiceModels.Student.ViewModels
{
  public  class CourseModel // for PAGING
    {
        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }
    }
}
