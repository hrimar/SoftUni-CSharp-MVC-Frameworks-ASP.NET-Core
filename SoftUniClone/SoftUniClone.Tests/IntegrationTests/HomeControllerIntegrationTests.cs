using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftUniClone.Web;
using System.Net;
using System.Threading.Tasks;

namespace SoftUniClone.Tests.IntegrationTests
{
    [TestClass]
  public  class HomeControllerIntegrationTests
    {
        [TestMethod]
        public async Task Index_ShouldReturnIndexView()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var result = await client.GetAsync("/");

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var resultContent = await result.Content.ReadAsStringAsync() ;
            Assert.IsTrue(resultContent.Contains("Welcom"));
        }
    }
}
