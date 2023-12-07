namespace SchoolApp.Middleware;

public class UserIdentityMiddleware
{
    private readonly RequestDelegate next;

    public UserIdentityMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        bool user = context.User.Identity!.IsAuthenticated;

        await next(context);
    }
}
