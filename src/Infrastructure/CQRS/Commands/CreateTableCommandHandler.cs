using Domain.Entities;
using MediatR;

namespace Infrastructure.CQRS.Commands;

public class CreateTableCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateTableCommand, Table>
{
    public async Task<Table> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = new Table
        {
            Number = request.Number,
            Capacity = request.Capacity,
            Type = request.Type
        };
        context.Add(table);
        await context.SaveChangesAsync(cancellationToken);
        return table;
    }
}