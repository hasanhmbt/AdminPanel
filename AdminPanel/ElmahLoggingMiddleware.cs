using ElmahCore;
public class ElmahLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ElmahLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception using ElmahCore
            context.RaiseError(ex);

            throw;
        }
    }
}
