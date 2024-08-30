﻿using AutoMapper;

namespace AydoganERP.Base.Application.Mappings;

public interface IMapFrom<T>
{   
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}

public interface IMapTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
