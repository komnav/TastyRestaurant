using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Role.Requests;
using Application.Services;
using RestaurantLayer.Dtos.Role.Responses;

namespace Restaurant.WebApi.Controllers
{
    //[Authorize(Roles = UserRoles.SuperAdmin)]
    [ApiController]
    [Route("api/roles")]
    public class RolesController(IRolesService rolesService) : Controller
    {
        private readonly IRolesService _rolesService = rolesService;

        [HttpPut]
        [Consumes("application/json")]
        public async Task<UpdateResponseModel> Update(UpdateRolesRequestModel request)
        {
            return await _rolesService.UpdateAsync(request);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string userName, string roles)
        {
            var affectedRows = await _rolesService.DeleteAsync(userName, roles);
            if (affectedRows > 0)
                return Ok();

            return NotFound();
        }

        [HttpGet("ByName")]
        public async Task<List<GetRoleResponseModel>> GetRolesByUserName(string userName)
        {
            return await _rolesService.GetRolesByUserNameAsync(userName);
        }

        [HttpGet("ByRole")]
        public async Task<List<GetUserResponseModel>> GetUserNameByRole(string role)
        {
            return await _rolesService.GetUserNameByRoleAsync(role);
        }
    }
}