using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Infrastructure.CQRS.Commands;

public class CreateTableCommand : IRequest<Table>
{
    public int Number { get; set; }
    public int Capacity { get; set; }
    public TableType Type { get; set; }
}