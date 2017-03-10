﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dapperexample.Repository.DbContextFactory.Impl;
namespace dapperexample.Repository.DbContextFactory
{
    public interface IDbContextFactory
    {
        DapperContext GetDbContext();
    }
}
