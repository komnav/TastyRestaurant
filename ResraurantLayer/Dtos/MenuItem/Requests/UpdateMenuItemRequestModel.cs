using Domain.Enums;

namespace Application.Dtos.MenuItem.Requests;

public record UpdateMenuItemRequestModel(
    int CategoryId,
    decimal Price,
    string Name,
    MenuItemStatus Status
    );
