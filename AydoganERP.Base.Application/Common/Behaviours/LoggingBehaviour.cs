using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AydoganERP.Base.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggingBehaviour(ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IBaseDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var method = _httpContextAccessor.HttpContext?.Request?.Method;
        var path = _httpContextAccessor.HttpContext?.Request?.Path;
        var requestName = typeof(TRequest).Name;
        var responseName = typeof(TResponse).Name;
        var userEmail = _currentUserService.UserEmail ?? string.Empty;
        var requestJson = JsonConvert.SerializeObject(request, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        });

        var response = await next();

        var responseJson = JsonConvert.SerializeObject(response, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        });
        
        if (method == "POST" || method == "PUT" || method == "DELETE" || method == "PATCH")
        {
            var (actionType, description) = ResolveAction(method, path, requestJson);
        
            var userAction = UserActionLog.Create(_currentUserService.Ip,
                userEmail,
                method,
                path,
                requestName,
                responseName,
                requestJson,
                responseJson,
                actionType,
                description);
        
            await _dbContext.UserActionLogs.AddAsync(userAction);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return response;
    }

    private (int, string) ResolveAction(string method, string path, string requestJson)
    {
        #region User

        if (method == "POST" && path.Contains("/Login"))
            return (UserActionTypeEnum.Login,
                $"Kullanıcı girişi - {JObject.Parse(requestJson).Property("email")?.Value}");
        if (method == "POST" && path.Contains("/VerifyToken"))
            return (UserActionTypeEnum.Login,
                $"Kullanıcı doğrulama - {JObject.Parse(requestJson).Property("refreshToken")?.Value}");
        if (method == "PATCH" && path.Contains("User/Disable"))
            return (UserActionTypeEnum.Update,
                $"Kullanıcı pasif yapıldı - {JObject.Parse(requestJson).Property("id")?.Value}");
        if (method == "PATCH" && path.Contains("User/Enable"))
            return (UserActionTypeEnum.Update,
                $"Kullanıcı aktif yapıldı - {JObject.Parse(requestJson).Property("id")?.Value}");
        if (method == "PATCH" && path.Contains("User/RefreshPassword"))
            return (UserActionTypeEnum.Update,
                $"Kullanıcı şifre yenileme - {JObject.Parse(requestJson).Property("email")?.Value}");
        if (method == "POST" && path.Contains("User/Register"))
            return (UserActionTypeEnum.Create,
                $"Kullanıcı eklendi - {JObject.Parse(requestJson).Property("email")?.Value}");
        if (method == "POST" && path.Contains("User/Update"))
            return (UserActionTypeEnum.Update,
                $"Kullanıcı güncellendi - {JObject.Parse(requestJson).Property("email")?.Value}");

        #endregion

        return (UserActionTypeEnum.Other, $"{method} {path}");
    }
}