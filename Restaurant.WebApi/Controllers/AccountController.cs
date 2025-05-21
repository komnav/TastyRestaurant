using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using Application.Services;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IAccountService accountService, ApplicationDbContext applicationDbContext) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpPost("register")]
    public async Task<AuthResponse> Register([FromBody] RegisterUserRequest request)
    {
        return await _accountService.CreateAsync(request);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        var getUser = await applicationDbContext.Users
         .FirstOrDefaultAsync(s => s.UserName.ToLower() == request.UserName.ToLower() && s.Password == request.Password);

        // var getUser = await _accountService.GetAsync(request.UserName, request.Password);

        if (getUser == null)
        {
            return Unauthorized();
        }

        var token = _accountService.CreateToken(getUser);

        return Ok(new AuthResponse { Token = token });
    }
}
