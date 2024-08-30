namespace AydoganERP.Base.Domain.Common;

public class BusinessRuleValidationException : Exception
{
    public IBusinessRule BrokenRule { get; }
    public string Details { get; }

    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
        Errors = new Dictionary<string, string[]>();

        BrokenRule = brokenRule;
        this.Details = brokenRule.Message;

        Errors.Add(brokenRule.Title, new string[] { brokenRule.Message });
    }

    public IDictionary<string, string[]> Errors { get; }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}
