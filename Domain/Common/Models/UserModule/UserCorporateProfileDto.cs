using AutoMapper;
using Domain.Common.MapperService;
using Domain.Common.Models.CorporateModule;
using Domain.Entities.UsersModule;

namespace Domain.Common.Models.UserModule
{
    public class UserCorporateProfileDto : IMapFrom<UserCorporateProfileDto>
    {
        public int ID { get; set; }
        public int fk_CorporateID { get; set; }
        public int fk_UserID { get; set; }


        public virtual CorporateDto? Corporate { get; set; }
        public virtual List<CorporateJobDto>? PostedJobs { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserCorporateProfile, UserCorporateProfileDto>();
            profile.CreateMap<UserCorporateProfileDto, UserCorporateProfile>();
        }
    }
}
