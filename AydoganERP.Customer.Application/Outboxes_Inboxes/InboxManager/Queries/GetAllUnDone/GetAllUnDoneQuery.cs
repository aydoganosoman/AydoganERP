using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Queries.GetAllUnDone;

public class GetAllUnDoneQuery : IRequest<GetListResponse<InboxDto>>
{
}
