using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Models
{
   public class HomeworkSubmition
    {
        public int LectureId { get; set; }

        public  Lecture Lecture { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public DateTime TimeUploaded { get; set; }

        public string PathFile { get; set; }
    }
}
