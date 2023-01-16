using Domain.IServices.IUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UtilityServices;

public static class Dependencyinjection
{
    public static IServiceCollection UtilityServices(this IServiceCollection services)
    {
        services.AddTransient<IFileUploadService, FileUploadService.FileUploadService>();

        return services;
    }
}
