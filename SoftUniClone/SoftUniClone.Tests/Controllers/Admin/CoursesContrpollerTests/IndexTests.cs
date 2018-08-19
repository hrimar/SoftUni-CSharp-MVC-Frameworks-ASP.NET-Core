using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoftUniClone.ServiceModels.Admin.ViewModels;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Tests.Controllers.Admin.CoursesContrpolerTests
{
    [TestClass]
    public class IndexTests
    {
        [TestMethod]
        public async Task Index_ReturnsAllCourses()
        {
            var courseModel =  new CourseShortViewModel() { Id = 1, Name = "First" };
            bool methodCalled = false;

            //1. Arrange:
            var mockRepository = new Mock<IAdminCoursesService>();
            mockRepository
                .Setup(service => service.GetCourses())
                .ReturnsAsync(new[] { courseModel })
                .Callback(() => methodCalled = true);

            var mockLocalizer = new Mock<IStringLocalizer<CoursesController>>();

            var controller = new CoursesController(mockRepository.Object, mockLocalizer.Object);

            //2. Act:
            var result = await controller.Index();

            //3. Assert with three separate tests in one:
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultView = result as ViewResult;
            Assert.IsNotNull(resultView.Model);
            Assert.IsInstanceOfType(resultView.Model, typeof(IEnumerable<CourseShortViewModel>));
            Assert.IsTrue(methodCalled);
        }

    }
}
