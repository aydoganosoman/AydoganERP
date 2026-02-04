using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Identity.Application.Models;
using System.Reflection;

namespace AydoganERP.Identity.Application.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        
        CreateMap<PaginatedList<Base.Domain.Modules.IdentityModule.Entities.User>, PaginatedList<UserDto>>()
            .ForCtorParam("items", o => o.MapFrom(k => k.Items))
            .ForCtorParam("count", o => o.MapFrom(k => k.TotalPages * k.PageSize))
            .ForCtorParam("currentPage", o => o.MapFrom(k => k.CurrentPage))
            .ForCtorParam("pageSize", o => o.MapFrom(k => k.PageSize))
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
            .ReverseMap();
    }
    
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                             ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });

        }
    }
}