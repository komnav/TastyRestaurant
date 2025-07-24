namespace Application.Dtos.Role.Requests;

public record UpdateRolesRequestModel(
    string Role,
    string NewName
);