using AutoMapper;
using AydoganERP.Base.Application.Common.Models.Dtos;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Application.Common.Mappings;

public class SharedMappingProfile : Profile
{
    public SharedMappingProfile()
    {
        CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.UsageAreaNames, 
                opt => opt.MapFrom(src => UsageAreaEnumExtensions.GetNames(src.UsageAreas)));

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.GroupName, 
                opt => opt.MapFrom(src => src.Group != null ? src.Group.Name : null))
            .ForMember(dest => dest.ProcessTypeNames, 
                opt => opt.MapFrom(src => ProcessTypeEnumExtensions.GetNames(src.ProcessTypes)));

        CreateMap<TagGroup, TagGroupDto>();

        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.TagGroupName, 
                opt => opt.MapFrom(src => src.TagGroup != null ? src.TagGroup.Name : null));

        CreateMap<Folder, FolderDto>()
            .ForMember(dest => dest.DocumentTypeNames, 
                opt => opt.MapFrom(src => DocumentTypeEnumExtensions.GetNames(src.DocumentTypes)));
    }
}
