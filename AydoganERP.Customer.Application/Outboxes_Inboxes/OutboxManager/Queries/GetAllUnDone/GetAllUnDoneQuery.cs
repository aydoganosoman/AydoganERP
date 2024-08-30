using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.OutboxManager.Queries.GetAllUnDone;

public class GetAllUnDoneQuery : IRequest<GetListResponse<OutboxDto>>
{
}
