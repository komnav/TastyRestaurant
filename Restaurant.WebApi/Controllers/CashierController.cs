using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
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
}
