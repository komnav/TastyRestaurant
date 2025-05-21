using Application.Dtos;
using Application.Dtos.MenuItem.Requests;
using Application.Dtos.MenuItem.Response;
using Domain.Entities;

namespace Application.Services
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
