using Microsoft.AspNetCore.Authorization;

namespace Common.Authorization.PolicyAuth.PermissionsPolicy
{
    public class IsAllowedRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; set; }

        public IsAllowedRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
