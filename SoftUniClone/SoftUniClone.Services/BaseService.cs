using AutoMapper;
using SoftUniClone.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniClone.Services
{
  public  class BaseService
    {
        public BaseService(SoftUniCloneDbContext dbContex, IMapper mapper)
        {
            this.DbContext = dbContex;
            this.Mapper = mapper;
        }

        public SoftUniCloneDbContext DbContext { get; private set; }

        public IMapper  Mapper { get; private set; }
    }
}
