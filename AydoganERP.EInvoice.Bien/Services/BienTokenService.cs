using AydoganERP.EInvoice.Abstractions.Models.Settings;
using AydoganERP.EInvoice.Bien.Integration;
using Microsoft.Extensions.Logging;

namespace AydoganERP.EInvoice.Bien.Services;

/// <summary>
/// Bien SOAP token yönetim servisi
/// Token tabanlı authentication ile SOAP client oluşturur
/// </summary>
public class BienTokenService
{
    private readonly BienSettings _settings;
    private readonly ILogger<BienTokenService> _logger;

    public BienTokenService(BienSettings settings, ILogger<BienTokenService> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    /// <summary>
    /// Token gerektirmeyen işlemler için basit SOAP client oluşturur
    /// </summary>
    private IntegrationClient CreateUnauthenticatedClient()
    {
        // var binding = new BasicHttpBinding
        // {
        //     MaxReceivedMessageSize = int.MaxValue,
        //     MaxBufferSize = int.MaxValue,
        //     ReaderQuotas =
        //     {
        //         MaxDepth = 32,
        //         MaxStringContentLength = int.MaxValue,
        //         MaxArrayLength = int.MaxValue,
        //         MaxBytesPerRead = int.MaxValue,
        //         MaxNameTableCharCount = int.MaxValue
        //     },
        //     Security = { Mode = BasicHttpSecurityMode.Transport }
        // };
        //
        // var endpoint = new EndpointAddress($"{_settings.Url.TrimEnd('/')}/Integration");
        // return new IntegrationClient(binding, endpoint);
        
        var _basicIntegrationClient = new IntegrationClient(IntegrationClient.EndpointConfiguration.BasicHttpBinding_IIntegration);
        _basicIntegrationClient.Endpoint.Address = new System.ServiceModel.EndpointAddress($"{_settings.Url.TrimEnd('/')}/Integration");
        _basicIntegrationClient.ClientCredentials.UserName.UserName = _settings.UserName;
        _basicIntegrationClient.ClientCredentials.UserName.Password = _settings.Password;
        
        return _basicIntegrationClient;
    }

    /// <summary>
    /// Token ile authenticate edilmiş SOAP client oluşturur
    /// </summary>
    public async Task<IntegrationClient> CreateAuthenticatedClientAsync()
    {
        // var token = await GetValidTokenAsync();
        //
        // var binding = new BasicHttpBinding
        // {
        //     MaxReceivedMessageSize = int.MaxValue,
        //     MaxBufferSize = int.MaxValue,
        //     ReaderQuotas =
        //     {
        //         MaxDepth = 32,
        //         MaxStringContentLength = int.MaxValue,
        //         MaxArrayLength = int.MaxValue,
        //         MaxBytesPerRead = int.MaxValue,
        //         MaxNameTableCharCount = int.MaxValue
        //     },
        //     Security =
        //     {
        //         Mode = BasicHttpSecurityMode.TransportWithMessageCredential,
        //         Message = { ClientCredentialType = BasicHttpMessageCredentialType.UserName }
        //     }
        // };
        
        // var endpoint = new EndpointAddress($"{_settings.Url.TrimEnd('/')}/Integration");
        // var client = new IntegrationClient(binding, endpoint);
        //
        // client.ClientCredentials.UserName.UserName = _settings.UserName;
        // client.ClientCredentials.UserName.Password = token;

        var _basicIntegrationClient = new IntegrationClient(IntegrationClient.EndpointConfiguration.BasicHttpBinding_IIntegration);
        _basicIntegrationClient.Endpoint.Address = new System.ServiceModel.EndpointAddress($"{_settings.Url.TrimEnd('/')}/Integration");
        _basicIntegrationClient.ClientCredentials.UserName.UserName = _settings.UserName;
        _basicIntegrationClient.ClientCredentials.UserName.Password = _settings.Password;
        
        return _basicIntegrationClient;
    }
}
