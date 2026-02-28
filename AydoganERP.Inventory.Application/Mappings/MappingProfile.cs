using AutoMapper;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Enums;
using AydoganERP.Inventory.Application.Models;

namespace AydoganERP.Inventory.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Name : null))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
            .ForMember(dest => dest.Barcodes, opt => opt.MapFrom(src => src.ProductBarcodes))
            .ForMember(dest => dest.Suppliers, opt => opt.MapFrom(src => src.ProductSuppliers))
            .ForMember(dest => dest.SerialNumbers, opt => opt.MapFrom(src => src.SerialNumbers))
            // Entity'deki yazım hatası (VatInculde) için explicit mapping
            .ForMember(dest => dest.PurchaseUnitPriceVatInclude, opt => opt.MapFrom(src => src.PurchaseUnitPriceVatInculde))
            .ForMember(dest => dest.SaleUnitPriceVatInclude, opt => opt.MapFrom(src => src.SaleUnitPriceVatInculde));

        CreateMap<ProductBarcode, ProductBarcodeDto>();

        CreateMap<ProductSupplier, ProductSupplierDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CustomerName : null));

        CreateMap<ProductSerialNumber, ProductSerialNumberDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.PurchaseCustomerName, opt => opt.MapFrom(src => src.PurchaseCustomer != null ? src.PurchaseCustomer.CustomerName : null))
            .ForMember(dest => dest.SaleCustomerName, opt => opt.MapFrom(src => src.SaleCustomer != null ? src.SaleCustomer.CustomerName : null));

        CreateMap<StockMovement, StockMovementDto>()
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product != null ? src.Product.Code : null))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => StockMovementTypeEnum.GetName(src.Type)));

        CreateMap<StockBatch, StockBatchDto>();

        CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
            .ConvertUsing(typeof(PaginatedListConverter<,>));
    }
}

public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
{
    public PaginatedList<TDestination> Convert(
        PaginatedList<TSource> source,
        PaginatedList<TDestination> destination,
        ResolutionContext context)
    {
        var items = context.Mapper.Map<List<TDestination>>(source.Items);
        return new PaginatedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}
