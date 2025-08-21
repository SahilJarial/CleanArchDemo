﻿using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public  interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
