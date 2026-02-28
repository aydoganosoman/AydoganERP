using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Inventory.Application.Models;
using AydoganERP.Inventory.Application.ProductManager.Commands.Create;
using AydoganERP.Inventory.Application.ProductManager.Commands.Update;
using AydoganERP.Inventory.Application.ProductManager.Queries.CheckCodeExists;
using AydoganERP.Inventory.Application.ProductManager.Queries.GetByBarcode;
using AydoganERP.Inventory.Application.ProductManager.Queries.GetById;
using AydoganERP.Inventory.Application.ProductManager.Queries.GetList;
using AydoganERP.Inventory.Application.ProductManager.Queries.GetNextCode;
using AydoganERP.Inventory.Application.ProductSerialNumberManager.Commands.UpdateStatus;
using AydoganERP.Inventory.Application.ProductSerialNumberManager.Queries.GetByCode;
using AydoganERP.Inventory.Application.StockMovementManager.Commands.BulkCreate;
using AydoganERP.Inventory.Application.StockMovementManager.Commands.Create;
using AydoganERP.Inventory.Application.StockMovementManager.Queries.GetByProduct;
using AydoganERP.Inventory.Application.StockMovementManager.Queries.GetList;
using AydoganERP.Inventory.Application.StockMovementManager.Queries.GetStockLevel;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AydoganERP.Api.Modules;

