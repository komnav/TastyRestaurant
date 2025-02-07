using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Customer.Requests;
using RestaurantLayer.Dtos.Customer.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Customer")]
    public class CustomerController(ICustomerService customerService) : Controller
    {
        private readonly ICustomerService _customerService = customerService;
        [HttpPost]
        public async Task<CreateCustomerResponseModel> Create(CreateCustomerRequestModel request)
        {
            return await _customerService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _customerService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetCustomerResponseModel>> GetAll()
        {
            return await _customerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetCustomerResponseModel?> Get(int id)
        {
            return await _customerService.GetAsync(id);
        }

        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateCustomerRequestModel request)
        {
            return await _customerService.UpdateAsync(id, request);
        }
    }
}
