using AutoMapper;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Finance.Application.Models;

namespace AydoganERP.Finance.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(d => d.InvoiceTypeName, opt => opt.MapFrom(s => GetInvoiceTypeName(s.InvoiceType)))
            .ForMember(d => d.StatusName, opt => opt.MapFrom(s => GetStatusName(s.Status)))
            .ForMember(d => d.CustomerCode, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.Code : ""))
            .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.CustomerName : ""))
            .ForMember(d => d.PaidAmount, opt => opt.MapFrom(s => s.GetPaidAmount()))
            .ForMember(d => d.RemainingAmount, opt => opt.MapFrom(s => s.GetRemainingAmount()))
            .ForMember(d => d.IsPaid, opt => opt.MapFrom(s => s.IsPaid()));

        CreateMap<Invoice, InvoiceListDto>()
            .ForMember(d => d.InvoiceTypeName, opt => opt.MapFrom(s => GetInvoiceTypeName(s.InvoiceType)))
            .ForMember(d => d.StatusName, opt => opt.MapFrom(s => GetStatusName(s.Status)))
            .ForMember(d => d.CustomerCode, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.Code : ""))
            .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.CustomerName : ""))
            .ForMember(d => d.PaidAmount, opt => opt.MapFrom(s => s.GetPaidAmount()))
            .ForMember(d => d.RemainingAmount, opt => opt.MapFrom(s => s.GetRemainingAmount()))
            .ForMember(d => d.IsPaid, opt => opt.MapFrom(s => s.IsPaid()));

        CreateMap<InvoiceLine, InvoiceLineDto>()
            .ForMember(d => d.SerialNumber, opt => opt.MapFrom(s => s.SerialNumber != null ? s.SerialNumber.SerialNumber : null));

        CreateMap<InvoicePayment, InvoicePaymentDto>()
            .ForMember(d => d.PaymentMethodName, opt => opt.MapFrom(s => GetPaymentMethodName(s.PaymentMethod)));

        // Yeni entity mapping'leri
        CreateMap<InvoiceNote, InvoiceNoteDto>();

        CreateMap<InvoicePaymentTerm, InvoicePaymentTermDto>()
            .ForMember(d => d.PaymentMethodName, opt => opt.MapFrom(s => GetPaymentMethodName(s.PaymentMethod)));

        CreateMap<InvoiceOrderInfo, InvoiceOrderInfoDto>();

        CreateMap<InvoicePartyNumber, InvoicePartyNumberDto>()
            .ForMember(d => d.NumberTypeName, opt => opt.MapFrom(s => GetPartyNumberTypeName(s.NumberType)));

        CreateMap<InvoiceOkcInfo, InvoiceOkcInfoDto>()
            .ForMember(d => d.FisTypeName, opt => opt.MapFrom(s => GetOkcFisTypeName(s.FisType)));

        CreateMap<Document, DocumentDto>();
    }

    private static string GetInvoiceTypeName(int type) => type switch
    {
        InvoiceTypeEnum.SalesInvoice => "Satış Faturası",
        InvoiceTypeEnum.PurchaseInvoice => "Alış Faturası",
        InvoiceTypeEnum.SalesReturn => "Satış İade",
        InvoiceTypeEnum.PurchaseReturn => "Alış İade",
        _ => "Bilinmiyor"
    };

    private static string GetStatusName(int status) => status switch
    {
        InvoiceStatusEnum.Draft => "Taslak",
        InvoiceStatusEnum.Approved => "Onaylandı",
        InvoiceStatusEnum.Cancelled => "İptal",
        InvoiceStatusEnum.EInvoiceSent => "E-Fatura Gönderildi",
        InvoiceStatusEnum.EInvoiceAccepted => "E-Fatura Kabul",
        InvoiceStatusEnum.EInvoiceRejected => "E-Fatura Red",
        _ => "Bilinmiyor"
    };

    private static string GetPaymentMethodName(int method) => method switch
    {
        PaymentMethodEnum.Cash => "Nakit",
        PaymentMethodEnum.BankTransfer => "Havale/EFT",
        PaymentMethodEnum.CreditCard => "Kredi Kartı",
        PaymentMethodEnum.Check => "Çek",
        PaymentMethodEnum.Other => "Diğer",
        _ => "Bilinmiyor"
    };

    private static string GetPartyNumberTypeName(int type) => type switch
    {
        PartyNumberTypeEnum.SubscriberNo => "Abone No",
        PartyNumberTypeEnum.DealerNo => "Bayi No",
        PartyNumberTypeEnum.FarmerNo => "Çiftçi No",
        PartyNumberTypeEnum.TaxNo => "VKN",
        PartyNumberTypeEnum.IdNo => "TCKN",
        PartyNumberTypeEnum.EpdkNo => "EPDK No",
        _ => "Bilinmiyor"
    };

    private static string GetOkcFisTypeName(int? type) => type switch
    {
        OkcFisTypeEnum.Sales => "Satış Fişi",
        OkcFisTypeEnum.Return => "İade Fişi",
        _ => ""
    };

    public static string GetEInvoiceScenarioName(int scenario) => scenario switch
    {
        EInvoiceScenarioEnum.Basic => "Temel Fatura",
        EInvoiceScenarioEnum.Commercial => "Ticari Fatura",
        EInvoiceScenarioEnum.Export => "İhracat Faturası",
        EInvoiceScenarioEnum.Public => "Kamu Faturası",
        _ => "Belirsiz"
    };
}
