using Domain.Enums;

namespace RestaurantLayer.Dtos.Table.Responses;

public record CreateTableResponseModel(
    int Id,
    int Number,
    int Capacity,
    TableType Type
     );
