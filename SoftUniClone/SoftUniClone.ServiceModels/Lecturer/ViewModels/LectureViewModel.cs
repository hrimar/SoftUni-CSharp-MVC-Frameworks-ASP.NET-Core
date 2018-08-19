using System;

namespace SoftUniClone.ServiceModels.Lecturer.ViewModels
{
    public class LectureViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

       public string LectureHall { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

       
        //public ICollection<Resource> Resources { get; set; }


        //public ICollection<HomeworkSubmition> HomeworkSubmitions { get; set; }
    }
}