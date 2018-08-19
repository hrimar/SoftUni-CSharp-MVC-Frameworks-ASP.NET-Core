using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftUniClone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Tests.Controllers.HomeControllerTests
{

    [TestClass]
    public class TestIndex
    {
        [TestMethod]
        public void Index_ReturnsTheProperView()
        {
            //1. Arrange:
            var controller = new HomeController();

            //2.Act:
            var result = controller.Index();

            //3.Assert:
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
