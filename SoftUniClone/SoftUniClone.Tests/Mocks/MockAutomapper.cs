using AutoMapper;
using SoftUniClone.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Tests.Mocks
{
  public static  class MockAutomapper
    {
        static MockAutomapper() // execut only once
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
        }

        public static IMapper GetMapper()
        {
            return Mapper.Instance;
        }
    }
}
