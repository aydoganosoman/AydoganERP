using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Finance.Application.Models;
using AydoganERP.Finance.Application.InvoiceManager.Commands.Create;
using AydoganERP.Finance.Application.InvoiceManager.Commands.Update;
using AydoganERP.Finance.Application.InvoiceManager.Commands.Approve;
using AydoganERP.Finance.Application.InvoiceManager.Commands.Cancel;
using AydoganERP.Finance.Application.InvoiceManager.Commands.AddLine;
using AydoganERP.Finance.Application.InvoiceManager.Commands.RemoveLine;
using AydoganERP.Finance.Application.InvoiceManager.Commands.RecordPayment;
using AydoganERP.Finance.Application.InvoiceManager.Commands.SendEInvoice;
using AydoganERP.Finance.Application.InvoiceManager.Queries.GetById;
using AydoganERP.Finance.Application.InvoiceManager.Queries.GetList;
using AydoganERP.Finance.Application.InvoiceManager.Queries.GetByCustomer;
using AydoganERP.Finance.Application.InvoiceManager.Queries.GetDashboardSummary;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class FinanceModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var invoiceGroup = app.MapGroup("/api/Invoices")
            .WithTags("Invoice")
            .RequireAuthorization();

        // List
        invoiceGroup
            .MapGet("", HandleGetInvoiceList)
            .Produces<PaginatedList<InvoiceListDto>>(200)
            .ProducesProblem(500);

        // Get by ID
        invoiceGroup
            .MapGet("/{id:guid}", HandleGetInvoiceById)
            .Produces<InvoiceDto>(200)
            .Produces(404)
            .ProducesProblem(500);

        // Get by Customer
        invoiceGroup
            .MapGet("/by-customer/{customerId:guid}", HandleGetInvoicesByCustomer)
            .Produces<List<InvoiceListDto>>(200)
            .ProducesProblem(500);

        // Create
        invoiceGroup
            .MapPost("", HandleCreateInvoice)
            .Produces<InvoiceDto>(201)
            .ProducesProblem(400)
            .ProducesProblem(500);

        // Update
        invoiceGroup
            .MapPut("/{id:guid}", HandleUpdateInvoice)
            .Produces<InvoiceDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Approve
        invoiceGroup
            .MapPost("/{id:guid}/approve", HandleApproveInvoice)
            .Produces<InvoiceDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Cancel
        invoiceGroup
            .MapPost("/{id:guid}/cancel", HandleCancelInvoice)
            .Produces<InvoiceDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Add Line
        invoiceGroup
            .MapPost("/{id:guid}/lines", HandleAddInvoiceLine)
            .Produces<InvoiceDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Remove Line
        invoiceGroup
            .MapDelete("/{id:guid}/lines/{lineId:guid}", HandleRemoveInvoiceLine)
            .Produces<InvoiceDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Record Payment
        invoiceGroup
            .MapPost("/{id:guid}/payments", HandleRecordPayment)
            .Produces<InvoiceDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Send E-Invoice
        invoiceGroup
            .MapPost("/{id:guid}/send-einvoice", HandleSendEInvoice)
            .Produces<SendEInvoiceResult>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Dashboard
        invoiceGroup
            .MapGet("/dashboard", HandleGetDashboard)
            .Produces<FinanceDashboardDto>(200)
            .ProducesProblem(500);
    }

    #region Handlers

    private static async Task<IResult> HandleGetInvoiceList(
        [FromServices] ISender sender,
        [FromQuery] Guid? companyId,
        [FromQuery] int? invoiceType,
        [FromQuery] int? status,
        [FromQuery] Guid? customerId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? searchText,
        [FromQuery] bool? isPaid,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new GetInvoiceListQuery(companyId, invoiceType, status, customerId, startDate, endDate, searchText, isPaid, pageNumber, pageSize),
            cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetInvoiceById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInvoiceByIdQuery(id), cancellationToken);
        return result != null ? Results.Ok(result) : Results.NotFound();
    }

    private static async Task<IResult> HandleGetInvoicesByCustomer(
        [FromServices] ISender sender,
        [FromRoute] Guid customerId,
        [FromQuery] int? invoiceType,
        [FromQuery] bool? unpaidOnly,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetInvoicesByCustomerQuery(customerId, invoiceType, unpaidOnly),
            cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateInvoice(
        [FromServices] ISender sender,
        [FromBody] CreateInvoiceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Results.Created($"/api/Invoices/{result.Id}", result);
    }

    private static async Task<IResult> HandleUpdateInvoice(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateInvoiceRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateInvoiceCommand(
            id,
            request.InvoiceDate,
            request.PaymentTermDays,
            request.Currency,
            request.ExchangeRate,
            request.Description,
            request.Notes);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleApproveInvoice(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ApproveInvoiceCommand(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCancelInvoice(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] CancelInvoiceRequest? request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CancelInvoiceCommand(id, request?.Reason), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleAddInvoiceLine(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] AddInvoiceLineRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddInvoiceLineCommand(
            id,
            request.ProductId,
            request.ProductCode,
            request.ProductName,
            request.UnitName,
            request.Quantity,
            request.UnitPrice,
            request.VatRate,
            request.DiscountRate,
            request.Description,
            request.SerialNumberId);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleRemoveInvoiceLine(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromRoute] Guid lineId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RemoveInvoiceLineCommand(id, lineId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleRecordPayment(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] RecordPaymentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RecordPaymentCommand(
            id,
            request.PaymentDate,
            request.Amount,
            request.PaymentMethod,
            request.Reference,
            request.Notes);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleSendEInvoice(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new SendEInvoiceCommand(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetDashboard(
        [FromServices] ISender sender,
        [FromQuery] Guid? companyId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDashboardSummaryQuery(companyId, startDate, endDate),
            cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region Request DTOs

    public record UpdateInvoiceRequest(
        DateTime InvoiceDate,
        int PaymentTermDays,
        int Currency,
        decimal ExchangeRate,
        string? Description,
        string? Notes);

    public record CancelInvoiceRequest(string? Reason);

    public record AddInvoiceLineRequest(
        Guid? ProductId,
        string ProductCode,
        string ProductName,
        string? UnitName,
        decimal Quantity,
        decimal UnitPrice,
        float VatRate,
        float DiscountRate = 0,
        string? Description = null,
        Guid? SerialNumberId = null);

    public record RecordPaymentRequest(
        DateTime PaymentDate,
        decimal Amount,
        int PaymentMethod = 0,
        string? Reference = null,
        string? Notes = null);

    #endregion
}
