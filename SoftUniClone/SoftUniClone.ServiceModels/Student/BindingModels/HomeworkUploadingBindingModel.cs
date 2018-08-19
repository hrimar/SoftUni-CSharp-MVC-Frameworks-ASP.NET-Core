using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.ServiceModels.Student.BindingModels
{
    public class HomeworkUploadingBindingModel
    {
        public int LecturerId { get; set; }

        public string AuthorId { get; set; }

        public IFormFile HomeworkFile { get; set; }
    }
}
