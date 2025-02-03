namespace RestaurantLayer.Dtos.MenuCategory.Responses;

    public record UpdateMenuCategoryResponseModel(
        int Id,
        string Name,
        int ParentId
        );

