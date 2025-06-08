namespace Application.Dtos.Account.Responses;

public class LoginResponseModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}