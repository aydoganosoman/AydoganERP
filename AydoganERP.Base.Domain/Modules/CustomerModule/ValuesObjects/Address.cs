namespace AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

public sealed record Address(
    int? Country,
    int? City,
    int? District,
    string? Line);