using Domain.Entities;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.MenuItem.Requests;
using RestaurantLayer.Dtos.MenuItem.Response;

namespace RestaurantLayer.Services
{
    public interface IMenuItemService
    {
        Task<CreateMenuItemResponseModel> CreateAsync(CreateMenuItemRequestModel createMenuItem);
        Task<int> DeleteAsync(int id);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateMenuItemRequestModel menuItem);
        Task<GetMenuItemResponseModel?> GetAsync(int id);
        Task<List<GetMenuItemResponseModel>> GetAll();
    }
}
