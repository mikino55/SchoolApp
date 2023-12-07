using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Endpoints.Teachers;
using SchoolApp.Endpoints.Users;

namespace SchoolApp.EndpointMapping;

public static class TeacherEndpoints
{
    public static IEndpointConventionBuilder MapTecherEndpoints(this WebApplication endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapGroup("api/teacher");

        group.MapPost("", async ([FromServices] ISender sender, [FromBody] CreateTeacherRequest request) =>
        {
            var student = await sender.Send(new CreateTeacherCommand
            {
                Dto = request,
            });

            return TypedResults.Ok(student);
        });

        return group;
    }
}
