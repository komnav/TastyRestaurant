using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.MenuCategory.Requests;

public record CreateMenuCategoryRequestModel([Required]string Name);
