// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AydoganERP.Base.Domain.Enums;
using System.ComponentModel;

namespace AydoganERP.Base.Api.Services.PolicyCode.Utilities;

public static class PermissionChecker
{
    /// <summary>
    /// This is used by the policy provider to check the permission name string
    /// </summary>
    /// <param name="packedPermissions"></param>
    /// <param name="permissionName"></param>
    /// <returns></returns>
    public static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
    {
        var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();

        if (!Enum.TryParse(permissionName, true, out PermissionEnum permissionToCheck))
            throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(PermissionEnum)}.");

        return usersPermissions.UserHasThisPermission(permissionToCheck);
    }

    /// <summary>
    /// This is the main checker of whether a user permissions allows them to access something with the given permission
    /// </summary>
    /// <param name="usersPermissions"></param>
    /// <param name="permissionToCheck"></param>
    /// <returns></returns>
    public static bool UserHasThisPermission(this PermissionEnum[] usersPermissions, PermissionEnum permissionToCheck)
    {
        return usersPermissions.Contains(permissionToCheck) || usersPermissions.Contains(PermissionEnum.AccessAll);
    }
}