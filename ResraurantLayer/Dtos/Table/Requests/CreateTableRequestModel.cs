using Domain.Enums;

namespace RestaurantLayer.Dtos.Table.Requests;

public record CreateTableRequestModel(
    int Number,
    int Capacity,
    TableType Type
    );
