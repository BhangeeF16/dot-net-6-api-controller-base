using Application.Modules.CandidateModule;
using Application.Modules.GeneralModule;
using Application.Modules.JobPostModule;
using Application.Modules.UserModule;
using Domain.IServices.IEntityServices.ICandidateModule;
using Domain.IServices.IEntityServices.IGenralModule;
using Domain.IServices.IEntityServices.IJobPostModule;
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
                    .AddScoped<ICandidateService, CandidateService>()
                    .AddScoped<IPostService, PostService>()
                    .AddScoped<IJobService, JobService>()
                    .AddScoped<IAppSettingService, AppSettingService>();

            return services;
        }
    }
}
