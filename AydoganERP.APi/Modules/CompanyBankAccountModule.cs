using AydoganERP.Company.Application.CompanyBankAccountManager.Commands.Create;
using AydoganERP.Company.Application.CompanyBankAccountManager.Commands.Delete;
using AydoganERP.Company.Application.CompanyBankAccountManager.Commands.Update;
using AydoganERP.Company.Application.CompanyBankAccountManager.Queries.GetList;
using AydoganERP.Company.Application.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class CompanyBankAccountModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/CompanyBankAccounts")
            .WithTags("CompanyBankAccount")
            .RequireAuthorization();

        group
            .MapGet("", HandleGetList)
            .Produces<List<CompanyBankAccountDto>>(200)
            .ProducesProblem(500);

        group
            .MapPost("", HandleCreate)
            .Produces<CompanyBankAccountDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(500);

        group
            .MapPut("/{id:guid}", HandleUpdate)
            .Produces<CompanyBankAccountDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        group
            .MapDelete("/{id:guid}", HandleDelete)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500);
    }

    private static async Task<IResult> HandleGetList(
        [FromServices] ISender sender,
        [FromQuery] Guid companyId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCompanyBankAccountListQuery(companyId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreate(
        [FromServices] ISender sender,
        [FromBody] CreateCompanyBankAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCompanyBankAccountCommand(
            request.CompanyId,
            request.BankName,
            request.Iban,
            request.Currency,
            request.BranchName,
            request.AccountNo,
            request.AccountName,
            request.SwiftCode);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdate(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateCompanyBankAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCompanyBankAccountCommand(
            id,
            request.BankName,
            request.Iban,
            request.Currency,
            request.BranchName,
            request.AccountNo,
            request.AccountName,
            request.SwiftCode,
            request.IsActive);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleDelete(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteCompanyBankAccountCommand(id), cancellationToken);
        return Results.Ok();
    }

    public record CreateCompanyBankAccountRequest(
        Guid CompanyId,
        string BankName,
        string Iban,
        int Currency,
        string? BranchName,
        string? AccountNo,
        string? AccountName,
        string? SwiftCode);

    public record UpdateCompanyBankAccountRequest(
        string BankName,
        string Iban,
        int Currency,
        string? BranchName,
        string? AccountNo,
        string? AccountName,
        string? SwiftCode,
        bool IsActive);
}
