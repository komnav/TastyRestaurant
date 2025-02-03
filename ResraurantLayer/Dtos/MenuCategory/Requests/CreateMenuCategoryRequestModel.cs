using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Dtos.MenuCategory.Requests;

public record CreateMenuCategoryRequestModel([Required]string Name);
