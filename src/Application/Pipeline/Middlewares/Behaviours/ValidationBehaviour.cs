using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Pipeline.Middlewares.Behaviours;

public class ValidationBehaviour
{
    private readonly IEnumerable<IValidator<HttpRequest>> _validators;
    private readonly RequestDelegate _next;

    public ValidationBehaviour(IEnumerable<IValidator<HttpRequest>> validators, RequestDelegate next)
    {
        _validators = validators;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<HttpRequest>(context.Request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(validationContext)));

                var validationFailures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (validationFailures.Any())
                    throw new ValidationException(validationFailures);
            }
            await _next.Invoke(context);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
