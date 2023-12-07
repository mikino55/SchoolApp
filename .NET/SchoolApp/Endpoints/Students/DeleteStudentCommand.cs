﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Access;

namespace SchoolApp.Endpoints.Students;

public class DeleteStudentCommand : IRequest
{
    public required string Id { get; set; }
}

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly ApplicationDbContext context;

    public DeleteStudentCommandHandler(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await this.context.Users
            .Where(s => s.Id == request.Id.ToString())
            .ExecuteDeleteAsync(cancellationToken);

        if (rowsAffected == 0) 
        {
            // TODO: throw Api Exception
            throw new Exception("Not found");
        }
    }
}
