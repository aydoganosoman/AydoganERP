using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.SharedManager.CategoryManager.Commands.Create;
using AydoganERP.Base.Application.SharedManager.CategoryManager.Commands.Update;
using AydoganERP.Base.Application.SharedManager.CategoryManager.Queries.GetCategoryById;
using AydoganERP.Base.Application.SharedManager.CategoryManager.Queries.GetCategoryList;
using AydoganERP.Base.Application.SharedManager.FolderManager.Commands.Create;
using AydoganERP.Base.Application.SharedManager.FolderManager.Commands.Update;
using AydoganERP.Base.Application.SharedManager.FolderManager.Queries.GetFolderById;
using AydoganERP.Base.Application.SharedManager.FolderManager.Queries.GetFolderList;
using AydoganERP.Base.Application.SharedManager.GroupManager.Commands.Create;
using AydoganERP.Base.Application.SharedManager.GroupManager.Commands.Update;
using AydoganERP.Base.Application.SharedManager.GroupManager.Queries.GetGroupById;
using AydoganERP.Base.Application.SharedManager.GroupManager.Queries.GetGroupList;
using AydoganERP.Base.Application.SharedManager.TagGroupManager.Commands.Create;
using AydoganERP.Base.Application.SharedManager.TagGroupManager.Commands.Update;
using AydoganERP.Base.Application.SharedManager.TagGroupManager.Queries.GetTagGroupById;
using AydoganERP.Base.Application.SharedManager.TagGroupManager.Queries.GetTagGroupList;
using AydoganERP.Base.Application.SharedManager.TagManager.Commands.Create;
using AydoganERP.Base.Application.SharedManager.TagManager.Commands.Update;
using AydoganERP.Base.Application.SharedManager.TagManager.Queries.GetTagById;
using AydoganERP.Base.Application.SharedManager.TagManager.Queries.GetTagList;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.APi.Modules;

public class SharedModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Groups
        var groups = app.MapGroup("/api/Groups").WithTags("Groups");

        groups.MapGet("/", async ([FromQuery] Guid? companyId, [FromQuery] UsageAreaEnum? usageArea, [FromQuery] bool? isActive, ISender sender) =>
        {
            var result = await sender.Send(new GetGroupListQuery(companyId, usageArea, isActive));
            return Results.Ok(result);
        });

        groups.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetGroupByIdQuery(id));
            return Results.Ok(result);
        });

        groups.MapPost("/", async ([FromBody] CreateGroupCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/api/Groups/{result.Id}", result);
        });

        groups.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateGroupCommand command, ISender sender) =>
        {
            if (id != command.Id) return Results.BadRequest("Id mismatch");
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
        #endregion

        #region Categories
        var categories = app.MapGroup("/api/Categories").WithTags("Categories");

        categories.MapGet("/", async ([FromQuery] Guid? companyId, [FromQuery] Guid? groupId, [FromQuery] ProcessTypeEnum? processType, [FromQuery] bool? isActive, ISender sender) =>
        {
            var result = await sender.Send(new GetCategoryListQuery(companyId, groupId, processType, isActive));
            return Results.Ok(result);
        });

        categories.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetCategoryByIdQuery(id));
            return Results.Ok(result);
        });

        categories.MapPost("/", async ([FromBody] CreateCategoryCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/api/Categories/{result.Id}", result);
        });

        categories.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateCategoryCommand command, ISender sender) =>
        {
            if (id != command.Id) return Results.BadRequest("Id mismatch");
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
        #endregion

        #region TagGroups
        var tagGroups = app.MapGroup("/api/TagGroups").WithTags("TagGroups");

        tagGroups.MapGet("/", async ([FromQuery] Guid? companyId, [FromQuery] bool? isActive, ISender sender) =>
        {
            var result = await sender.Send(new GetTagGroupListQuery(companyId, isActive));
            return Results.Ok(result);
        });

        tagGroups.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetTagGroupByIdQuery(id));
            return Results.Ok(result);
        });

        tagGroups.MapPost("/", async ([FromBody] CreateTagGroupCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/api/TagGroups/{result.Id}", result);
        });

        tagGroups.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateTagGroupCommand command, ISender sender) =>
        {
            if (id != command.Id) return Results.BadRequest("Id mismatch");
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
        #endregion

        #region Tags
        var tags = app.MapGroup("/api/Tags").WithTags("Tags");

        tags.MapGet("/", async ([FromQuery] Guid? companyId, [FromQuery] Guid? tagGroupId, [FromQuery] bool? isActive, ISender sender) =>
        {
            var result = await sender.Send(new GetTagListQuery(companyId, tagGroupId, isActive));
            return Results.Ok(result);
        });

        tags.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetTagByIdQuery(id));
            return Results.Ok(result);
        });

        tags.MapPost("/", async ([FromBody] CreateTagCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/api/Tags/{result.Id}", result);
        });

        tags.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateTagCommand command, ISender sender) =>
        {
            if (id != command.Id) return Results.BadRequest("Id mismatch");
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
        #endregion

        #region Folders
        var folders = app.MapGroup("/api/Folders").WithTags("Folders");

        folders.MapGet("/", async ([FromQuery] Guid? companyId, [FromQuery] DocumentTypeEnum? documentType, [FromQuery] bool? isActive, ISender sender) =>
        {
            var result = await sender.Send(new GetFolderListQuery(companyId, documentType, isActive));
            return Results.Ok(result);
        });

        folders.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetFolderByIdQuery(id));
            return Results.Ok(result);
        });

        folders.MapPost("/", async ([FromBody] CreateFolderCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/api/Folders/{result.Id}", result);
        });

        folders.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateFolderCommand command, ISender sender) =>
        {
            if (id != command.Id) return Results.BadRequest("Id mismatch");
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
        #endregion

        #region ProductUnits
        var units = app.MapGroup("/api/ProductUnits").WithTags("ProductUnits");

        units.MapGet("/", async (IBaseDbContext db) =>
        {
            var result = await db.ProductUnits
                .AsNoTracking()
                .Select(u => new { u.Id, u.Code, u.Name, u.EInvoice })
                .ToListAsync();
            return Results.Ok(result);
        });
        #endregion
    }
}
