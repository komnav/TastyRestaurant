using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos.Waiter.Requests;
using RestaurantLayer.Dtos.Waiter.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Waiter")]
    public class WaiterController(IWaiterService waiterService) : Controller
    {
        private readonly IWaiterService _waiterService = waiterService;

        [HttpPost]
        public async Task<CreateWaiterResponseModel> Create(CreateWaiterRequestModel request)
        {
            return await _waiterService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _waiterService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetWaiterResponseModel>> GetAll()
        {
            return await _waiterService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetWaiterResponseModel?> Get(int id)
        {
            return await _waiterService.GetAsync(id);
        }
    }
}
