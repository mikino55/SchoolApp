using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Access;

namespace SchoolApp.Endpoints.Students;

public class GetStudentDetailsQuery : IRequest<StudentDto>
{
    public required string Id { get; set; }
}

public class GetStudentDetailsQueryHandler : IRequestHandler<GetStudentDetailsQuery, StudentDto>
{
    private readonly ApplicationDbContext context;

    public GetStudentDetailsQueryHandler(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<StudentDto> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
    {
        var student = await this.context
            .Users
            .Where(x => x.UserType == Data.UserType.Student)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id)
            ?? throw new Exception("Not found");

        return new StudentDto
        ( 
            student.Id,
            student.Email!,
            student.FirstName,
            student.LastName
        );
    }
}
