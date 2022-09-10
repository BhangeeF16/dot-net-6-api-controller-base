using Common.Authorization;
using Common.HelperServices;
using Domain.IServices.IHelperServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonLayerServices(this IServiceCollection services, IConfiguration configuration)
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

}
