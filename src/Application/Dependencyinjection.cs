using Application.Modules;
using Application.Pipeline.Authorization;
using Application.UtilityServices;
using Domain;
using FluentValidation;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class Dependencyinjection
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJWTAuthorization(configuration)
                .AddRolePermissionAuthorization();

        services.AddDomainLayerServices()
                .AddInfrastructureLayerServices(configuration)
                .AddApplicationLayerServices();

        return services;
    }
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddModules()
                .UtilityServices();

        return services;
    }
}
