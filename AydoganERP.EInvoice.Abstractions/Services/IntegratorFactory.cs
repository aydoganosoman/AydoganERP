using AydoganERP.EInvoice.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;

namespace AydoganERP.EInvoice.Abstractions.Services;

/// <summary>
/// E-Fatura entegratör factory.
/// Provider tipine göre uygun entegratör instance'ı oluşturur.
/// </summary>
public class IntegratorFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Entegratör tipleri
    /// </summary>
    public static class IntegratorType
    {
        public const int MySoft = 1;
        public const int Bien = 2;
    }

    public IntegratorFactory(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
    {
        _loggerFactory = loggerFactory;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Entegratör tipine göre instance oluşturur.
    /// </summary>
    /// <param name="integratorType">Entegratör tipi (1=MySoft, 2=Bien)</param>
    /// <param name="settingsJson">Ayarlar JSON string</param>
    /// <returns>IEInvoiceIntegrator instance</returns>
    public IEInvoiceIntegrator Create(int integratorType, string settingsJson)
    {
        return integratorType switch
        {
            IntegratorType.MySoft => CreateMySoftIntegrator(settingsJson),
            IntegratorType.Bien => CreateBienIntegrator(settingsJson),
            _ => throw new NotSupportedException($"Entegratör tipi desteklenmiyor: {integratorType}")
        };
    }

    private IEInvoiceIntegrator CreateMySoftIntegrator(string settingsJson)
    {
        // Runtime'da MySoft assembly'si yüklenecek
        var assemblyName = "AydoganERP.EInvoice.MySoft";
        var typeName = $"{assemblyName}.MySoftIntegrator";

        var assembly = System.Reflection.Assembly.Load(assemblyName);
        var type = assembly.GetType(typeName)
            ?? throw new InvalidOperationException($"Tip bulunamadı: {typeName}");

        return (IEInvoiceIntegrator)Activator.CreateInstance(type, settingsJson, _loggerFactory)!;
    }

    private IEInvoiceIntegrator CreateBienIntegrator(string settingsJson)
    {
        // Runtime'da Bien assembly'si yüklenecek
        var assemblyName = "AydoganERP.EInvoice.Bien";
        var typeName = $"{assemblyName}.BienIntegrator";

        var assembly = System.Reflection.Assembly.Load(assemblyName);
        var type = assembly.GetType(typeName)
            ?? throw new InvalidOperationException($"Tip bulunamadı: {typeName}");

        return (IEInvoiceIntegrator)Activator.CreateInstance(type, settingsJson, _loggerFactory)!;
    }
}
