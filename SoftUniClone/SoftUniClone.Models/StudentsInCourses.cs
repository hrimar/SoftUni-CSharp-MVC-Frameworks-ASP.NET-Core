using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Models
{
   public class StudentsInCourses // -> to have only one Student in one course
    {
        public string StudentId { get; set; }
        public User Student { get; set; }

        public int CourseId { get; set; }
        public CourseInstance Course { get; set; }
    }
}
