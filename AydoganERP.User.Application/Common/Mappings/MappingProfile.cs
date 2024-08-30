using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Common.Models;

namespace AydoganERP.User.Application.Common.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        CreateMap<IPaginate<Outbox>, GetListResponse<OutboxDto>>().ReverseMap();
        CreateMap<IPaginate<Domain.Entities.User>, GetListResponse<UserDto>>().ReverseMap();
    }
}