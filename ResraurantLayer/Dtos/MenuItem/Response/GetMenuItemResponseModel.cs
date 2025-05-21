using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.MenuItem.Response;

public record GetMenuItemResponseModel(
    int Id,
    int? CategoryId,
    decimal Price,
    [Required] string Name,
    MenuItemStatus Status
    );
