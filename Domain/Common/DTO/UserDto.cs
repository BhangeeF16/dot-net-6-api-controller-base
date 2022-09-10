using AutoMapper;
using Domain.Common.Extensions;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IServices.IMapperServices;
using FluentValidation;

namespace Domain.Common.DTO;


public class UserDto : IMapFrom<UserDto>
{
    public int Id { get; set; }
    public string? GoogleUserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public DateTime DOB { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public Ethnicity? Ethnicity { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    public bool IsConnectedToGoogle { get; set; } = false;
    public string? Gmail { get; set; } = string.Empty;

    public int fk_RoleId { get; set; }
    public string? RoleName { get; set; }

    public int fk_CompanyId { get; set; }
    public string? CompanyName { get; set; }

    public RoleDto? Role { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserDto, User>();
        profile.CreateMap<User, UserDto>();
    }
    public class ArtistDtoValidation : AbstractValidator<UserDto>
    {
        public ArtistDtoValidation()
        {
            RuleFor(c => c.FirstName).ValidateProperty();
            RuleFor(c => c.LastName).ValidateProperty();
            RuleFor(c => c.Email).ValidateProperty();
            RuleFor(c => c.UserName).ValidateProperty();
            RuleFor(c => c.Password).ValidateProperty();
            RuleFor(c => c.DOB).ValidateProperty();
            RuleFor(c => c.PhoneNumber).ValidateProperty();
            RuleFor(c => c.Gender).ValidateProperty();
            RuleFor(c => c.Ethnicity).ValidateProperty();
            RuleFor(c => c.Address).ValidateProperty();
            RuleFor(c => c.Image).NotNull();
        }
    }
}