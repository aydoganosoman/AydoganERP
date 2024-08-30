using AutoMapper;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using System.Reflection;

namespace AydoganERP.Base.Application.Mappings;

public class BaseMappingProfile : Profile
{
    public BaseMappingProfile()
    {
        CreateMap<Inbox, InboxDto>().ReverseMap();
        CreateMap<Outbox, OutboxDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();

        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
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