using Application.Dtos;
using Application.Dtos.Role.Requests;

namespace Application.Services
{
    public interface IUpdateRolesService
    {
        Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request);
    }
}
