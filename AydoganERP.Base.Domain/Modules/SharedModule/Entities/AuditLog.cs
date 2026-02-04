namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

public class AuditLog
{
    public Int64 Id { get; set; }
    public string? TableName { get; set; }
    public string? Action { get; set; } 
    public string? UserEmail { get; set; }
    public DateTime? Date { get; set; }
    public string? KeyValues { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
}