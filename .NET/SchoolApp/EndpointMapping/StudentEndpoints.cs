using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Data;
using SchoolApp.Endpoints.Students;

namespace SchoolApp.EndpointMapping;

public static class StudentEndpoints
{
    public static IEndpointConventionBuilder MapStudentEndpoints(this WebApplication endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapGroup("api/student");

        group.MapGet("", async ([FromServices] ISender sender) => 
        {
            var students = await sender.Send(new GetStudentsQuery());
            return TypedResults.Ok(students);
        }).RequireAuthorization();

        group.MapGet("/{id:int}", async ([FromServices] ISender sender, [FromRoute] int id) =>
        {
            var student = await sender.Send(new GetStudentDetailsQuery() { Id = id});
            return TypedResults.Ok(student);
        });

        group.MapPost("", async ([FromServices] ISender sender, [FromBody] CreateStudentDto dto) =>
        {
            var student = await sender.Send(new CreateStudentCommand
            {
                Student = dto
            });

            return TypedResults.Ok(student);
        });

        group.MapDelete("/{id:int}", async ([FromServices] ISender sender, [FromRoute] int id) =>
        {
            await sender.Send(new DeleteStudentCommand
            {
                Id = id
            });

            return TypedResults.NoContent();
        });

        return group;
    }
}
