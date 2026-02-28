using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.CategoryManager.Commands.Create;

public record CreateCategoryCommand(Guid CompanyId, string Code, string Name, Guid GroupId, string? Color, ProcessTypeEnum ProcessTypes)
    : IRequest<CategoryDto>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);
        if (group == null) throw new NotFoundException(nameof(Group), request.GroupId);

        var category = Category.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.GroupId,
            request.Code,
            request.Name,
            request.ProcessTypes,
            request.Color
        );
        await _context.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }
}

