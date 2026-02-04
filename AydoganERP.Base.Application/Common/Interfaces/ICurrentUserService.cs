namespace AydoganERP.Base.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string Ip { get; }
    string UserEmail { get; }
}
