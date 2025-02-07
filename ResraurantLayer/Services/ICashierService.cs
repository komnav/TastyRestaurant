using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Cashier.Requests;
using RestaurantLayer.Dtos.Cashier.Responses;

namespace RestaurantLayer.Services
{
    public interface ICashierService
    {
        Task<CreateCahierResponseModel> CreateAsync(CreateCashierRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateCashierRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetCashierResponseModel?> GetAsync(int id);
        Task<List<GetCashierResponseModel>> GetAllAsync();
    }
}
