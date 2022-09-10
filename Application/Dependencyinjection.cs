using Application.EntityServices;
using Common;
using Domain;
using Domain.IServices.IEntityServices;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Dependencyinjection
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainLayerServices(configuration);
        services.AddCommonLayerServices(configuration);
        services.AddInfrastructureLayerServices(configuration);
        services.AddApplicationLayerServices();

        return services;
    }
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAppSettingService, AppsSettingService>();

        return services;
    }
}
