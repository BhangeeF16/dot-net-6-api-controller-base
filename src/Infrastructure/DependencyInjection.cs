using Domain.IRepositories.IGenericRepositories;
using Domain.Models.GeneralModels;
using Infrastructure.DataAccess.GenericRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Add(new ServiceDescriptor(typeof(ConnectionInfo), new ConnectionInfo(configuration.GetConnectionString("SQLSERVER_CON_STR"))));
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
