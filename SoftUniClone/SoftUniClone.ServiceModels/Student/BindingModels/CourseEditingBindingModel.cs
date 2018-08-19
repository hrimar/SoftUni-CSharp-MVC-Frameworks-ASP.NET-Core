using SoftUniClone.ServiceModels.Validation;
using System.ComponentModel.DataAnnotations;

namespace SoftUniClone.ServiceModels.Student.BindingModels 
{
    public class CourseEditingBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.CourseNameMessage)]
        [StringLength(ValidationConstants.NameMaxLength, MinimumLength = ValidationConstants.NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationConstants.CourseSlugMessage)]
        //[RegularExpression("[\\W+\\-]")]
        public string Slug { get; set; }

    }
}
