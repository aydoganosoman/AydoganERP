using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Base.Application.Repositories.OutboxRepository;

public class OutboxDto : IMapFrom<Outbox>
{
    public Guid Id { get; set; }
    public OutboxInboxSendTypeEnum SendType { get; set; }
    public string Event { get; set; }
    public string Data { get; set; }
    public bool IsDone { get; set; }
    public string? Result { get; set; }

    public static explicit operator OutboxDto(Outbox outbox)
    {
        return new OutboxDto()
        {
            Id = outbox.Id,
            SendType = outbox.SendType,
            Event = outbox.Event,
            Data = outbox.Data,
            IsDone = outbox.IsDone,
            Result = outbox.Result,
        };
    }

}
