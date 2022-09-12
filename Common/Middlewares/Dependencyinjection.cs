using Application.Middlewares.Behaviours;
using Application.Middlewares.Logging;
using Microsoft.AspNetCore.Builder;

namespace Application.Middlewares;

public static class Dependencyinjection
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestResponseLogging>();
        return app;
    }
    public static IApplicationBuilder UseBehaviouralMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnhandledExceptionBehaviour>();
        app.UseMiddleware<LoggingBehaviour>();
        app.UseMiddleware<PerformanceBehaviour>();
        app.UseMiddleware<ValidationBehaviour>();

        return app;
    }
}
