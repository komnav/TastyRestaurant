using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Cashier.Requests;
using RestaurantLayer.Dtos.Cashier.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Cashier")]
    public class CashierController(ICashierService cashierService) : Controller
    {
        private readonly ICashierService _cashierService = cashierService;

        [HttpPost]
        public async Task<CreateCahierResponseModel> Create(CreateCashierRequestModel request)
        {
            return await _cashierService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _cashierService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<GetCashierResponseModel?> Get(int id)
        {
            return await _cashierService.GetAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<List<GetCashierResponseModel>> GetAll()
        {
            return await _cashierService.GetAllAsync();
        }

        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateCashierRequestModel request)
        {
            return await _cashierService.UpdateAsync(id, request);
        }
    }
}
