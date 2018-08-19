﻿
using System;

namespace SoftUniClone.ServiceModels.Student.ViewModels
{
    public class CourseInstanceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Lecturer { get; set; }
    }
}