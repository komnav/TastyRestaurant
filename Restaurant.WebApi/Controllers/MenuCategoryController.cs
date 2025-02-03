using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ResraurantLayer.Services;
using RestaurantLayer.Dtos.MenuCategory.Requests;
using RestaurantLayer.Dtos.MenuCategory.Responses;

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
        public async Task<UpdateMenuCategoryResponseModel> Update([FromRoute] int id, [FromBody] UpdateMenuCategoryRequestModel request)
        {
            var menuCategory = new MenuCategory
            {
                Id = id,
                Name = request.Name,
                ParentId = request.ParentId,
            };
            var updateCategory = await _menuCategoryService.UpdateAsync(id,menuCategory);
            return updateCategory;
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
