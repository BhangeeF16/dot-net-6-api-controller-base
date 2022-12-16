using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection UseSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Job-Finder.API",
                    Description = $"{(env.IsDevelopment() ? "Development" : "Production")} Environment",
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (JWT).",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                c.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.EnableAnnotations();
            });

            //services.AddFluentValidationRulesToSwagger();

            return services;
        }
    }
}
