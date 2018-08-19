using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftUniClone.Data;
using SoftUniClone.Models;
using SoftUniClone.Services.Admin;
using SoftUniClone.Tests.Mocks;
using SoftUniClone.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniClone.Tests.Services.AdminCourses
{
    [TestClass]
    public class GetCoursesTests
    {
        private SoftUniCloneDbContext dbContext;
        private IMapper mapper;

        [TestInitialize]
        public void InitilizeTests() // runs befor every test
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutomapper.GetMapper();
        }



        [TestMethod]
        public async Task GetCourses_WithAFewCourses_ShouldReturnAll()
        {
            // 1. Arrange
            //var options = new DbContextOptionsBuilder<SoftUniCloneDbContext>()
            //    .UseInMemoryDatabase(Guid.NewGuid().ToString())
            //    .Options;
            
            //// 1.1. Moke DB:
            //var dbContext = new SoftUniCloneDbContext(options);
            dbContext.Courses.Add(new Course() { Id = 1, Name = "First course" });            
            dbContext.Courses.Add(new Course() { Id = 2, Name = "Second course" });
            dbContext.Courses.Add(new Course() { Id = 3, Name = "Third course" });
            dbContext.SaveChanges();

            
            //// 1.2.
            //AutoMapper.Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());


            var service = new AdminCoursesService(dbContext, this.mapper);

            ////2. Act
            var courses = await service.GetCourses();

            ////3.Assert
            Assert.IsNotNull(courses);
            Assert.AreEqual(3, courses.Count());
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, courses.Select(c => c.Id).ToArray());
        }

        [TestMethod]
        public async Task GetCourses_WithNoCourses_ShouldReturnNone()
        {
            // 1. Arrange           
            var service = new AdminCoursesService(dbContext, this.mapper);

            ////2. Act
            var courses = await service.GetCourses();

            ////3.Assert
            Assert.IsNotNull(courses);
            Assert.AreEqual(0, courses.Count());            
        }
    }
}
