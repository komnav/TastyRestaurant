using Domain.Enums;

namespace RestaurantLayer.Dtos.MenuItem.Response;

public record CreateMenuItemResponseModel(
    int? CategoryId,
    decimal Price,
    string Name,
    MenuItemStatus Status
    );

