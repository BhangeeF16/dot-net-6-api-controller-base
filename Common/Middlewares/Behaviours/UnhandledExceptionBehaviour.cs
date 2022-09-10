using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.Middlewares.Behaviours;

public class UnhandledExceptionBehaviour
{
    private readonly ILogger<HttpRequest> _logger;
    private readonly RequestDelegate _next;

    public UnhandledExceptionBehaviour(ILogger<HttpRequest> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var requestName = context.Request.Path;
            _logger.LogError(ex, "Gig Panel Request: Unhandled Exception for Request {Name}", requestName);

            throw;
        }
    }
}
