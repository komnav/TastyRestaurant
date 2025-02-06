using Domain.Entities;
using Infrastructure.Repositories;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Cooker.Request;
using RestaurantLayer.Dtos.Cooker.Response;

namespace RestaurantLayer.Services
{
    public class CookerService(ICookerRepository cookerRepository) : ICookerService
    {
        private readonly ICookerRepository _cookerRepository = cookerRepository;
        public async Task<CreateCookerResponseModel> CreateAsync(CreateCookerRequestModel request)
        {
            var cooker = new Cooker
            {
                ContactId = request.ContactId
            };
            var addCooker = await _cookerRepository.CreateAsync(cooker);

            if (addCooker <= 0)
            {
                return null;
            }

            return new CreateCookerResponseModel(cooker.ContactId);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _cookerRepository.DeleteAsync(id);
        }

        public async Task<List<GetCookerResponseModel>> GetAllAsync()
        {
            var getCooker = await _cookerRepository.GetAllAsync();

            return getCooker.Select(getCooker => new GetCookerResponseModel(
                getCooker.Id,
                getCooker.ContactId
                )).ToList();
        }

        public async Task<GetCookerResponseModel?> GetAsync(int id)
        {
            var getCooker = await _cookerRepository.GetAsync(id);
            if (getCooker == null)
            {
                return null;
            }

            return new GetCookerResponseModel(id, getCooker.ContactId);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateCookerRequestModel request)
        {
            var updateCooker = await _cookerRepository.UpdateAsync(id, request.ContactId);

            return new UpdateResponseModel(updateCooker);
        }
    }
}
