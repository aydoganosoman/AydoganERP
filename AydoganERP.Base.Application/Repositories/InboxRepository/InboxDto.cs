using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Base.Application.Repositories.InboxRepository;

public class InboxDto : IMapFrom<Inbox>
{
    public Guid Id { get; set; }
    public OutboxInboxSendTypeEnum SendType { get; set; }
    public string Event { get; set; }
    public string Data { get; set; }
    public bool IsDone { get; set; }
    public string? Result { get; set; }

    public static explicit operator InboxDto(Inbox inbox)
    {
        return new InboxDto()
        {
            Id = inbox.Id,
            SendType = inbox.SendType,
            Event = inbox.Event,
            Data = inbox.Data,
            IsDone = inbox.IsDone,
            Result = inbox.Result,
        };
    }

}
