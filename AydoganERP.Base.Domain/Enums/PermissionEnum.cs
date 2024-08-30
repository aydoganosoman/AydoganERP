using System.ComponentModel.DataAnnotations;

namespace AydoganERP.Base.Domain.Enums;

public enum PermissionEnum
{
    [Display(Name = "CustomerCreate", GroupName = "Customer")]
    CustomerCreate = 0,
    [Display(Name = "CustomerUpdate", GroupName = "Customer")]
    CustomerUpdate = 1,
    [Display(Name = "CustomerDelete", GroupName = "Customer")]
    CustomerDelete = 2,
    [Display(Name = "CustomerCreateOnlyName", GroupName = "Customer")]
    CustomerCreateOnlyName = 3,
    [Display(Name = "CustomerGetAllByAuthUser", GroupName = "Customer")]
    CustomerGetAllByAuthUser = 4,
    [Display(Name = "CustomerGetById", GroupName = "Customer")]
    CustomerGetById = 5,
    [Display(Name = "UserGetAll", GroupName = "User")]
    UserGetAll = 6,
    [Display(Name = "UserGetById", GroupName = "User")]
    UserGetById = 7,


    [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
    AccessAll = Int16.MaxValue,
}
