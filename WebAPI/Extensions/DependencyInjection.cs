using Application.Middlewares;

namespace WebAPI.Extensions;

public static class DependencyInjection
{
    public static IApplicationBuilder AddMiddleWares(this IApplicationBuilder app)
    {
        app.UseBehaviouralMiddleware();
        app.UseLoggingMiddleware();

        return app;
    }
}
