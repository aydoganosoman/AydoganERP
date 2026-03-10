using AydoganERP.Company.Application.EInvoiceIntegrationManager.Commands.Create;
using AydoganERP.Company.Application.EInvoiceIntegrationManager.Commands.Delete;
using AydoganERP.Company.Application.EInvoiceIntegrationManager.Commands.Update;
using AydoganERP.Company.Application.EInvoiceIntegrationManager.Queries.GetById;
using AydoganERP.Company.Application.EInvoiceIntegrationManager.Queries.GetList;
using AydoganERP.Company.Application.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class EInvoiceIntegrationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/EInvoiceIntegrations")
            .WithTags("EInvoiceIntegration")
            .RequireAuthorization();

        group
            .MapGet("", HandleGetList)
            .Produces<List<EInvoiceIntegrationDto>>(200)
            .ProducesProblem(500);

        group
            .MapGet("/{id:guid}", HandleGetById)
            .Produces<EInvoiceIntegrationDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        group
            .MapPost("", HandleCreate)
            .Produces<EInvoiceIntegrationDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(500);

        group
            .MapPut("/{id:guid}", HandleUpdate)
            .Produces<EInvoiceIntegrationDto>(200)
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
        [FromQuery] int? integrationType,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetEInvoiceIntegrationListQuery(companyId, integrationType), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetEInvoiceIntegrationByIdQuery(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreate(
        [FromServices] ISender sender,
        [FromBody] CreateEInvoiceIntegrationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateEInvoiceIntegrationCommand(
            request.CompanyId,
            request.IntegrationType,
            request.Settings);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdate(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateEInvoiceIntegrationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateEInvoiceIntegrationCommand(
            id,
            request.Settings,
            request.IsActive);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleDelete(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteEInvoiceIntegrationCommand(id), cancellationToken);
        return Results.Ok();
    }

    public record CreateEInvoiceIntegrationRequest(
        Guid CompanyId,
        int IntegrationType,
        string Settings);

    public record UpdateEInvoiceIntegrationRequest(
        string Settings,
        bool IsActive);

}
