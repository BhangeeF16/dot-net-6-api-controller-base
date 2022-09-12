using Application.EntityServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.IServices.IEntityServices;
using Domain.IServices.IHelperServices;
using Application.HelperServices;
using Application.Authorization;
using Infrastructure;
using Domain;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainLayerServices(configuration);
        services.AddInfrastructureLayerServices(configuration);
        services.AddApplicationLayerServices(configuration);

        return services;
    }
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.UseJwtTokenAuthorization(configuration);
        services.UsePermissionToRolePolicyAuthorization();

        services.AddCommonHelperServices();

        return services;
    }
    public static IServiceCollection AddCommonHelperServices(this IServiceCollection services)
    {
        services.AddSingleton<IUploadImageService, UploadImageService>();

        return services;
    }
    public static IServiceCollection AddEntityServicesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAppSettingService, AppsSettingService>();

        return services;
    }

}
