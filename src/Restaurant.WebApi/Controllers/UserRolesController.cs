using Application.Dtos.Role.Requests;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("UserRoles")]
[Authorize(Roles = UserRoles.SuperAdmin)]
public class UserRolesController(
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;


    [HttpPost]
    public async Task<IActionResult> LinkRoleToUser(LinkRoleToUserRequestModel request)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == request.IdUser);
        if (getUser is null)
            return NotFound($"This user: {request.IdUser} not found");

        var getRole = await _roleManager.FindByNameAsync(request.Role);
        if (getRole == null) return NotFound($"This role: {request.Role} doesn't exist to {getUser}");

        await _userManager.AddToRoleAsync(getUser, $"{request.Role}");
        return Ok($"User {getUser.UserName} has been linked to role {getRole.Name}");
    }

    [HttpDelete("{idUser}/{role}")]
    public async Task<IActionResult> UnLinkRoleToUser(int idUser, string role)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == idUser);
        if (getUser is null)
            return NotFound($"This user: {idUser} not found");

        var getRole = await _roleManager.FindByNameAsync(role);
        if (getRole == null) return NotFound($"This role: {role} doesn't exist to {getUser}");

        var delete = await _userManager.RemoveFromRoleAsync(getUser, $"{role}");
        if (delete.Succeeded)
        {
            return Ok($"User {getUser.UserName} has been unlinked to role {getRole.Name}");
        }

        return BadRequest($"There isn't user {getUser} linked to this {role}");
    }

    [HttpGet("{idUser}")]
    public async Task<IActionResult> GetUserRoles(int idUser)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == idUser);
        if (getUser is null)
            return NotFound($"This user: {idUser} not found");

        var getRoles = await _userManager.GetRolesAsync(getUser);

        return Ok(getRoles.ToList());
    }
}