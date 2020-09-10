using Microsoft.AspNetCore.Authorization;

namespace Demo1
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string ParmissionName { get; }

        public PermissionRequirement(string parmissionName)
        {
            this.ParmissionName = parmissionName;
        }
    }
}