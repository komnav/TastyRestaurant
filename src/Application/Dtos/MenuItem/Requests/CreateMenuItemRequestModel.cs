namespace Application.Dtos.MenuItem.Requests;

public record CreateMenuItemRequestModel(
    int? CategoryId,
    decimal Price,
    string Name
    );

