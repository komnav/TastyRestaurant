using Domain.Enums;

namespace Application.Dtos.Table.Requests;

public record CreateTableRequestModel(
    int Number,
    int Capacity,
    TableType Type
    );
