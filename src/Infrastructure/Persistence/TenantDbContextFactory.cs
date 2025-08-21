using Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class TenantDbContextFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantResolver _tenantResolver;
        private readonly IApplicationDbContext _appDbContext;

        public TenantDbContextFactory(IConfiguration configuration, ITenantResolver tenantResolver,IApplicationDbContext appDbContext)
        {
            _configuration = configuration;
            _tenantResolver = tenantResolver;
            _appDbContext = appDbContext;

        }


        string GetConnectionString(string tenant)
        {

            //string result = _appDbContext.Users.Where();
            return "";
        }
        public ITenantDbContext CreateDbContext()
        {
            var tenant = _tenantResolver.GetTenant();



            ///mainDb fetch 
            var connectionString = _configuration.GetConnectionString(tenant);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TenantDbContext(optionsBuilder.Options, tenant);
        }
    }
}
