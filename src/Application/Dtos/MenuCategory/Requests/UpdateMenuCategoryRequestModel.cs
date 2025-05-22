using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.MenuCategory.Requests;

    public record UpdateMenuCategoryRequestModel(
        [Required] string Name,
        int? ParentId
        );

