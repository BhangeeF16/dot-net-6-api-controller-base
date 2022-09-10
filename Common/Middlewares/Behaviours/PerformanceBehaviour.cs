using Domain.IServices.IAuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common.Middlewares.Behaviours;

public class PerformanceBehaviour
{
    private readonly Stopwatch _timer;
    private readonly ILogger<HttpRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly RequestDelegate _next;

    public PerformanceBehaviour(ILogger<HttpRequest> logger, ICurrentUserService currentUserService, RequestDelegate next)
    {
        _timer = new Stopwatch();
        _currentUserService = currentUserService;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            _timer.Start();

            await _next.Invoke(context);

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = context.Request.Path;
                var requestedPath = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path} {context.Request.QueryString}".Trim();
                var userName = "Swagger";

                if (!string.IsNullOrEmpty(_currentUserService.UserName))
                {
                    userName = _currentUserService.UserName;
                }

                _logger.LogWarning("Gig Panel Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@RequestedPath} {@UserName}",
                    requestName, elapsedMilliseconds, requestedPath, userName);
            }
        }
        catch (Exception)
        {

            throw;
        }

        await Task.FromResult(Task.CompletedTask);
        return;
    }
}
