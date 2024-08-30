namespace AydoganERP.Base.Application.Extansions;

public class UnknownExceptionProblemDetails
{
    //public UnknownExceptionProblemDetails(int? status,
    //    string? title,
    //    string? type,
    //    string? detail,
    //    string? instance)
    //{
    //    this.Status = status;
    //    this.Title = title;
    //    this.Type = type;
    //    this.Detail = detail;
    //    this.Instance = instance;
    //}

    public IDictionary<string, string[]> Errors { set; get; } = new Dictionary<string, string[]>();
}
