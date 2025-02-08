using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Waiter.Requests;
using RestaurantLayer.Dtos.Waiter.Responses;

namespace RestaurantLayer.Services
{
    public interface IWaiterService
    {
        Task<CreateWaiterResponseModel> CreateAsync(CreateWaiterRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateWaiterRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetWaiterResponseModel?> GetAsync(int id);
        Task<List<GetWaiterResponseModel>> GetAllAsync();
    }
}
