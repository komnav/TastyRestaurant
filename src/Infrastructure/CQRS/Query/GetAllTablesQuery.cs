using Application.Dtos.Table.Responses;
using Domain.Entities;
using MediatR;

namespace Infrastructure.CQRS.Query;

public sealed class GetAllTablesQuery : IRequest<List<Table>>
{
    
}