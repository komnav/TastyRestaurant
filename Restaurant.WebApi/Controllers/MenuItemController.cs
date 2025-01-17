using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ResraurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("menuitem")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        private readonly IMenuItemService _menuItemService = menuItemService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuItem menuItem)
        {
            await _menuItemService.CreateAsync(menuItem);
            return Ok();
        }
        [HttpDelete]
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
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            await _menuItemService.GetAsync(id);
            return Ok();
        }
    }
}
