using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Models
{
   public class CourseInstance
    {
        public CourseInstance()
        {
            this.Students = new List<StudentsInCourses>();
            this.Lectures = new List<Lecture>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Slug { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string LecurerId { get; set; }
        public User Lecturer{ get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentsInCourses> Students { get; set; }

        public ICollection<Lecture> Lectures { get; set; }
    }
}
