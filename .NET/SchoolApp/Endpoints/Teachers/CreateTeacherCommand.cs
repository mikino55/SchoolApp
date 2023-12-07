using MediatR;
using SchoolApp.Data;
using SchoolApp.Data.Access.Providers;

namespace SchoolApp.Endpoints.Teachers;

public class CreateTeacherCommand : IRequest<CreateTeacherResponse>
{
    public required CreateTeacherRequest Dto { get; set; }
}

public class CreateTeacherCommandHandler(IUserRegistrationProvider registrationProvider) : IRequestHandler<CreateTeacherCommand, CreateTeacherResponse>
{
    private readonly IUserRegistrationProvider registrationProvider = registrationProvider;

    public async Task<CreateTeacherResponse> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = new Teacher()
        {
            Email = request.Dto.Email,
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
        };
        await this.registrationProvider.RegisterTeacherAsync(teacher, "abc123");
        return new(teacher.Id, teacher.FirstName, teacher.LastName);
    }
}

public record CreateTeacherRequest(string Email, string FirstName, string LastName);

public record CreateTeacherResponse(string Id,string FirstName, string LastName);
