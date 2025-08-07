using Domain.Entities;
using Application.Dtos;
using Application.Dtos.Order.Requests;
using Application.Dtos.Order.Responses;
using Application.Exceptions;
using Application.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService(IOrderRepository orderRepository, UserManager<User> userManager) : IOrderService
    {
        private const int WaiterFeePercentage = 10;
        
        public async Task<CreateOrderResponseModel> CreateAsync(CreateOrderRequestModel request)
        {
            var checkUserId = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (checkUserId == null) throw new UserDoesNotExistException(nameof(request.UserId));

            var addOrder = new Order
            {
                UserId = request.UserId,
                DateTime = request.DateTime,
                Status = request.Status,
            };

            var rows = await orderRepository.CreateAsync(addOrder);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(addOrder));
            }

            return new CreateOrderResponseModel(addOrder.Id, addOrder.DateTime, addOrder.Status);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await orderRepository.DeleteAsync(id);
        }

        public async Task<List<GetOrderResponseModel>> GetAllAsync()
        {
            var getOrders = await orderRepository.GetAllAsync();

            return getOrders.Select(
                orders => new GetOrderResponseModel(
                    orders.Id,
                    orders.UserId,
                    orders.DateTime,
                    orders.Status
                )).ToList();
        }

        public decimal GetTotalPrice(List<OrderDetail> details)
        {
            var calculatePrice = details.Sum(x => x.Price * x.Quantity);
            var waiterPercentage = (calculatePrice * WaiterFeePercentage) / 100;
            return waiterPercentage + calculatePrice;
        }

        public async Task<GetOrderResponseModel?> GetAsync(int id)
        {
            var getOrder = await orderRepository.GetAsync(id);

            if (getOrder == null)
            {
                return null;
            }

            return new GetOrderResponseModel(
                getOrder.Id,
                getOrder.UserId,
                getOrder.DateTime,
                getOrder.Status
            );
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateOrderRequestModel request)
        {
            var updateOrder = await orderRepository.UpdateAsync(id, request.UserId, request.DateTime, request.Status);

            return new UpdateResponseModel(updateOrder);
        }
    }
}