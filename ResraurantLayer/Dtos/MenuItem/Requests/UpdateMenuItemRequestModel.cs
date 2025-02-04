using Domain.Enums;

namespace RestaurantLayer.Dtos.MenuItem.Requests;

public record UpdateMenuItemRequestModel(
    int CategoryId,
    decimal Price,
    string Name,
    MenuItemStatus Status
    );
