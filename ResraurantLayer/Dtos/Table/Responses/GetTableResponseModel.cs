using Domain.Enums;

namespace RestaurantLayer.Dtos.Table.Responses;

public record GetTableResponseModel(
    int Id,
    int Number,
    int Capaciry,
    TableType Type
    );
