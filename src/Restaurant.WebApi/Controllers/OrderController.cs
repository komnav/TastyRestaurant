using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Order.Requests;
using Application.Dtos.Order.Responses;
using Application.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController(IOrderService orderService) : Controller
    {
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateOrderResponseModel> Create(CreateOrderRequestModel request)
        {
            return await orderService.CreateAsync(request);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateOrderRequestModel request)
        {
            return await orderService.UpdateAsync(id, request);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await orderService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetOrderResponseModel>> GetAll()
        {
            return await orderService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetOrderResponseModel?> Get(int id)
        {
            return await orderService.GetAsync(id);
        }
    }
}
