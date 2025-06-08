using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.MenuItem.Requests;
using Application.Dtos.MenuItem.Response;
using Application.Services;

namespace Restaurant.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("MenuItem")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        private readonly IMenuItemService _menuItemService = menuItemService;

       [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost]
        public async Task<CreateMenuItemResponseModel> Create([FromBody] CreateMenuItemRequestModel menuItem)
        {
            return await _menuItemService.CreateAsync(menuItem);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedRows = await _menuItemService.DeleteAsync(id);
            if (affectedRows > 0)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPut("{id}")]
        public async Task<UpdateResponseModel> Update(int id, [FromBody] UpdateMenuItemRequestModel menuItem)
        {
            return await _menuItemService.UpdateAsync(id, menuItem);
        }

        [HttpGet("{id}")]
        public async Task<GetMenuItemResponseModel?> Get([FromRoute] int id)
        {
            return await _menuItemService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<GetMenuItemResponseModel>> GetAll()
        {
            return await _menuItemService.GetAll();
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<List<GetMenuItemResponseModel>> GetByCategory(int categoryId)
        {
            return await _menuItemService.GetByCategoryAsync(categoryId);
        }
    }
}