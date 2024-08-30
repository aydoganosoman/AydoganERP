using AutoMapper;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Application.Common.Repositories;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Queries.GetAll;

public class GetAllQuery : IRequest<GetListResponse<UserDto>>
{
}

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetListResponse<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetAllQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<UserDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Domain.Entities.User> users = await _userRepository
            .GetListAsync(cancellationToken: cancellationToken);

       return _mapper.Map<GetListResponse<UserDto>>(users);

    }
}
