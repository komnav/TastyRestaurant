using Domain.Entities;
using Infrastructure.Repositories;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Waiter.Requests;
using RestaurantLayer.Dtos.Waiter.Responses;
using RestaurantLayer.Exceptions;

namespace RestaurantLayer.Services
{
    public class WaiterService(IWaiterRepository waiterRepository) : IWaiterService
    {
        private readonly IWaiterRepository _waiterRepository = waiterRepository;
        public async Task<CreateWaiterResponseModel> CreateAsync(CreateWaiterRequestModel request)
        {
            var addWaiter = new Waiter
            {
                ContactId = request.ContactId
            };

            var rows = await _waiterRepository.CreateAsync(addWaiter);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedExceptionForIntType(rows);
            }

            return new CreateWaiterResponseModel(addWaiter.ContactId);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _waiterRepository.DeleteAsync(id);
        }

        public async Task<List<GetWaiterResponseModel>> GetAllAsync()
        {
            var getWaiters = await _waiterRepository.GetAllAsync();

            return getWaiters.Select(getWaiters => new GetWaiterResponseModel(
                getWaiters.Id,
                getWaiters.ContactId
                )).ToList();
        }

        public async Task<GetWaiterResponseModel?> GetAsync(int id)
        {
            var getWaiter = await _waiterRepository.GetAsync(id);

            if (getWaiter == null)
            {
                return null;
            }
            return new GetWaiterResponseModel(getWaiter.Id, getWaiter.ContactId);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateWaiterRequestModel request)
        {
            var updateWaiter = await _waiterRepository.UpdateAsync(id, request.ContactId);

            return new UpdateResponseModel(updateWaiter);
        }
    }
}
