using AydoganERP.Company.Application.CompanyManager.Commands.Activate;
using AydoganERP.Company.Application.CompanyManager.Commands.Create;
using AydoganERP.Company.Application.CompanyManager.Commands.Suspend;
using AydoganERP.Company.Application.CompanyManager.Commands.UpdateDetails;
using AydoganERP.Company.Application.CompanyManager.Commands.UpdateName;
using AydoganERP.Company.Application.CompanyManager.Queries.GetById;
using AydoganERP.Company.Application.CompanyManager.Queries.GetList;
using AydoganERP.Company.Application.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class CompanyModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var companyGroup = app.MapGroup("/api/Companies")
            .WithTags("Company")
            .RequireAuthorization();

        companyGroup
            .MapGet("", HandleGetList)
            .Produces<List<CompanyDto>>(200)
            .ProducesProblem(500);

        companyGroup
            .MapGet("/{id:guid}", HandleGetById)
            .Produces<CompanyDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        companyGroup
            .MapPost("", HandleCreate)
            .Produces<CompanyDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(500);

        companyGroup
            .MapPut("/{id:guid}/Name", HandleUpdateName)
            .Produces<CompanyDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        companyGroup
            .MapPut("/{id:guid}/Details", HandleUpdateDetails)
            .Produces<CompanyDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        companyGroup
            .MapPost("/{id:guid}/Activate", HandleActivate)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        companyGroup
            .MapPost("/{id:guid}/Suspend", HandleSuspend)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500);
    }

    private static async Task<IResult> HandleGetList(
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCompanyListQuery(), cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCompanyByIdQuery(id), cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreate(
        [FromServices] ISender sender,
        [FromBody] CreateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdateName(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateCompanyNameRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateCompanyNameCommand(id, request.Name), cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdateDetails(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateCompanyDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCompanyDetailsCommand(
            id,
            request.Name,
            request.CompanyType,
            request.ShortName,
            request.TradeRegisterNo,
            request.TradeRegisterTitle,
            request.MersisNo,
            request.TapdkNo,
            request.HeadquartersAddress,
            request.Currency,
            request.Capital,
            request.EstablishmentDate,
            // TaxInfo
            request.TaxNumber,
            request.TaxOffice,
            // ContactInfo
            request.Phone,
            request.Fax,
            request.Email,
            request.Website,
            // Address
            request.CountryId,
            request.CityId,
            request.DistrictId,
            request.AddressLine);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleActivate(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await sender.Send(new ActivateCompanyCommand(id), cancellationToken);

        return Results.Ok();
    }

    private static async Task<IResult> HandleSuspend(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await sender.Send(new SuspendCompanyCommand(id), cancellationToken);

        return Results.Ok();
    }

    public record UpdateCompanyNameRequest(string Name);

    public record UpdateCompanyDetailsRequest(
        string Name,
        int CompanyType,
        string? ShortName,
        string? TaxNumber,
        string? TaxOffice,
        string? TradeRegisterNo,
        string? TradeRegisterTitle,
        string? MersisNo,
        string? TapdkNo,
        string? HeadquartersAddress,
        int Currency,
        decimal Capital,
        DateTime? EstablishmentDate,
        string? Phone,
        string? Fax,
        string? Email,
        string? Website,
        int? CountryId,
        int? CityId,
        int? DistrictId,
        string? AddressLine);
}
