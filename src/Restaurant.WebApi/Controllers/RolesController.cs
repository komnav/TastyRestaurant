using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Role.Requests;
using Application.Services;

namespace Restaurant.WebApi.Controllers
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
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
    }
}
