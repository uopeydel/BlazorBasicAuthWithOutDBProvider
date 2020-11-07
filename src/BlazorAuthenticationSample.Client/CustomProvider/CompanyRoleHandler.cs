using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorAuthenticationSample.Client.CustomProvider
{
    public class CompanyRoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; }

        public CompanyRoleRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }

    public class CompanyRoleHandler : AuthorizationHandler<CompanyRoleRequirement>
    {
        private readonly IKEAppProfileProvider _KEAppProfile;
        public CompanyRoleHandler(IKEAppProfileProvider KEAppProfile)
        {
            _KEAppProfile = KEAppProfile;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CompanyRoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name || c.Type == ClaimTypes.NameIdentifier))
            {
                return;
            }

            var nameId = context.User.FindAll(c => c.Type == ClaimTypes.Name || c.Type == ClaimTypes.NameIdentifier).ToList();
            bool isHaveRole = await _KEAppProfile.CheckRole(nameId.Select(s => s.Value).FirstOrDefault());
            if (isHaveRole)
            {
                context.Succeed(requirement);
            }
            return;
        }
    }
}
