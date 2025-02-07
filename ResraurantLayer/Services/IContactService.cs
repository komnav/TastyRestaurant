using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Contact.Requests;
using RestaurantLayer.Dtos.Contact.Responses;

namespace RestaurantLayer.Services
{
    public interface IContactService
    {
        Task<CreateContactResponseModel> CreateAsync(CreateContactRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateContactRequestModel request);
        Task<GetContactResponseModel?> GetAsync(int id);
        Task<List<GetContactResponseModel>> GetAllAsync();
    }
}
