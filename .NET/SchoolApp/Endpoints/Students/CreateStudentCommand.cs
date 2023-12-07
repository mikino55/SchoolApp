using MediatR;
using SchoolApp.Data;
using SchoolApp.Data.Access;
using SchoolApp.Data.Access.Providers;

namespace SchoolApp.Endpoints.Students;

public class CreateStudentCommand : IRequest<StudentDto>
{
    public required CreateStudentRequest Student { get; set; }
}

public record CreateStudentRequest(string Email, string FirstName, string LastName);

public class CreateStudentCommandHandler(IUserRegistrationProvider registrationProvider) : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly IUserRegistrationProvider registrationProvider = registrationProvider;

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.Student.FirstName,
            LastName = request.Student.LastName,
            Email = request.Student.Email,
        };

        await this.registrationProvider.RegisterStudentAsync(student, "abc123");
        return new StudentDto(student.Id, student.Email, student.FirstName, student.LastName);
    }
}
