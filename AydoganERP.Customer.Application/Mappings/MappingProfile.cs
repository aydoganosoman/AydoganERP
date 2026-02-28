using AutoMapper;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Customer.Application.Models;

namespace AydoganERP.Customer.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Base.Domain.Modules.CustomerModule.Entities.Customer, CustomerDto>();
        CreateMap<CustomerBankAccount, CustomerBankAccountDto>();
        CreateMap<CustomerBranch, CustomerBranchDto>();
        CreateMap<CustomerContact, CustomerContactDto>();
        CreateMap<CustomerNote, CustomerNoteDto>();
        CreateMap<CustomerNumber, CustomerNumberDto>();

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
