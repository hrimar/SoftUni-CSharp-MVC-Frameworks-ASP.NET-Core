using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoftUniClone.Web.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Tests.Filters.ValidateModelStateTests
{
    [TestClass]
    public class OnActionExecutingTests
    {
        [TestMethod]
        public void OnActionExecuting_WithValidModelStrate_ShouldReturnNothing()
        {
            var mockController = new Mock<Controller>();

            var mockContext = new Mock<ActionExecutingContext>(
                new ActionContext(new DefaultHttpContext(), new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                mockController.Object);

            mockContext.SetupGet(context => context.Controller)
            .Returns(mockController.Object);

            var filter = new ValidateModelStateAttribute();
            filter.OnActionExecuting(mockContext.Object);

            Assert.IsNull(mockContext.Object.Result);
        }

        //[TestMethod]
        //public void OnActionExecuting_WithInValidModelStrate_ShouldReturnsView()
        //{
        //    // неуспешен и недовършен опит на Данчо
        //    var mockController = new Mock<Controller>();

        //    var controller = mockController.Object;
        //    controller.ModelState.AddModelError("model", "Invalid model");

        //    Mock<ActionExecutingContext> mockContext = GetContext(mockController);

        //    mockContext.SetupGet(context => context.Controller)
        //    .Returns(controller);

        //    var filter = new ValidateModelStateAttribute();
        //    filter.OnActionExecuting(mockContext.Object);

        //    Assert.IsNull(mockContext.Object.Result);
        //}

        //private static Mock<ActionExecutingContext> GetContext(Mock<Controller> mockController)
        //{
        //    return new Mock<ActionExecutingContext>(
        //                    new ActionContext(new DefaultHttpContext(), new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
        //                    new List<IFilterMetadata>(),
        //                    new Dictionary<string, object>(),
        //                    mockController.Object);
        //}
    }
}
