namespace AydoganERP.Base.Domain.ValueObjects;
public class RedisEntryId
{
    public long? Timestamp { get; set; }
    public int? Sequence { get; set; }

    //For EF
    public RedisEntryId()
    {
        
    }

    public RedisEntryId(string entryId)
    {
        var parts = entryId.Split('-');
        Timestamp = long.Parse(parts[0]);
        Sequence = int.Parse(parts[1]);
    }

    public override string ToString()
    {
        return $"{Timestamp}-{Sequence}";
    }
}
