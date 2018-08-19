using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftUniClone.ServiceModels.Constants;
using SoftUniClone.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniClone.Tests.Controllers.Admin.CoursesContrpolerTests
{
    // тестваме, че AdminController е достъпрен само за админ-ри:
    [TestClass]
   public class AdminAccessTests
    {
        [TestMethod]
        public void CorsesController_ShoudBeInAdminArea()
        {
            //1. Arrabge:
            var controller = new CoursesController(null, null);

            var area = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AreaAttribute) as AreaAttribute;

                   //3. Assert:
            Assert.IsNotNull(area);
            Assert.AreEqual(WebConstants.AdminArea, area.RouteValue);
        }

        [TestMethod]
        public void CorsesController_ShoudAuthorizeAdmins()
        {
            //1. Arrabge:
            var controller = new CoursesController(null, null);

            var authorization = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AuthorizeAttribute) as AuthorizeAttribute;
                       
            //3. Assert:
            Assert.IsNotNull(authorization);
            Assert.AreEqual(WebConstants.AdminRole, authorization.Roles);
        }
    }
}
