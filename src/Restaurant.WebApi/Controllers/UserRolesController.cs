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
    public async Task<IActionResult> LinkRoleToUser(int idUser, string role)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == idUser);
        if (getUser is null)
            return BadRequest($"This user: {idUser} not found");

        var getRole = await _roleManager.FindByNameAsync(role);
        if (getRole == null) return BadRequest($"This role: {role} doesn't exist to {getUser}");

        await _userManager.AddToRoleAsync(getUser, $"{role}");
        return Ok($"User {getUser.UserName} has been linked to role {getRole.Name}");
    }

    [HttpDelete]
    public async Task<IActionResult> UnLinkRoleToUser(int idUser, string role)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == idUser);
        if (getUser is null)
            return BadRequest($"This user: {idUser} not found");

        var getRole = await _roleManager.FindByNameAsync(role);
        if (getRole == null) return BadRequest($"This role: {role} doesn't exist to {getUser}");

        var delete = await _userManager.RemoveFromRoleAsync(getUser, $"{role}");
        if (delete.Succeeded)
        {
            return Ok($"User {getUser.UserName} has been unlinked to role {getRole.Name}");
        }

        return BadRequest($"There isn't user {getUser} linked to this {role}");
    }

    [HttpGet]
    public async Task<IActionResult> GetUserRoles(int idUser)
    {
        var getUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == idUser);
        if (getUser is null)
            return BadRequest($"This user: {idUser} not found");

        var getRoles = await _userManager.GetRolesAsync(getUser);

        return Ok(getRoles.ToList());
    }
}