using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            return students;
        });

        group.MapPost("", async ([FromServices] ISender sender, [FromBody] CreateStudentDto dto) =>
        {
            var student = await sender.Send(new CreateStudentCommand
            {
                Student = dto
            });

            return student;
        });

        return group;
    }
}
