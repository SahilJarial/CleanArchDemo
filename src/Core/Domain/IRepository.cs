using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
