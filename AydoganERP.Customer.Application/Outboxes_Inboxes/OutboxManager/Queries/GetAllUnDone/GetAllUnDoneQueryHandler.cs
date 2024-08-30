using AutoMapper;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.OutboxManager.Queries.GetAllUnDone;

public class GetAllUnDoneQueryHandler : IRequestHandler<GetAllUnDoneQuery, GetListResponse<OutboxDto>>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IMapper _mapper;
    public GetAllUnDoneQueryHandler(IOutboxRepository outboxRepository,
        IMapper mapper)
    {
        _outboxRepository = outboxRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<OutboxDto>> Handle(GetAllUnDoneQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Outbox> outboxes = await _outboxRepository
            .GetListAsync(predicate: x => x.IsDone == false, cancellationToken: cancellationToken);

        var _mapperModel = _mapper.Map<GetListResponse<OutboxDto>>(outboxes);

        return _mapperModel;
    }
}
