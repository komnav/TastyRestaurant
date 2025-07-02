namespace RestaurantLayer.Dtos.Role.Requests;

public record UpdateRolesRequestModel(
    string UserName,
    string Role
    );