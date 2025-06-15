using Application.Dtos.Account.Requests;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.WebApi.Controllers;

[ApiController]
public class AccountIdentityController(UserManager<User> userManager) : Controller
{
    private readonly UserManager<User> _userManager = userManager;


    [HttpPost("registerWithIdentity")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest model)
    {
        var user = new User
        {
            UserName = model.UserName,
            Role = UserRoles.Customer
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUsersByUserName(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [HttpGet("check-admin/{userName}")]
    public async Task<IActionResult> CheckIsAdmin(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
    
        if (user == null)
        {
            return NotFound();
        }
    
        bool isAdmin = user.Role == UserRoles.Admin || user.Role == UserRoles.SuperAdmin;
    
        return Ok(new { IsAdmin = isAdmin });
    }
}