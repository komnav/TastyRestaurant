using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantLayer.Dtos;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("Role")]
public class RoleController(
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] string role)
    {
        var adminRole = await _roleManager.FindByNameAsync(role);
        if (adminRole == null)
        {
            var newRole = new IdentityRole<int>(role);
            await _roleManager.CreateAsync(newRole);
            return Ok(role);
        }
        else
        {
            return BadRequest("Role already exists");
        }
    }

    [HttpPut]
    public async Task<UpdateResponseModel> Update(string role, string newName)
    {
        var findOldRoleByName = await _roleManager.FindByNameAsync(role);
        var findNewRoleByName = await _roleManager.FindByNameAsync(role);

        if (findOldRoleByName == null && findNewRoleByName == null)
            return new UpdateResponseModel(0);

        findOldRoleByName!.Name = newName;
        var update = await _roleManager.UpdateAsync(findOldRoleByName);
        if (update.Succeeded)
        {
            return new UpdateResponseModel(1);
        }

        return new UpdateResponseModel(0);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string role)
    {
        var findRoleByName = await _roleManager.FindByNameAsync(role);
        if (findRoleByName is null)
        {
            return BadRequest("Role not found");
        }

        await _roleManager.DeleteAsync(findRoleByName);
        return Ok($"{role} is deleted");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getFromDb = await _roleManager.Roles.ToListAsync();
        return Ok(getFromDb.Select(x => x.Name));
    }
}