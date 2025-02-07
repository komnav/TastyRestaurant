using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Order.Requests;
using RestaurantLayer.Dtos.Order.Responses;

namespace RestaurantLayer.Services
{
    public interface IOrderService
    {
        Task<CreateOrderResponseModel> CreateAsync(CreateOrderRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateOrderRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetOrderResponseModel?> GetAsync(int id);
        Task<List<GetOrderResponseModel>> GetAllAsync();
    }
}
