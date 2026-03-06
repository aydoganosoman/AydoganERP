using AutoMapper;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using AydoganERP.Company.Application.Models;

namespace AydoganERP.Company.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Base.Domain.Modules.CompanyModule.Entities.Company, CompanyDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => CompanyStatusEnum.GetName(src.Status)))
            .ForMember(dest => dest.CompanyTypeName, opt => opt.MapFrom(src => CompanyTypeEnum.GetName(src.CompanyType)))
            // TaxInfo mapping
            .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxInfo.TaxNumber))
            .ForMember(dest => dest.TaxOffice, opt => opt.MapFrom(src => src.TaxInfo.TaxOffice))
            // ContactInfo mapping
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contact.Phone))
            .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Contact.Fax))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contact.Email))
            .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Contact.Website))
            // Address mapping
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Address != null ? src.Address.Country : null))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null))
            .ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.Address != null ? src.Address.District : null))
            .ForMember(dest => dest.AddressLine, opt => opt.MapFrom(src => src.Address != null ? src.Address.Line : null));

        CreateMap<DocumentNumbering, DocumentNumberingDto>()
            .ForMember(dest => dest.DocumentTypeName, opt => opt.MapFrom(src => DocumentTypeEnum.GetName(src.DocumentType)));

        CreateMap<CompanyBankAccount, CompanyBankAccountDto>()
            .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => CurrencyEnum.GetName(src.Currency)));

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
