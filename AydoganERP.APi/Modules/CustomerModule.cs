using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Customer.Application.CustomerManager.Commands.ChangeStatus;
using AydoganERP.Customer.Application.CustomerManager.Commands.Create;
using AydoganERP.Customer.Application.CustomerManager.Commands.Update;
using AydoganERP.Customer.Application.CustomerManager.Queries.GetById;
using AydoganERP.Customer.Application.CustomerManager.Queries.GetList;
using AydoganERP.Customer.Application.CustomerManager.Queries.GetPaged;
using AydoganERP.Customer.Application.CustomerManager.Queries.CheckCodeExists;
using AydoganERP.Customer.Application.CustomerManager.Queries.GetNextCode;
using AydoganERP.Customer.Application.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class CustomerModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var customerGroup = app.MapGroup("/api/Customers")
            .WithTags("Customer")
            .RequireAuthorization();

        customerGroup
            .MapGet("", HandleGetList)
            .Produces<List<CustomerDto>>(200)
            .ProducesProblem(500);

        customerGroup
            .MapGet("/paged", HandleGetPaged)
            .Produces<PaginatedList<CustomerDto>>(200)
            .ProducesProblem(500);

        customerGroup
            .MapGet("/{id:guid}", HandleGetById)
            .Produces<CustomerDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        customerGroup
            .MapPost("", HandleCreate)
            .Produces<CustomerDto>(201)
            .ProducesProblem(400)
            .ProducesProblem(500);

        customerGroup
            .MapPut("/{id:guid}", HandleUpdate)
            .Produces<CustomerDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        customerGroup
            .MapPatch("/{id:guid}/status", HandleChangeStatus)
            .Produces(204)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        // Kod kontrolü
        customerGroup
            .MapGet("/check-code", HandleCheckCode)
            .Produces<CheckCodeExistsResult>(200)
            .ProducesProblem(500);

        // Sonraki kod üret
        customerGroup
            .MapGet("/next-code", HandleGetNextCode)
            .Produces<string>(200)
            .ProducesProblem(500);
    }

    private static async Task<IResult> HandleGetList(
        [FromServices] ISender sender,
        [FromQuery] Guid? companyId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomerListQuery(companyId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetPaged(
        [FromServices] ISender sender,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? companyId = null,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetPagedQuery(currentPage, pageSize, companyId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreate(
        [FromServices] ISender sender,
        [FromBody] CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Results.Created($"/api/Customers/{result.Id}", result);
    }

    private static async Task<IResult> HandleUpdate(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(
            id,
            request.CustomerName,
            request.Name,
            request.SurName,
            request.Type,
            request.PartyType,
            request.TaxNumber,
            request.TaxOffice,
            request.Email,
            request.Phone,
            request.CountryId,
            request.CityId,
            request.DistrictId,
            request.AddressLine,
            request.BankAccounts?.Select(b => new UpdateBankAccountItem(b.Id, b.IBAN, b.BankName, b.CurrencyType, b.SortOrder)).ToList(),
            request.Branches?.Select(b => new UpdateBranchItem(b.Id, b.Name, b.Email, b.Phone, b.CountryId, b.CityId, b.DistrictId, b.AddressLine)).ToList(),
            request.Contacts?.Select(c => new UpdateContactItem(c.Id, c.Name, c.Surname, c.Title, c.GSM, c.Email)).ToList(),
            request.Numbers?.Select(n => new UpdateNumberItem(n.Id, n.NumberType, n.Description)).ToList(),
            request.Notes?.Select(n => new UpdateNoteItem(n.Id, n.Date, n.Note)).ToList());

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleChangeStatus(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] ChangeStatusRequest request,
        CancellationToken cancellationToken)
    {
        await sender.Send(new ChangeCustomerStatusCommand(id, request.Status), cancellationToken);
        return Results.NoContent();
    }

    private static async Task<IResult> HandleCheckCode(
        [FromServices] ISender sender,
        [FromQuery] string code,
        [FromQuery] Guid companyId,
        [FromQuery] Guid? excludeCustomerId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new CheckCodeExistsQuery(code, companyId, excludeCustomerId),
            cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetNextCode(
        [FromServices] ISender sender,
        [FromQuery] Guid companyId,
        [FromQuery] string? prefix,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetNextCodeQuery(companyId, prefix ?? "CRI"),
            cancellationToken);
        return Results.Ok(result);
    }

    public record UpdateCustomerRequest(
        string CustomerName,
        string? Name,
        string? SurName,
        int Type,
        int PartyType,
        string? TaxNumber,
        string? TaxOffice,
        string? Email,
        string? Phone,
        int? CountryId,
        int? CityId,
        int? DistrictId,
        string? AddressLine,
        List<BankAccountRequest>? BankAccounts = null,
        List<BranchRequest>? Branches = null,
        List<ContactRequest>? Contacts = null,
        List<NumberRequest>? Numbers = null,
        List<NoteRequest>? Notes = null);

    public record BankAccountRequest(
        Guid? Id,
        string IBAN,
        string BankName,
        int CurrencyType = 0,
        int SortOrder = 0);

    public record BranchRequest(
        Guid? Id,
        string Name,
        string? Email,
        string? Phone,
        int? CountryId,
        int? CityId,
        int? DistrictId,
        string? AddressLine);

    public record ContactRequest(
        Guid? Id,
        string Name,
        string Surname,
        string? Title,
        string? GSM,
        string? Email);

    public record NumberRequest(
        Guid? Id,
        int NumberType,
        string Description);

    public record NoteRequest(
        Guid? Id,
        DateTime Date,
        string Note);

    public record ChangeStatusRequest(int Status);
}
