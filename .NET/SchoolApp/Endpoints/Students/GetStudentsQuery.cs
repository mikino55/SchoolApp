using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Data.Access;

namespace SchoolApp.Endpoints.Students;

public class GetStudentsQuery : IRequest<List<StudentDto>>
{
    public StudentDto Student { get; set; }
}

public record StudentDto(int Id, string FirstName, string LastName);

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentDto>>
{
    private readonly ApplicationDbContext context;

    public GetStudentsQueryHandler(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await context.Students.ToListAsync();
        return students
            .Select(s => new StudentDto(s.Id, s.FirstName, s.LastName))
            .ToList();
    }
}
