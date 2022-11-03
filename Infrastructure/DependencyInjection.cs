using Domain.Common.DataAccessHelpers;
using Domain.IRepositories.IGenericRepositories;
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
        var connectionString = configuration.GetConnectionString("SQLSERVER_CON_STR");

        services.Add(new ServiceDescriptor(typeof(ConnectionInfo), new ConnectionInfo(connectionString)));

        services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer( connectionString ?? string.Empty, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
