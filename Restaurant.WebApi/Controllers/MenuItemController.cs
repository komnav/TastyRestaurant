using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("MenuItem")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        private readonly IMenuItemService _menuItemService = menuItemService;

        [HttpPost]
        public async Task<MenuItem> Create([FromBody] MenuItem menuItem)
        {
            return await _menuItemService.CreateAsync(menuItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _menuItemService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MenuItem menuItem)
        {
            await _menuItemService.UpdateAsync(id, menuItem);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<MenuItem> Get([FromRoute] int id)
        {
            return await _menuItemService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<MenuItem>> GetAll()
        {
            return await _menuItemService.GetAll();
        }
    }
}
