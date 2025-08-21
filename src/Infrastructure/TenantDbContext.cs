using Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TenantDbContext:ApplicationDbContext, ITenantDbContext
    {
        private readonly string _tenant;
        public string Tenant => _tenant;

        public TenantDbContext(DbContextOptions<ApplicationDbContext> options,string tenant) : base(options)
        {
                        _tenant = tenant;
        }



    }
}
