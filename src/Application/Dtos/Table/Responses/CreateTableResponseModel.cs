using Domain.Enums;

namespace Application.Dtos.Table.Responses;

public record CreateTableResponseModel(
    int Id,
    int Number,
    int Capacity,
    TableType Type
     );
