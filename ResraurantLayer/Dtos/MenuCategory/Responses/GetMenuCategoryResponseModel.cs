using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.MenuCategory.Responses;

public record GetMenuCategoryResponseModel(
    int Id,
    [Required] string Name,
    int? ParentId
    );

