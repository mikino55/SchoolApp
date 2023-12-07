using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Data.Access;

namespace SchoolApp.Endpoints.Students;

public class GetStudentsQuery : IRequest<List<StudentDto>>
{
}

public record StudentDto(string Id, string Email, string FirstName, string LastName);

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentDto>>
{
    private readonly ApplicationDbContext context;

    public GetStudentsQueryHandler(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await context.Users
            .AsNoTracking()
            .Where(x => x.UserType == UserType.Student)
            .ToListAsync();

        return students
            .Select(s => new StudentDto(s.Id, s.Email!, s.FirstName, s.LastName))
            .ToList();
    }
}