public class InventoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Product endpoints
        var productGroup = app.MapGroup("/api/Products")
            .WithTags("Product")
            .RequireAuthorization();

        productGroup
            .MapGet("", HandleGetProductList)
            .Produces<PaginatedList<ProductDto>>(200)
            .ProducesProblem(500);

        productGroup
            .MapGet("/{id:guid}", HandleGetProductById)
            .Produces<ProductDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        productGroup
            .MapGet("/by-barcode", HandleGetProductByBarcode)
            .Produces<ProductWithStockDto>(200)
            .Produces(204)
            .ProducesProblem(500);

        productGroup
            .MapGet("/check-code", HandleCheckProductCode)
            .Produces<bool>(200)
            .ProducesProblem(500);

        productGroup
            .MapGet("/next-code", HandleGetNextProductCode)
            .Produces<string>(200)
            .ProducesProblem(500);

        productGroup
            .MapPost("", HandleCreateProduct)
            .Produces<ProductDto>(201)
            .ProducesProblem(400)
            .ProducesProblem(500);

        productGroup
            .MapPut("/{id:guid}", HandleUpdateProduct)
            .Produces<ProductDto>(200)
            .ProducesProblem(400)
            .ProducesProblem(404)
            .ProducesProblem(500);

        productGroup
            .MapGet("/{id:guid}/stock-level", HandleGetStockLevel)
            .Produces<StockLevelDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(500);

        productGroup
            .MapGet("/{id:guid}/movements", HandleGetStockMovementsByProduct)
            .Produces<List<StockMovementDto>>(200)
            .ProducesProblem(500);

        // StockMovement endpoints
        var stockMovementGroup = app.MapGroup("/api/StockMovements")
            .WithTags("StockMovement")
            .RequireAuthorization();

        stockMovementGroup
            .MapGet("", HandleGetStockMovementList)
            .Produces<PaginatedList<StockMovementDto>>(200)
            .ProducesProblem(500);

        stockMovementGroup
            .MapPost("", HandleCreateStockMovement)
            .Produces<StockMovementDto>(201)
            .ProducesProblem(400)
            .ProducesProblem(500);

        stockMovementGroup
            .MapPost("/bulk", HandleBulkCreateStockMovement)
            .Produces<BulkCreateStockMovementResult>(200)
            .ProducesProblem(400)
            .ProducesProblem(500);

        // SerialNumber endpoints
        var serialNumberGroup = app.MapGroup("/api/SerialNumbers")
            .WithTags("SerialNumber")
            .RequireAuthorization();

        serialNumberGroup
            .MapGet("/by-code", HandleGetSerialNumberByCode)
            .Produces<SerialNumberDetailDto>(200)
            .Produces(204)
            .ProducesProblem(500);

        serialNumberGroup
            .MapPut("/{id:guid}/status", HandleUpdateSerialNumberStatus)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(500);
    }

    #region Product Handlers

    private static async Task<IResult> HandleGetProductList(
        [FromServices] ISender sender,
        [FromQuery] Guid? companyId,
        [FromQuery] Guid? categoryId,
        [FromQuery] string? searchText,
        [FromQuery] bool? isActive,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new GetProductListQuery(companyId, categoryId, searchText, isActive, pageNumber, pageSize),
            cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetProductById(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetProductByBarcode(
        [FromServices] ISender sender,
        [FromQuery] string barcode,
        [FromQuery] Guid? companyId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProductByBarcodeQuery(barcode, companyId), cancellationToken);
        return result != null ? Results.Ok(result) : Results.NoContent();
    }

    private static async Task<IResult> HandleCheckProductCode(
        [FromServices] ISender sender,
        [FromQuery] Guid companyId,
        [FromQuery] string code,
        [FromQuery] Guid? excludeId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CheckProductCodeExistsQuery(code, companyId, excludeId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetNextProductCode(
        [FromServices] ISender sender,
        [FromQuery] Guid companyId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetNextProductCodeQuery(companyId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateProduct(
        [FromServices] ISender sender,
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Results.Created($"/api/Products/{result.Id}", result);
    }

    private static async Task<IResult> HandleUpdateProduct(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.UnitId,
            request.IsLotTracked,
            request.IsSerialTracked,
            request.IsActive,
            request.PurchaseUnitPrice,
            request.PurchaseUnitPriceCurrency,
            request.PurchaseUnitPriceVatInclude,
            request.PurchaseVatRate,
            request.SaleUnitPrice,
            request.SaleUnitPriceCurrency,
            request.SaleUnitPriceVatInclude,
            request.SaleVatRate,
            request.CategoryId,
            request.Barcodes?.Select(b => new UpdateProductBarcodeItem(b.Id, b.Barcode, b.Quantity ?? 1, b.Unit ?? "Adet")).ToList(),
            request.Suppliers?.Select(s => new UpdateProductSupplierItem(s.Id, s.CustomerId, s.Code, s.Name)).ToList());

        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetStockLevel(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetStockLevelQuery(id), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleGetStockMovementsByProduct(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetStockMovementsByProductQuery(id), cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region StockMovement Handlers

    private static async Task<IResult> HandleGetStockMovementList(
        [FromServices] ISender sender,
        [FromQuery] Guid? companyId,
        [FromQuery] Guid? productId,
        [FromQuery] int? type,
        [FromQuery] DateOnly? dateFrom,
        [FromQuery] DateOnly? dateTo,
        [FromQuery] string? searchText,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new GetStockMovementListQuery(companyId, productId, type, dateFrom, dateTo, searchText, pageNumber, pageSize),
            cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateStockMovement(
        [FromServices] ISender sender,
        [FromBody] CreateStockMovementCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Results.Created($"/api/StockMovements/{result.Id}", result);
    }

    private static async Task<IResult> HandleBulkCreateStockMovement(
        [FromServices] ISender sender,
        [FromBody] BulkCreateStockMovementCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region SerialNumber Handlers

    private static async Task<IResult> HandleGetSerialNumberByCode(
        [FromServices] ISender sender,
        [FromQuery] string serialNumber,
        [FromQuery] Guid? companyId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetSerialNumberByCodeQuery(serialNumber, companyId), cancellationToken);
        return result != null ? Results.Ok(result) : Results.NoContent();
    }

    private static async Task<IResult> HandleUpdateSerialNumberStatus(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateSerialNumberStatusRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateSerialNumberStatusCommand(id, request.Status, request.Notes),
            cancellationToken);
        return result ? Results.Ok() : Results.NotFound();
    }

    public record UpdateSerialNumberStatusRequest(int Status, string? Notes = null);

    #endregion

    public record UpdateProductRequest(
        string Name,
        Guid UnitId,
        bool IsLotTracked,
        bool IsSerialTracked,
        bool IsActive,
        decimal PurchaseUnitPrice = 0,
        int PurchaseUnitPriceCurrency = 0,
        bool PurchaseUnitPriceVatInclude = false,
        float PurchaseVatRate = 0,
        decimal SaleUnitPrice = 0,
        int SaleUnitPriceCurrency = 0,
        bool SaleUnitPriceVatInclude = false,
        float SaleVatRate = 0,
        Guid? CategoryId = null,
        List<BarcodeRequest>? Barcodes = null,
        List<SupplierRequest>? Suppliers = null);

    public record BarcodeRequest(
        Guid? Id,
        string Barcode,
        decimal? Quantity = 1,
        string? Unit = "Adet");

    public record SupplierRequest(
        Guid? Id,
        Guid CustomerId,
        string Code,
        string Name);
}
