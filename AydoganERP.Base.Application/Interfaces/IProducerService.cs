namespace AydoganERP.Base.Application.Interfaces;

public interface IProducerService
{
    Task PublishAsync(string name, string data);
}
