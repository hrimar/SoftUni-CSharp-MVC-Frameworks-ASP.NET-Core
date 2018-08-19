using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Tests.Mocks
{
   public static class MockDbContext
    {
        public static SoftUniCloneDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<SoftUniCloneDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            // 1.1. Moke DB:
           return  new SoftUniCloneDbContext(options);
        }
    }
}
