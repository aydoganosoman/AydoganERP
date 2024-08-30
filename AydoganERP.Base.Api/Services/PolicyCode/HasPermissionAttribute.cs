// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AydoganERP.Base.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AydoganERP.Base.Api.Services.PolicyCode;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionEnum permission) : base(permission.ToString())
    { }
}