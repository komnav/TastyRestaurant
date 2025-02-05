using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Admin.Requests;
using RestaurantLayer.Dtos.Admin.Responses;

namespace RestaurantLayer.Services
{
    public interface IAdminService
    {
        Task<CreateAdminResponseModel> CreateAsync(CreateAdminRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(UpdateAdminRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetAdminResponseModel> GetAsync(int id);
        Task<List<GetAdminResponseModel>> GetAllAsync();
    }
}
