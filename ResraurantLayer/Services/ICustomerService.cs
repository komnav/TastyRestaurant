using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Customer.Requests;
using RestaurantLayer.Dtos.Customer.Responses;

namespace RestaurantLayer.Services
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponseModel> CreateAsync(CreateCustomerRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateCustomerRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetCustomerResponseModel?> GetAsync(int id);
        Task<List<GetCustomerResponseModel>> GetAllAsync();
    }
}
