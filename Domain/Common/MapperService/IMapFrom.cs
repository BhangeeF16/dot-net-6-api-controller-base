using AutoMapper;

namespace Domain.Common.MapperService;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}