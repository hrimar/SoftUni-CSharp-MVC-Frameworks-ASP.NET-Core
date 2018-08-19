using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.ServiceModels.Validation
{
   public class ValidationConstants
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 40;
        public const string NameNullMessage = "The name has to be between 3 and 40.";

        public const string CourseNullMessage = "The course to add must have a value.";
        public const string CourseNameMessage = "The course name must have a value.";
        public const string CourseSlugMessage = "The course slug must have a value.";
    }
}
