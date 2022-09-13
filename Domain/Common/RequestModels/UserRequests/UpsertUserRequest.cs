using AutoMapper;
using Domain.Entities.UsersModule;
using Domain.IServices.IMapperServices;

namespace Domain.Common.RequestModels.UserRequests
{
    public class UpsertUserRequest : IMapFrom<UpsertUserRequest>
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public int fk_RoleID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UpsertUserRequest>();
        }
    }
}
