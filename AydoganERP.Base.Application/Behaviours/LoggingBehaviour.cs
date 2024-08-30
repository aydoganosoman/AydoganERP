using AydoganERP.Base.Application.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Base.Application.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userApiKey = _currentUserService.UserApiKey ?? string.Empty;
        string userName = string.Empty;

        //if (!string.IsNullOrEmpty(userId))
        //{
        //    userName = await _identityService.GetUserNameAsync(userId);
        //}

        _logger.LogInformation("Request: {Name} {@UserApiKey} {@UserName} {@Request}",
            requestName, userApiKey, userName, request);
    }
}
