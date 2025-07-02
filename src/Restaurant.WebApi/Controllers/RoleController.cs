using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("Role")]
public class RoleController(IRoleService rolesService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] string role)
    {
        var add = await rolesService.AddRoleAsync(role);
        if (add <= 0) return Conflict(add);
        return Ok(add);
    }

    [HttpPut]
    public async Task<UpdateResponseModel> Update(UpdateRolesRequestModel request)
    {
        return await rolesService.UpdateAsync(request);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string userName, string roles)
    {
        var affectedRows = await rolesService.DeleteAsync(userName, roles);
        if (affectedRows > 0)
            return Ok();

        return NotFound();
    }
}