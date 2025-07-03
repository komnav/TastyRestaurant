using Application.Dtos;
using Application.Dtos.OrderDetail.Requests;
using Application.Dtos.OrderDetail.Responses;

namespace Application.Services
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