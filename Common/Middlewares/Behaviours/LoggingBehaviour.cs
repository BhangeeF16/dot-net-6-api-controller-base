using Domain.IServices.IAuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.Middlewares.Behaviours;

public class LoggingBehaviour
{
    private readonly ILogger<HttpRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly RequestDelegate _next;
    public LoggingBehaviour(ILogger<HttpRequest> logger, ICurrentUserService currentUserService, RequestDelegate next)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestName = context.Request.Path;
        var requestedPath = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path} {context.Request.QueryString}".Trim();
        var userName = "Swagger";

        if (!string.IsNullOrEmpty(_currentUserService.UserName))
        {
            userName = _currentUserService.UserName;
        }

        _logger.LogInformation("GigPanel Request: {Name} {@RequestedPath} {@UserName}",
            requestName, requestedPath, userName);

        await _next.Invoke(context);

        await Task.FromResult(Task.CompletedTask);

        return;
    }
}