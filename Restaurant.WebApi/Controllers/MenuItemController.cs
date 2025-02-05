using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.MenuItem.Requests;
using RestaurantLayer.Dtos.MenuItem.Response;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("MenuItem")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        private readonly IMenuItemService _menuItemService = menuItemService;

        [HttpPost]
        public async Task<CreateMenuItemResponseModel> Create([FromBody] CreateMenuItemRequestModel menuItem)
        {
            return await _menuItemService.CreateAsync(menuItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuItemService.DeleteAsync(id);
            return Ok();
        }

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
