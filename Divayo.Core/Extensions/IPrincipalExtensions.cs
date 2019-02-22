using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Divayo.Core.Extensions
{
    public static class IPrincipalExtensions
    {
        public static Guid GetUserId(this IPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var id = claims.FirstOrDefault(s => s.Type == ClaimTypes.Name)?.Value;
            return new Guid(id);
        }
    }
}
