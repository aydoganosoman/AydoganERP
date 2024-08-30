using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Domain.Entities;

namespace AydoganERP.Customer.Application.Common.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Customer, CustomerDto>().ReverseMap();
        CreateMap<Company, CompanyDto>().ReverseMap();
        CreateMap<CustomerSegmentation, CustomerSegmentationDto>().ReverseMap();

        CreateMap<IPaginate<Outbox>, GetListResponse<OutboxDto>>().ReverseMap();
        CreateMap<IPaginate<Inbox>, GetListResponse<InboxDto>>().ReverseMap();
        CreateMap<IPaginate<Domain.Entities.Customer>, GetListResponse<CustomerDto>>().ReverseMap();
    }

}