using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.OrderDetail.Requests;
using RestaurantLayer.Dtos.OrderDetail.Responses;

namespace RestaurantLayer.Services
{
    public interface IOrderDetailService
    {
        Task<CreateOrderDetailResponseModel> CreateAsync(CreateOrderDetailRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateOrderDetailRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<List<GetOrderDetailResponseModel>> GetAllAsync();
        Task<GetOrderDetailResponseModel?> GetAsync(int id);
    }
}
