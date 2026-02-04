using AydoganERP.Api.Services;
using AydoganERP.Identity.Application.Models;
using AydoganERP.Identity.Application.UserManager.Commands.Register;
using AydoganERP.Identity.Application.UserManager.Commands.ResetPassword;
using AydoganERP.Identity.Application.UserManager.Commands.UpdateRefreshToken;
using AydoganERP.Identity.Application.UserManager.Queries.GetUserByEMailAndPassword;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/api/Auth")
            .WithTags("Auth");

        authGroup
            .MapPost("/Login", HandleLogin)
            .Produces<UserAuthModel>(200)
            .ProducesProblem(400)
            .ProducesProblem(500)
            .AllowAnonymous();

        authGroup
            .MapPost("/VerifyToken", HandleVerifyToken)
            .Produces<UserAuthModel>(200)
            .ProducesProblem(400)
            .ProducesProblem(400)
            .ProducesProblem(500)
            .RequireAuthorization();

        authGroup
            .MapPost("/RegisterCompany", HandleRegisterCompany)
            .Produces(200)
            .ProducesProblem(400)
            .ProducesProblem(500)
            .AllowAnonymous();

        authGroup.MapPost("/ResetPassword", HandleReset)
            .Produces(200)
            .ProducesProblem(500)
            .ProducesProblem(400)
            .AllowAnonymous();
    }

    private async Task<IResult> HandleLogin(HttpContext ctx,
        [FromServices] ISender _sender,
        [FromServices] IUserService _userService,
        [FromBody] GetUserByEMailAndPasswordQuery query)
    {
        var userVm = await _sender.Send(query);

        userVm.Token = _userService.Authenticate(userVm);

        await _sender.Send(new UpdateRefreshTokenCommand(userVm.ApiKey, userVm.Token.RefreshToken));

        return Results.Ok(userVm);
    }

    private async Task<IResult> HandleVerifyToken(HttpContext ctx,
        [FromServices] ISender _sender,
        [FromServices] IUserService _userService,
        [FromBody] TokenModel token)
    {
        var userVm = await _userService.VerifyToken(token);

        return Results.Ok(userVm);
    }

    private async Task<IResult> HandleRegisterCompany(HttpContext ctx,
        [FromServices] ISender _sender,
        [FromServices] IUserService _userService,
        [FromBody] RegisterCompanyCommand command)
    {
        var result = await _sender.Send(command);

        return Results.Ok();
    }

    private async Task<IResult> HandleReset(HttpContext ctx,
        [FromServices] ISender _sender,
        [FromBody] ResetPasswordCommand command)
    {
        await _sender.Send(command);
        return Results.Ok();
    }
}