using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Cooker.Request;
using RestaurantLayer.Dtos.Cooker.Response;

namespace RestaurantLayer.Services
{
    public interface ICookerService
    {
        Task<CreateCookerResponseModel> CreateAsync(CreateCookerRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateCookerRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetCookerResponseModel?> GetAsync(int id);
        Task<List<GetCookerResponseModel>> GetAllAsync();
    }
}
