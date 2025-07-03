using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("Role")]
[Authorize(Roles = UserRoles.SuperAdmin)]
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
    public async Task<UpdateResponseModel> Update(UpdateRolesRequestModel rolesRequestModel)
    {
        var findOldRoleByName = await _roleManager.FindByNameAsync(rolesRequestModel.Role);
        var findNewRoleByName = await _roleManager.FindByNameAsync(rolesRequestModel.NewName);

        if (findOldRoleByName == null && findNewRoleByName == null)
            return new UpdateResponseModel(0);

        findOldRoleByName!.Name = rolesRequestModel.NewName;
        var update = await _roleManager.UpdateAsync(findOldRoleByName);
        if (update.Succeeded)
        {
            return new UpdateResponseModel(1);
        }

        return new UpdateResponseModel(0);
    }

    [HttpDelete("{role}")]
    public async Task<IActionResult> Delete(string role)
    {
        var findRoleByName = await _roleManager.FindByNameAsync(role);
        if (findRoleByName is null)
        {
            return NotFound("Role not found");
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