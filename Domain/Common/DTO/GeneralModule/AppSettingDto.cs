using AutoMapper;
using Domain.Entities.GeneralModule;

#nullable disable

namespace Domain.Common.DTO.GeneralModule
{
    public partial class AppSettingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppSettingDto, AppSetting>();
            profile.CreateMap<AppSetting, AppSettingDto>();
        }

    }
}
