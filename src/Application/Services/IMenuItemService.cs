using Application.Dtos.MenuItem.Requests;
using Application.Dtos.MenuItem.Response;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.MenuItem.Requests;

namespace RestaurantLayer.Services
{
    public interface IMenuItemService
    {
        Task<CreateMenuItemResponseModel> CreateAsync(CreateMenuItemRequestModel createMenuItem);
        Task<int> DeleteAsync(int id);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateMenuItemRequestModel menuItem);
        Task<GetMenuItemResponseModel?> GetAsync(int id);
        Task<List<GetMenuItemResponseModel>> GetAll();
        Task<List<GetMenuItemResponseModel>> GetByCategoryAsync(int categoryId);
    }
}