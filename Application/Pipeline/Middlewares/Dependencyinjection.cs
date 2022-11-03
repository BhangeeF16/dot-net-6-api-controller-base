using Application.Pipeline.Middlewares.Behaviours;
using Application.Pipeline.Middlewares.Logging;
using Microsoft.AspNetCore.Builder;

namespace Application.Pipeline.Middlewares;

public static class Dependencyinjection
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggingBehaviour>()
           .UseMiddleware<RequestResponseLogging>()
           .UseMiddleware<UnhandledExceptionBehaviour>()
           .UseMiddleware<PerformanceBehaviour>()
           .UseMiddleware<ValidationBehaviour>();

        return app;
    }
}
