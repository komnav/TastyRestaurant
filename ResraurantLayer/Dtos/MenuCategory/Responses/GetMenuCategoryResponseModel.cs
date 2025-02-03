using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Dtos.MenuCategory.Responses;

public record GetMenuCategoryResponseModel(
    int Id,
    [Required] string Name,
    int? ParentId
    );

