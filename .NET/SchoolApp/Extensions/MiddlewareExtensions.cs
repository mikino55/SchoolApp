using SchoolApp.Middleware;

namespace SchoolApp.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIdentity(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserIdentityMiddleware>();
    }
}
