using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public class DomainTenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DomainTenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTenant()
        {
            var host = _httpContextAccessor.HttpContext?.Request.Host.Host;
            return host ?? "default";
        }
    }
}
