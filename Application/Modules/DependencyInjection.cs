using Application.Modules.GeneralModule;
using Application.Modules.UserModule;
using Domain.IServices.IEntityServices.IGeneralModule;
using Domain.IServices.IEntityServices.IUserModule;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Modules
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>()
                    .AddScoped<IRoleService, RoleService>()
                    .AddScoped<IAppSettingService, AppSettingService>();

            return services;
        }
    }
}
