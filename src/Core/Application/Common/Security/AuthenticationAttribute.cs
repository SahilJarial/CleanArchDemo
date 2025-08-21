using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AuthenticationAttribute : Attribute
    {
        public string Role { get; }

        public AuthenticationAttribute(string role)
        {
            Role = role;
        }
    }
}
