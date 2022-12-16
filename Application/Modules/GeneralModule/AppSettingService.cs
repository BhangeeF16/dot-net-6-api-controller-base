using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Entities.GeneralModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IGeneralModule;
using Domain.Models.GeneralModels;

namespace Application.Modules.GeneralModule
{
    public class AppSettingService : IAppSettingService
    {
        #region Constructors and Locals

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public AppSettingService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        #endregion
        public async Task<List<AppSettingModel>> ListRequestAsync()
        {
            try
            {
                var appSettings = await _unitOfWork.AppsettingsRepository.GetAllAsync();
                return _mapper.Map<List<AppSettingModel>>(appSettings);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AppSettingModel> AddRequestAsync(AppSettingModel model)
        {
            var thisUser = _mapper.Map<AppSetting>(model);
            await _unitOfWork.AppsettingsRepository.AddAsync(thisUser);
            _unitOfWork.Complete();
            return _mapper.Map<AppSettingModel>(thisUser);
        }
        public async Task<AppSettingModel> UpdateRequestAsync(AppSettingModel model)
        {
            var thisUserAppSettings = await _unitOfWork.AppsettingsRepository.GetFirstOrDefaultAsync(x => x.Id == model.Id);
            if (thisUserAppSettings == null)
            {
                throw new ClientException("Not Found", System.Net.HttpStatusCode.NotFound);
            }
            else
            {
                thisUserAppSettings.Label = model.Label;
                thisUserAppSettings.Value = model.Value;
                thisUserAppSettings.Name = model.Name;
                thisUserAppSettings.Description = model.Description;
                _unitOfWork.Complete();
            }
            return await Task.FromResult(_mapper.Map<AppSettingModel>(thisUserAppSettings));
        }


        public async Task<SmtpCredentialModel> GetSmtpCredentials()
        {
            try
            {
                var appSettings = await _unitOfWork.AppsettingsRepository.GetAllAsync();
                return new SmtpCredentialModel()
                {
                    FromMail = appSettings.FirstOrDefault(c => c.Name == "FromMail")?.Value ?? string.Empty,
                    SmtpClient = appSettings.FirstOrDefault(c => c.Name == "SmtpClient")?.Value ?? string.Empty,
                    SmtpPort = appSettings.FirstOrDefault(c => c.Name == "SmtpPort")?.Value ?? string.Empty,
                    SmtpUser = appSettings.FirstOrDefault(c => c.Name == "SmtpUser")?.Value ?? string.Empty,
                    SmtpPassword = appSettings.FirstOrDefault(c => c.Name == "SmtpPassword")?.Value ?? string.Empty,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<SmtpCredentialModel> UpsertRequestAsync(SmtpCredentialModel request)
        {
            var appSettings = await _unitOfWork.AppsettingsRepository.GetAllAsync();
            if (!appSettings.Any())
            {
                var appsettings = new List<AppSetting>();
                foreach (var propertyInfo in request.GetType().GetProperties())
                {
                    appsettings.Add(new AppSetting()
                    {
                        Name = propertyInfo.Name,
                        Label = propertyInfo.Name,
                        Value = (propertyInfo.GetValue(request, null) ?? string.Empty).ToString(),
                    });
                }
                await _unitOfWork.AppsettingsRepository.AddRangeAsync(appsettings);
            }
            else
            {
                foreach (var item in appSettings)
                {
                    if (item.Name == "FromMail")
                    {
                        item.Value = request.FromMail;
                    }
                    if (item.Name == "SmtpClient")
                    {
                        item.Value = request.SmtpClient;
                    }
                    if (item.Name == "SmtpPort")
                    {
                        item.Value = request.SmtpPort;
                    }
                    if (item.Name == "SmtpUser")
                    {
                        item.Value = request.SmtpUser;
                    }
                    if (item.Name == "SmtpPassword")
                    {
                        item.Value = request.SmtpPassword;
                    }
                }
            }
            _unitOfWork.Complete();
            return request;
        }
    }
}
