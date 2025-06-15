using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using Application.Services;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IAccountService accountService)
    : ControllerBase
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
        var getUser = await _accountService.LoginAsync(request);

        if (getUser == null)
        {
            return Unauthorized();
        }

        return Ok(getUser.Token);
    }
}