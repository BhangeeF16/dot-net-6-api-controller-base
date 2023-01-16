using AutoMapper;

namespace Domain.IServices.IHelperServices;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}