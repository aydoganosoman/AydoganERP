using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AydoganERP.User.Api.Filters;

public class CustomAuthFilter : IActionFilter
{
    private readonly ICurrentUserService _currentUserService;
    private readonly AuthorizationTransactionTypeEnum _authorizationTransactionType;
    public CustomAuthFilter(ICurrentUserService currentUserService, AuthorizationTransactionTypeEnum authorizationTransactionType)
    {
        _currentUserService = currentUserService;
        _authorizationTransactionType = authorizationTransactionType;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User?.FindFirstValue("Permissions") != null)
        {
            List<CompanyAuthModel> permissionCompaines = JsonConvert.DeserializeObject<List<CompanyAuthModel>>(context.HttpContext.User?.FindFirstValue("Permissions"));

            CompanyAuthModel selectedCompany = permissionCompaines
                .FirstOrDefault(x => x.Id == _currentUserService.WorkingCompanyId);

            if (selectedCompany == null)
                context.Result = new UnauthorizedResult();
            else
            {
                CompanyUserAuthModel companyUserAuthorization = selectedCompany.CompanyUserAuthorizations.
                                 FirstOrDefault(x => x.Module == (int)ModuleEnum.CompanySettingsModule);

                if (companyUserAuthorization.AuthorizationTransactionType == (int)AuthorizationTransactionTypeEnum.None)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

    }
}
