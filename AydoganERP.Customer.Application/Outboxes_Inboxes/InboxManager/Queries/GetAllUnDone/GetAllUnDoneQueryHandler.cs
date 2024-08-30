using AutoMapper;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Queries.GetAllUnDone;

public class GetAllUnDoneQueryHandler : IRequestHandler<GetAllUnDoneQuery, GetListResponse<InboxDto>>
{
    private readonly IInboxRepository _inboxRepository;
    private readonly IMapper _mapper;
    public GetAllUnDoneQueryHandler(IInboxRepository inboxRepository,
        IMapper mapper)
    {
        _inboxRepository = inboxRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<InboxDto>> Handle(GetAllUnDoneQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Inbox> inboxes = await _inboxRepository
            .GetListAsync(predicate: x => x.IsDone == false, cancellationToken: cancellationToken);

        var _mapperModel = _mapper.Map<GetListResponse<InboxDto>>(inboxes);

        return _mapperModel;
    }
}
