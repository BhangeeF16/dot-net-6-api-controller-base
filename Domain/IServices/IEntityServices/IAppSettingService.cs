using Domain.Common.DTO;
using Domain.Common.RequestModels;

namespace Domain.IServices.IEntityServices
{
    public interface IAppSettingService
    {
        Task<List<AppSettingDto>> ListRequestAsync();
        Task<AppSettingDto> AddRequestAsync(AppSettingDto model);
        Task<AppSettingDto> UpdateRequestAsync(AppSettingDto model);

        Task<SmtpCredentialModel> GetSmtpCredentials();
        Task<SmtpCredentialModel> UpsertRequestAsync(SmtpCredentialModel request);
    }
}
