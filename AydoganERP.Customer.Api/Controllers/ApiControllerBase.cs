﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Customer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}
