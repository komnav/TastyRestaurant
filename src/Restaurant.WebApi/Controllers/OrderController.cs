using Application.Dtos.Order.Requests;
using Application.Dtos.Order.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Order.Requests;
using RestaurantLayer.Dtos.Order.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController(IOrderService orderService) : Controller
    {
        private readonly IOrderService _orderService = orderService;

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateOrderResponseModel> Create(CreateOrderRequestModel request)
        {
            return await _orderService.CreateAsync(request);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateOrderRequestModel request)
        {
            return await _orderService.UpdateAsync(id, request);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _orderService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetOrderResponseModel>> GetAll()
        {
            return await _orderService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetOrderResponseModel?> Get(int id)
        {
            return await _orderService.GetAsync(id);
        }
    }
}
