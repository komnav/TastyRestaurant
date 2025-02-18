using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [ApiController]
    [Route("api/roles")]
    public class UpdateRolesController(IUpdateRolesService updateRolesService) : Controller
    {
        private readonly IUpdateRolesService _updateRolesService = updateRolesService;

        [HttpPut]
        [Consumes("application/json")]
        public async Task<UpdateResponseModel> Update(UpdateRolesRequestModel request)
        {
            return await _updateRolesService.UpdateAsync(request);
        }
    }
}
