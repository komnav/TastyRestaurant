using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;
using Application.Dtos.Role.Requests;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("Role")]
[Authorize(Roles = UserRoles.SuperAdmin)]
public class RoleController(RoleManager<IdentityRole<int>> roleManager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] string role)
    {
        var adminRole = await roleManager.FindByNameAsync(role);
        if (adminRole == null)
        {
            var newRole = new IdentityRole<int>(role);
            await roleManager.CreateAsync(newRole);
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
        var findOldRoleByName = await roleManager.FindByNameAsync(rolesRequestModel.Role);
        var findNewRoleByName = await roleManager.FindByNameAsync(rolesRequestModel.NewName);

        if (findOldRoleByName == null && findNewRoleByName == null)
            return new UpdateResponseModel(0);

        findOldRoleByName!.Name = rolesRequestModel.NewName;
        var update = await roleManager.UpdateAsync(findOldRoleByName);
        if (update.Succeeded)
        {
            return new UpdateResponseModel(1);
        }

        return new UpdateResponseModel(0);
    }

    [HttpDelete("{role}")]
    public async Task<IActionResult> Delete(string role)
    {
        var findRoleByName = await roleManager.FindByNameAsync(role);
        if (findRoleByName is null)
        {
            return NotFound("Role not found");
        }

        await roleManager.DeleteAsync(findRoleByName);
        return Ok($"{role} is deleted");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getFromDb = await roleManager.Roles.ToListAsync();
        return Ok(getFromDb.Select(x => x.Name));
    }
}