using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ResraurantLayer.Dtos.MenuCategory.Requests;
using ResraurantLayer.Dtos.MenuCategory.Responses;
using ResraurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("MenuCategory")]
    public class MenuCategoryController(IMenuCategoryService menuCategoryService) : ControllerBase
    {
        private readonly IMenuCategoryService _menuCategoryService = menuCategoryService;

        [HttpPost]
        public async Task<CreateMenuCategoryResponseModel> Create([FromBody] CreateMenuCategoryRequestModel request)
        {
            var createdCategory = await _menuCategoryService.CreateAsync(request.Name);
            return createdCategory;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _menuCategoryService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<MenuCategory> Update([FromRoute] int id, [FromBody] MenuCategory menuCategory)
        {
            return await _menuCategoryService.UpdateAsync(id, menuCategory);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<MenuCategory?> Get([FromRoute] int id)
        {
            return await _menuCategoryService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<MenuCategory>> GetAll()
        {
            return await _menuCategoryService.GetAllAsync();
        }
    }
}
