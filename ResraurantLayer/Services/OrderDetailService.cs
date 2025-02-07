using Domain.Entities;
using Infrastructure.Repositories;
using ResrautantLayer.Exceptions;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.OrderDetail.Requests;
using RestaurantLayer.Dtos.OrderDetail.Responses;

namespace RestaurantLayer.Services
{
    public class OrderDetailService(IOrderDetailRepository orderDetailRepository) : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;
        public async Task<CreateOrderDetailResponseModel> CreateAsync(CreateOrderDetailRequestModel request)
        {
            var addOrderDetail = new OrderDetail
            {
                OrderId = request.OrderId,
                MenuItemId = request.MenuItemId,
                Price = request.Price,
                Quantity = request.Quantity,
                Status = request.Status
            };

            var rows = await _orderDetailRepository.CreateAsync(addOrderDetail);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(addOrderDetail));
            }

            return new CreateOrderDetailResponseModel(addOrderDetail.Id, addOrderDetail.MenuItemId, addOrderDetail.OrderId, addOrderDetail.Quantity, addOrderDetail.Price, addOrderDetail.Status);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _orderDetailRepository.DeleteAsync(id);
        }

        public async Task<List<GetOrderDetailResponseModel>> GetAllAsync()
        {
            var getOrderDetails = await _orderDetailRepository.GetAllAsync();

            return getOrderDetails.Select(getOrderDetails => new GetOrderDetailResponseModel(
                getOrderDetails.Id,
                getOrderDetails.MenuItemId,
                getOrderDetails.OrderId,
                getOrderDetails.Quantity,
                getOrderDetails.Price,
                getOrderDetails.Status
                )).ToList();
        }

        public async Task<GetOrderDetailResponseModel?> GetAsync(int id)
        {
            var getOrderDetail = await _orderDetailRepository.GetAsync(id);

            if (getOrderDetail == null)
            {
                return null;
            }

            return new GetOrderDetailResponseModel(
                getOrderDetail.Id,
                getOrderDetail.MenuItemId,
                getOrderDetail.OrderId,
                getOrderDetail.Quantity,
                getOrderDetail.Price,
                getOrderDetail.Status);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateOrderDetailRequestModel request)
        {
            var updateOrderDetail = await _orderDetailRepository.UpdateAsync(id, request.OrderId, request.MenuItemId, request.Quantity, request.Price, request.Status);

            return new UpdateResponseModel(updateOrderDetail);
        }
    }
}
