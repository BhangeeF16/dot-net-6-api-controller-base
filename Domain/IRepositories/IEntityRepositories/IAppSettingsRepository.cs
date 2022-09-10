using Domain.Entities.GeneralModule;
using Domain.IRepositories.IGenericRepositories;

namespace Domain.IRepositories.IEntityRepositories;

public interface IAppSettingsRepository : IGenericRepository<AppSetting>
{
    Task<string> UpsertAppSettings(AppSetting appSetting);
    // Task AddAsync(GigPanel.Common.Common.DTO.AppSettingDto model);
}
