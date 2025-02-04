using Domain.Enums;

namespace RestaurantLayer.Dtos.Table.Requests;

public record UpdateTableRequestModel(
    int Number,
    int Capacity,
    TableType Type
    );
