using Domain.Enums;

namespace Application.Dtos.MenuItem.Response;

public record CreateMenuItemResponseModel(
    int? CategoryId,
    decimal Price,
    string Name,
    MenuItemStatus Status
    );

