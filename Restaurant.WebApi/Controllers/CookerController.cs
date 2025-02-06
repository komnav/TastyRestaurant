using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Cooker.Request;
using RestaurantLayer.Dtos.Cooker.Response;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Cooker")]
    public class CookerController(ICookerService cookerService) : Controller
    {
        private readonly ICookerService _cookerService = cookerService;

        [HttpPost]
        public async Task<CreateCookerResponseModel> Create(CreateCookerRequestModel request)
        {
            return await _cookerService.CreateAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _cookerService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetCookerResponseModel>> GetAll()
        {
            return await _cookerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetCookerResponseModel?> Get(int id)
        {
            return await _cookerService.GetAsync(id);
        }

        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateCookerRequestModel request)
        {
            return await _cookerService.UpdateAsync(id, request);
        }
    }
}
