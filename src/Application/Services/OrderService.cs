using Application.Dtos;
using Application.Dtos.Order.Requests;
using Application.Dtos.Order.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using ResrautantLayer.Exceptions;
using RestaurantLayer.Repositories;

namespace Application.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<CreateOrderResponseModel> CreateAsync(CreateOrderRequestModel request)
        {
            var addOrder = new Order
            {
                TableId = request.TableId,
                DateTime = request.DateTime,
                Status = request.Status,
            };

            var rows = await _orderRepository.CreateAsync(addOrder);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(addOrder));
            }

            return new CreateOrderResponseModel(addOrder.Id, addOrder.TableId, addOrder.DateTime, addOrder.Status);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<List<GetOrderResponseModel>> GetAllAsync()
        {
            var getOrders = await _orderRepository.GetAllAsync();

            return getOrders.Select(getOrders => new GetOrderResponseModel(
                getOrders.Id,
                getOrders.TableId,
                getOrders.DateTime,
                getOrders.Status
            )).ToList();
        }

        public async Task<GetOrderResponseModel?> GetAsync(int id)
        {
            var getOrder = await _orderRepository.GetAsync(id);

            if (getOrder == null)
            {
                return null;
            }

            return new GetOrderResponseModel(
                getOrder.Id,
                getOrder.TableId,
                getOrder.DateTime,
                getOrder.Status
            );
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateOrderRequestModel request)
        {
            var updateOrder = await _orderRepository.UpdateAsync(id, request.TableId, request.DateTime, request.Status);

            return new UpdateResponseModel(updateOrder);
        }
    }
}