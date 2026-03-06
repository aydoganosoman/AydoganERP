using AydoganERP.Company.Application.DocumentNumberingManager.Commands.Create;
using AydoganERP.Company.Application.DocumentNumberingManager.Commands.Delete;
using AydoganERP.Company.Application.DocumentNumberingManager.Commands.Update;
using AydoganERP.Company.Application.DocumentNumberingManager.Queries.GetList;
using AydoganERP.Company.Application.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class DocumentNumberingModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/DocumentNumberings")
            .WithTags("DocumentNumbering")
            .RequireAuthorization();

        group
            .MapGet("", HandleGetList)
            .Produces<List<DocumentNumberingDto>>(200)
            .ProducesProblem(500);

        group
            .MapPost("", HandleCreate)
            .Produces<DocumentNumberingDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(500);

        group
            .MapPut("/{id:guid}", HandleUpdate)
            .Produces<DocumentNumberingDto>(200)
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
        var result = await sender.Send(new GetDocumentNumberingListQuery(companyId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreate(
        [FromServices] ISender sender,
        [FromBody] CreateDocumentNumberingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateDocumentNumberingCommand(
            request.CompanyId,
            request.DocumentType,
            request.Prefix,
            request.IsDefault);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdate(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateDocumentNumberingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDocumentNumberingCommand(
            id,
            request.Prefix,
            request.IsDefault,
            request.IsActive);

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleDelete(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteDocumentNumberingCommand(id), cancellationToken);
        return Results.Ok();
    }

    public record CreateDocumentNumberingRequest(
        Guid CompanyId,
        int DocumentType,
        string Prefix,
        bool IsDefault);

    public record UpdateDocumentNumberingRequest(
        string Prefix,
        bool IsDefault,
        bool IsActive);
}
