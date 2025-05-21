using Domain.Enums;

namespace Application.Dtos.Table.Requests;

public record UpdateTableRequestModel(
    int Number,
    int Capacity,
    TableType Type
    );
