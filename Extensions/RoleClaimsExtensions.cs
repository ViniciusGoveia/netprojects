using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Extensions
{
    public static class RoleClaimsExtensions
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            List<Claim> results =
            [
                new(ClaimTypes.Name, user.Email), .. user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            ];

            return results;
        }
    }
}
