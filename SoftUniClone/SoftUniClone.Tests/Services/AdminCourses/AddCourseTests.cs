using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftUniClone.Data;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.ServiceModels.Validation;
using SoftUniClone.Services.Admin;
using SoftUniClone.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Tests.Services.AdminCourses
{
    [TestClass]
    public class AddCourseTests
    {
        private AdminCoursesService service;
        private SoftUniCloneDbContext dbContext;

        [TestInitialize]
        public void InitilizeTests() // runs befor every test
        {
            this.dbContext = MockDbContext.GetContext();
            var mapper = MockAutomapper.GetMapper();
            this.service = new AdminCoursesService(dbContext, mapper);
        }


        [TestMethod]
        public async Task AddCourse_WithProperCourse_ShouldAddCorrectly() // wahyWeDo_conditions_wahyExpect
        {
            // 1. Arrange
            var courseName = "New course name";
            var slugName = "new-course-name";

            var courseModel = new CourseCreationBindingModel()
            {
                Name =courseName ,
                Slug =slugName
            };

            // 2. Act
            await this.service.AddCourse(courseModel);

            // 3. Asserts 
            Assert.AreEqual(1, this.dbContext.Courses.Count());
            var course = this.dbContext.Courses.First();
            Assert.AreEqual(courseName, course.Name);
            Assert.AreEqual(slugName, course.Slug);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public async Task AddCourse_WithNullCourse_ShouldThrowException() // wahyWeDo_conditions_wahyExpect
        {
            // 1. Arrange           
            CourseCreationBindingModel courseModel = null;

            // 2. Act
            Func<Task> addCourse = () => this.service.AddCourse(courseModel);

            // 3. Asserts 
           // await Assert.ThrowsExceptionAsync<ArgumentException>(addCourse);
           var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(addCourse);
            Assert.AreEqual(ValidationConstants.CourseNullMessage, exception.Message);
        }


        [TestMethod]      
        public async Task AddCourse_WithMissingName_ShouldThrowException() // wahyWeDo_conditions_wahyExpect
        {
            // 1. Arrange           
            CourseCreationBindingModel courseModel = new CourseCreationBindingModel()
            {
                Name = null,
                Slug = "some-slug"
            };

            // 2. Act
            Func<Task> addCourse = () => this.service.AddCourse(courseModel);

            // 3. Asserts 
            // await Assert.ThrowsExceptionAsync<ArgumentException>(addCourse);
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(addCourse);
            Assert.AreEqual(ValidationConstants.CourseNameMessage, exception.Message);
        }
    }
}
