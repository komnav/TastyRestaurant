using Domain.Entities;
using Infrastructure.Repositories;
using ResrautantLayer.Exceptions;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Admin.Requests;
using RestaurantLayer.Dtos.Admin.Responses;
using RestaurantLayer.Exceptions;

namespace RestaurantLayer.Services
{
    public class AdminService(IAdminRepository adminRepository) : IAdminService
    {
        private readonly IAdminRepository _adminRepository = adminRepository;
        public async Task<CreateAdminResponseModel> CreateAsync(CreateAdminRequestModel request)
        {
            var admin = new Admin
            {
                ContactId = request.ContactId,
            };
            var rows = await _adminRepository.CreateAsync(admin);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedExceptionForIntType(rows);
            }
            return new CreateAdminResponseModel(admin.ContactId);
        }

        public Task<int> DeleteAsync(int id)
        {
            return _adminRepository.DeleteAsync(id);
        }

        public async Task<List<GetAdminResponseModel>> GetAllAsync()
        {
            var getAdmin = await _adminRepository.GetAllAsync();

            return getAdmin.Select(getAdmin => new GetAdminResponseModel(
                getAdmin.Id,
                getAdmin.ContactId
                )).ToList();
        }

        public async Task<GetAdminResponseModel?> GetAsync(int id)
        {
            var getAdmin = await _adminRepository.GetAsync(id);
            if (getAdmin == null)
            {
                return null;
            }

            return new GetAdminResponseModel(id, getAdmin.ContactId);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateAdminRequestModel request)
        {
            var updateAdmin = await _adminRepository.UpdateAsync(id, request.ContactId);

            return new UpdateResponseModel(updateAdmin);
        }
    }
}
