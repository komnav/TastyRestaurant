namespace Application.Dtos.Role.Requests;

public record LinkRoleToUserRequestModel(
    int IdUser,
    string Role
);