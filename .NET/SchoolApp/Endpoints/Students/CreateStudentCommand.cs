using MediatR;
using SchoolApp.Data;
using SchoolApp.Data.Access;

namespace SchoolApp.Endpoints.Students;

public class CreateStudentCommand : IRequest<StudentDto>
{
    public CreateStudentDto Student { get; set; }
}

public record CreateStudentDto(string FirstName, string LastName);

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly ApplicationDbContext context;

    public CreateStudentCommandHandler(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.Student.FirstName,
            LastName = request.Student.LastName,
        };

        this.context.Students.Add(student);
        await this.context.SaveChangesAsync();
        return new StudentDto(student.Id, student.FirstName, student.LastName);
    }
}
