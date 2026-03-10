using AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.Create;
using AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.Delete;
using AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.Update;
using AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.UpdateDefaults;
using AydoganERP.Company.Application.ECommerceIntegrationManager.Queries.GetById;
using AydoganERP.Company.Application.ECommerceIntegrationManager.Queries.GetList;
using AydoganERP.Company.Application.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class ECommerceIntegrationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/ECommerceIntegrations")
            .WithTags("ECommerceIntegration")
            .RequireAuthorization();

        group
            .MapGet("", HandleGetList)
            .Produces<List<ECommerceIntegrationDto>>(200)
            .ProducesProblem(500);

        group
            .MapGet("/{id:guid}", HandleGetById)
            .Produces<ECommerceIntegrationDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        group
            .MapPost("", HandleCreate)
            .Produces<ECommerceIntegrationDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(500);

        group
            .MapPut("/{id:guid}", HandleUpdate)
            .Produces<ECommerceIntegrationDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        group
            .MapDelete("/{id:guid}", HandleDelete)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        group
            .MapPut("/{id:guid}/Defaults", HandleUpdateDefaults)
            .Produces<IntegrationDefaultsDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);
    }

    private static async Task<IResult> HandleGetList(
        [FromServices] ISender sender,
        [FromQuery] Guid companyId,
        [FromQuery] int? integrationType,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetECommerceIntegrationListQuery(companyId, integrationType), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetECommerceIntegrationByIdQuery(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreate(
        [FromServices] ISender sender,
        [FromBody] CreateECommerceIntegrationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateECommerceIntegrationCommand(
            request.CompanyId,
            request.IntegrationType,
            request.StoreName,
            request.Credentials,
            request.IntegrationUrl,
            request.Username);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdate(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateECommerceIntegrationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateECommerceIntegrationCommand(
            id,
            request.StoreName,
            request.Credentials,
            request.IntegrationUrl,
            request.Username,
            request.IsActive);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleDelete(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteECommerceIntegrationCommand(id), cancellationToken);
        return Results.Ok();
    }

    private static async Task<IResult> HandleUpdateDefaults(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateIntegrationDefaultsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateIntegrationDefaultsCommand(
            id,
            request.ConsiderOrderStatuses,
            request.OrderStatuses,
            request.AutoCreateBarcode,
            request.InvoiceDateType,
            request.DefaultVatRate,
            request.VatExemptionCode,
            request.ExportVatExemptionCode,
            request.ShippingFeeAccountId,
            request.InstallmentFeeAccountId,
            request.DefaultCustomerId,
            request.PaymentMethod,
            request.CargoCompanyId,
            request.DefaultCategoryId,
            request.EInvoiceSeriesId,
            request.EArchiveSeriesId,
            request.OrderFilterDaysBefore);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    public record CreateECommerceIntegrationRequest(
        Guid CompanyId,
        int IntegrationType,
        string StoreName,
        string Credentials,
        string? IntegrationUrl,
        string? Username);

    public record UpdateECommerceIntegrationRequest(
        string StoreName,
        string Credentials,
        string? IntegrationUrl,
        string? Username,
        bool IsActive);

    public record UpdateIntegrationDefaultsRequest(
        bool ConsiderOrderStatuses,
        string? OrderStatuses,
        bool AutoCreateBarcode,
        int? InvoiceDateType,
        decimal DefaultVatRate,
        string? VatExemptionCode,
        string? ExportVatExemptionCode,
        Guid? ShippingFeeAccountId,
        Guid? InstallmentFeeAccountId,
        Guid? DefaultCustomerId,
        int? PaymentMethod,
        Guid? CargoCompanyId,
        Guid? DefaultCategoryId,
        Guid? EInvoiceSeriesId,
        Guid? EArchiveSeriesId,
        int OrderFilterDaysBefore);
}
