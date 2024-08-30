using FastEndpoints;
using MassTransit;
using MediatR;

namespace AydoganERP.User.Api.Controllers;

public class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}

public class BaseEndpointWithoutResponse<TRequest> : Endpoint<TRequest>
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}

public class BaseEndpointWithoutRequest<TResponse> : EndpointWithoutRequest<TResponse>
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}