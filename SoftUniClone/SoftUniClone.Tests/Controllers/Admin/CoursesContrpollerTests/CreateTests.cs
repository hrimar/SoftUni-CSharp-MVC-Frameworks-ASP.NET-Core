using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoftUniClone.ServiceModels.Admin.BindingModels;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Tests.Controllers.Admin.CoursesContrpolerTests
{
    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        public void Create_WithValidCourse_ShoudCallService() // for POST 
        {
            //1. Arrange
            var model = new CourseCreationBindingModel();
            bool serviceCalled = false;


            //1. Arrange:
            var mockRepository = new Mock<IAdminCoursesService>();
            mockRepository
                .Setup(repo => repo.AddCourse(model))              
                .Callback(() => serviceCalled = true);

            var mockLocalizer = new Mock<IStringLocalizer<CoursesController>>();

            var controller = new CoursesController(mockRepository.Object, mockLocalizer.Object);

            //2. Act:
            var result =  controller.Create(model);

            //3. Assert with three separate tests in one:
            Assert.IsTrue(serviceCalled);
        }
    }
}
