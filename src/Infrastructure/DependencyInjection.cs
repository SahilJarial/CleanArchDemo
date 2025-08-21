using Core.Application.Behaviours;
using Core.Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Use in-memory database instead of SQL Server
        services.AddDbContext<IApplicationDbContext ,ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CleanArchitectureDb"));


        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthenticationBehavior<,>));

        services.AddScoped<ITenantResolver, DomainTenantResolver>();
        services.AddScoped<TenantDbContextFactory>();

        services.AddScoped<ITenantDbContext>(provider =>
        {

            var factory = provider.GetRequiredService<TenantDbContextFactory>();
            return factory.CreateDbContext();
        });

        return services;
    }
}