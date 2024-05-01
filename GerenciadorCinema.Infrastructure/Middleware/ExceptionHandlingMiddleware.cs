using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace GerenciadorCinema.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        response.StatusCode = exception switch
        {
            ArgumentNullException => (int)HttpStatusCode.BadRequest,
            ArgumentException => (int)HttpStatusCode.BadRequest, //400
            _ => (int)HttpStatusCode.InternalServerError // 500
        };

        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            stackTrace = SimplifyStackTrace(exception)
        });

        await response.WriteAsync(result);
    }
    
    private static string SimplifyStackTrace(Exception exception)
    {
        return string.Join("\n", exception.StackTrace
            .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.Contains("GerenciadorCinema"))
            .Select(line => line.Trim()));
    }
}
