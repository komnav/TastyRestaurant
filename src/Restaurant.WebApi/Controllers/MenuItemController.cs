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
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateMenuItemResponseModel> Create([FromBody] CreateMenuItemRequestModel menuItem)
        {
            return await menuItemService.CreateAsync(menuItem);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedRows = await menuItemService.DeleteAsync(id);
            if (affectedRows > 0)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<UpdateResponseModel> Update(int id, [FromBody] UpdateMenuItemRequestModel menuItem)
        {
            return await menuItemService.UpdateAsync(id, menuItem);
        }

        [HttpGet("{id}")]
        public async Task<GetMenuItemResponseModel?> Get([FromRoute] int id)
        {
            return await menuItemService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<GetMenuItemResponseModel>> GetAll()
        {
            return await menuItemService.GetAll();
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<List<GetMenuItemResponseModel>> GetByCategory(int categoryId)
        {
            return await menuItemService.GetByCategoryAsync(categoryId);
        }
    }
}