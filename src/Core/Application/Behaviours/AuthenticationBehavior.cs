using Core.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Interfaces;
namespace Core.Application.Behaviours
{
    public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantDbContext _tenantDbContext;

        public AuthenticationBehavior(IHttpContextAccessor httpContextAccessor, ITenantDbContext tenantDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantDbContext = tenantDbContext;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var attribute = typeof(TRequest)
                .GetCustomAttributes(typeof(AuthenticationAttribute), true)
                .FirstOrDefault() as AuthenticationAttribute;

            if (attribute != null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null || !httpContext.User.Identity?.IsAuthenticated == true)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }

                var userRoles = httpContext.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value);

                if (!userRoles.Contains(attribute.Role))
                {
                    throw new UnauthorizedAccessException(
                        $"User does not have required role: {attribute.Role}");
                }
            }
            return await next();
        }
    }

}
