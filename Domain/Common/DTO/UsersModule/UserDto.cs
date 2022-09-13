using AutoMapper;
using Domain.Common.Utilities;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IServices.IMapperServices;

namespace Domain.Common.DTO.UsersModule
{
    public class UserDto : IMapFrom<UserDto>
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(trg => trg.Role, src => src.MapFrom(dst => dst.Role))
                .ForMember(trg => trg.DOB, src => src.MapFrom(dst => dst.DOB.ToString("yyyy-MM-dd")))
                .ForMember(dst => dst.Ethnicity, src => src.MapFrom(trg => (trg.Ethnicity ?? Entities.GeneralModule.Ethnicity.AmericanIndian).GetDescription()))
                .ForMember(dst => dst.Gender, src => src.MapFrom(trg => (trg.Gender ?? Entities.GeneralModule.Gender.PreferNotToAnswer).GetDescription()))
                .ForMember(dst => dst.CommunicationPreference, src => src.MapFrom(trg => (trg.CommunicationPreference ?? Entities.GeneralModule.CommunicationPreference.Email).GetDescription()));
        }
    }
}
