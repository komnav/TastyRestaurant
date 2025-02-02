using System.ComponentModel.DataAnnotations;

namespace ResraurantLayer.Dtos.MenuCategory.Requests;

public record CreateMenuCategoryRequestModel([Required]string Name);
