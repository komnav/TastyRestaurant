using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos.OrderDetail.Requests;
using RestaurantLayer.Dtos.OrderDetail.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("OrderDetail")]
    public class OrderDetailController(IOrderDetailService orderDetailService) : Controller
    {
        private readonly IOrderDetailService _orderDetailService = orderDetailService;

        [HttpPost]
        public async Task<CreateOrderDetailResponseModel> Create(CreateOrderDetailRequestModel request)
        {
            return await _orderDetailService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _orderDetailService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetOrderDetailResponseModel>> GetAll()
        {
            return await _orderDetailService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetOrderDetailResponseModel?> Get(int id)
        {
            return await _orderDetailService.GetAsync(id);
        }
    }
}
