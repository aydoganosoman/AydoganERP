namespace AydoganERP.Base.Application.Common.Extansions;

public record UnknownExceptionProblemDetails(string? Title, string? Type, string? Detail)
{
    public IDictionary<string, string[]> Errors { set; get; } = new Dictionary<string, string[]>();
}
