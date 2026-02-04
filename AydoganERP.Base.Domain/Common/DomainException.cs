namespace AydoganERP.Base.Domain.Common;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
public static class Guard
{
    public static void Against(bool condition, string message)
    {
        if (condition) throw new DomainException(message);
    }
}
