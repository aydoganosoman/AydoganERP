using AutoMapper;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Identity.Application.Models;
using AydoganERP.Identity.Application.Repositories;
using MediatR;

namespace AydoganERP.Identity.Application.UserManager.Queries.GetById;

public record GetByIdQuery(Guid Id) : IRequest<UserDto>;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        User query = await _userRepository
            .GetAsync(expression: x => x.Id == request.Id);

        var _mapped = _mapper.Map<UserDto>(query);

        return _mapped;
    }
}