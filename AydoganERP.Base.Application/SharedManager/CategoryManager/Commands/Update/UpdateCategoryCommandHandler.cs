using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.CategoryManager.Commands.Update;

public record UpdateCategoryCommand(Guid Id, string Code, string Name, Guid GroupId, string? Color, ProcessTypeEnum ProcessTypes, bool IsActive)
    : IRequest<CategoryDto>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IBaseDbContext context, IDomainEventUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category == null) throw new NotFoundException(nameof(Category), request.Id);
        category.Update(request.Name, request.ProcessTypes, request.Color);
        category.ChangeGroup(request.GroupId);
        category.SetActive(request.IsActive);
        await _unitOfWork.CommitAsync(null, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }
}