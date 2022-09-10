using Common.Middlewares.Behaviours;
using Common.Middlewares.Logging;
using Microsoft.AspNetCore.Builder;

namespace Common.Middlewares;

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
