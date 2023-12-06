using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Endpoints.Users;

namespace SchoolApp.EndpointMapping;

public static class UserEndpoints
{
    public static IEndpointConventionBuilder MapUserEndpoints(this WebApplication endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapGroup("api/user");

        group.MapPost("/signin", async ([FromServices] ISender sender, [FromBody] SignInRequest request) =>
        {
            var student = await sender.Send(new SignInCommand
            {
                Request = request,
            });

            return TypedResults.Ok(student);
        });

        return group;
    }
}
