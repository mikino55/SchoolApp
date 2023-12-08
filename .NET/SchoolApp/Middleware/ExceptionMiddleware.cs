using SchoolApp.Infrastructure;

namespace SchoolApp.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (ApiException ex)
        {
            await this.HandleExceptionAsync(context, new ApiErrorResponse(ex.Message, ex.FriendlyMessage), (int)ex.Code);
        }
        catch (Exception ex)
        {
            await this.HandleExceptionAsync(context, new ApiErrorResponse(ex.Message, ""), (int)HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, ApiErrorResponse errorDetails, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(errorDetails);
    }
}
