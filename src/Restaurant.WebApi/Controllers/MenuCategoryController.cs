using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResraurantLayer.Services;
using Application.Dtos;
using Application.Dtos.MenuCategory.Requests;
using Application.Dtos.MenuCategory.Responses;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("MenuCategory")]
    public class MenuCategoryController(IMenuCategoryService menuCategoryService) : ControllerBase
    {
        private readonly IMenuCategoryService _menuCategoryService = menuCategoryService;

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateMenuCategoryResponseModel> Create([FromBody] CreateMenuCategoryRequestModel request)
        {
            return await _menuCategoryService.CreateAsync(request.Name);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var affectedRows = await _menuCategoryService.DeleteAsync(id);
            if (affectedRows > 0)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<UpdateResponseModel> Update(int id, [FromBody] UpdateMenuCategoryRequestModel request)
        {
            var updateCategory = await _menuCategoryService.UpdateAsync(id, request);
            return updateCategory;
        }

        [HttpGet("{id}")]
        public async Task<GetMenuCategoryResponseModel?> Get([FromRoute] int id)
        {
            return await _menuCategoryService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<GetMenuCategoryResponseModel>> GetAll()
        {
            return await _menuCategoryService.GetAllAsync();
        }
    }
}