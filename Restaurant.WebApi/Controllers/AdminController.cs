using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Admin.Requests;
using RestaurantLayer.Dtos.Admin.Responses;
using RestaurantLayer.Services;
using System.Runtime.CompilerServices;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Admin")]
    public class AdminController(IAdminService adminService) : Controller
    {
        private readonly IAdminService _adminService = adminService;

        [HttpPost]
        public async Task<CreateAdminResponseModel> Create([FromBody] CreateAdminRequestModel request)
        {
            return await _adminService.CreateAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _adminService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetAdminResponseModel>> Get()
        {
            return await _adminService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetAdminResponseModel?> GetAll(int id)
        {
            return await _adminService.GetAsync(id);
        }

        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, [FromBody] UpdateAdminRequestModel request)
        {
            return await _adminService.UpdateAsync(id, request);
        }
    }
}
