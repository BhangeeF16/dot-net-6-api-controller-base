﻿using AutoMapper;

namespace Domain.IServices.IUtilities;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}