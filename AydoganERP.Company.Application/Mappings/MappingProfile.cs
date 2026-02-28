using AutoMapper;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using AydoganERP.Company.Application.Models;

namespace AydoganERP.Company.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Base.Domain.Modules.CompanyModule.Entities.Company, CompanyDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => CompanyStatusEnum.GetName(src.Status)));

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
