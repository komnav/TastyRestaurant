using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CQRS.Query;

public sealed class GetAllTablesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetAllTablesQuery, List<Table>>
{
    public async Task<List<Table>> Handle(GetAllTablesQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Tables.ToListAsync(cancellationToken);
    }
}