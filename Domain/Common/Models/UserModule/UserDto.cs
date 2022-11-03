using AutoMapper;
using Domain.Common.MapperService;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;

namespace Domain.Common.Models.UserModule
{
    public class UserDto : IMapFrom<UserDto>
    {
        public int ID { get; set; }
        public string? UserName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public Ethnicity? Ethnicity { get; set; }
        public CommunicationPreference? CommunicationPreference { get; set; }
        public string? ImageKey { get; set; }
        public bool? IsOnBoarded { get; set; } = false;
        public bool IsPasswordChanged { get; set; } = false;
        public int fk_RoleID { get; set; }

        public RoleDto? Role { get; set; }
        public virtual List<UsersStripeDetailDto>? StripeDetails { get; set; }
        public virtual List<UsersShippingAddressDto>? ShippingAddresses { get; set; }
        public virtual UserCorporateProfileDto? CorporateProfile { get; set; }
        public virtual UserCandidateProfileDto? CandidateProfile { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
            profile.CreateMap<UserDto, User>();
        }
    }
}
