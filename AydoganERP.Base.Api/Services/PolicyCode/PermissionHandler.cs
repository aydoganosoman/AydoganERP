// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AydoganERP.Base.Api.Services.PolicyCode.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace AydoganERP.Base.Api.Services.PolicyCode;

//thanks to https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissionsClaim =
            context.User.Claims.SingleOrDefault(c => c.Type == "Permissions");
        // If user does not have the scope claim, get out of here
        if (permissionsClaim == null)
            return Task.CompletedTask;

        if (permissionsClaim.Value.ThisPermissionIsAllowed(requirement.PermissionName))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}