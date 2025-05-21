using Application.Dtos;
using Application.Dtos.Order.Requests;
using Application.Dtos.Order.Responses;

namespace Application.Services
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
