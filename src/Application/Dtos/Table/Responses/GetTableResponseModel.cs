using Domain.Enums;

namespace Application.Dtos.Table.Responses;

public record GetTableResponseModel(
    int Id,
    int Number,
    int Capacity,
    TableType Type
    );
