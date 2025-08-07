using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.MenuCategory.Requests;
using Application.Dtos.MenuCategory.Responses;
using Application.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("MenuCategory")]
    public class MenuCategoryController(IMenuCategoryService menuCategoryService) : ControllerBase
    {
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateMenuCategoryResponseModel> Create([FromBody] CreateMenuCategoryRequestModel request)
        {
            return await menuCategoryService.CreateAsync(request.Name);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var affectedRows = await menuCategoryService.DeleteAsync(id);
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
            var updateCategory = await menuCategoryService.UpdateAsync(id, request);
            return updateCategory;
        }

        [HttpGet("{id}")]
        public async Task<GetMenuCategoryResponseModel?> Get([FromRoute] int id)
        {
            return await menuCategoryService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<GetMenuCategoryResponseModel>> GetAll()
        {
            return await menuCategoryService.GetAllAsync();
        }
    }
}