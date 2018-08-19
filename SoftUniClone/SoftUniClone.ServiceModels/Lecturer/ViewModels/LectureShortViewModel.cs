using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.ServiceModels.Lecturer.ViewModels
{
   public class LectureShortViewModel
    {
        public int Id { get; set; } // LectionId

        public string Name { get; set; }

        public string Description { get; set; }

        public string Order { get; set; }
    }
}
