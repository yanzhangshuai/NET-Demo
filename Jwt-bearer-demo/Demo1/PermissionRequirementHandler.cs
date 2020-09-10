using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo1
{
    public class PermissionRequirementHandler:AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var role = context.User.FindFirst(c => c.Type ==  ClaimTypes.Role);
            if (role !=  null)
            {
                var roleValue = role.Value;
                if (roleValue == requirement.ParmissionName)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}