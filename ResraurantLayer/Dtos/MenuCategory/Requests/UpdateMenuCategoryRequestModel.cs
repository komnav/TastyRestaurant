using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Dtos.MenuCategory.Requests;

    public record UpdateMenuCategoryRequestModel(
        [Required] string Name,
        int ParentId
        );

