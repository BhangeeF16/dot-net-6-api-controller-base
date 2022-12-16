using Domain.Models.GeneralModels;

namespace Domain.IServices.IEntityServices.IGeneralModule
{
    public interface IAppSettingService
    {
        Task<List<AppSettingModel>> ListRequestAsync();
        Task<AppSettingModel> AddRequestAsync(AppSettingModel model);
        Task<AppSettingModel> UpdateRequestAsync(AppSettingModel model);

        Task<SmtpCredentialModel> GetSmtpCredentials();
        Task<SmtpCredentialModel> UpsertRequestAsync(SmtpCredentialModel request);
    }
}
