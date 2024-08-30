// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AydoganERP.Base.Domain.Enums;

namespace AydoganERP.Base.Api.Services.PolicyCode.Utilities;

public static class PermissionPackers
{
    public static IEnumerable<PermissionEnum> UnpackPermissionsFromString(this string packedPermissions)
    {
        if (packedPermissions == null)
            throw new ArgumentNullException(nameof(packedPermissions));

        foreach (var character in packedPermissions.Split(','))
        {
            yield return (PermissionEnum)Convert.ToInt32(character);
        }
    }

    public static PermissionEnum? FindPermissionViaName(this string permissionName)
    {
        return Enum.TryParse(permissionName, out PermissionEnum permission)
            ? permission
            : null;
    }

}