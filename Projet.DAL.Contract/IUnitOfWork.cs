﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IRepository GetRepository<T>() where T : class;
    }
}
