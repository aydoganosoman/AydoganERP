using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.ValueObjects;

namespace AydoganERP.Base.Domain.Entities;

public class Outbox : Entity<Guid>
{
    private Outbox(Guid id,
        string @event,
        string data,
        OutboxInboxSendTypeEnum sendType)
    {
        this.Id = id;
        this.Event = @event;
        this.Data = data;
        this.SendType = sendType;
    }

    private Outbox(string @event,
        string data,
        OutboxInboxSendTypeEnum sendType,
        RedisEntryId entryId)
    {
        this.Id = Guid.NewGuid();
        this.Event = @event;
        this.Data = data;
        this.SendType = sendType;
        this.EntryId = entryId;
    }

    public RedisEntryId EntryId { get; set; }
    public OutboxInboxSendTypeEnum SendType { get; private set; }
    public string Event { get; private set; }
    public string Data { get; private set; }
    public bool IsDone { get; private set; }
    public string? Result { get; private set; }

    public void SetDone(bool value) => this.IsDone = value;
    public void SetResult(string result) => this.Result = result;

    public static Outbox Create(string @event,
        string data,
        OutboxInboxSendTypeEnum sendType,
        RedisEntryId entryId = null)
    {
        return new Outbox(@event, data, sendType, entryId);
    }

}
