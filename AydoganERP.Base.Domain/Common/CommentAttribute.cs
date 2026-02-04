namespace AydoganERP.Base.Domain.Common;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
public class CommentAttribute : Attribute
{
    public string Text { get; }
    public CommentAttribute(string text)
    {
        Text = text;
    }
}
