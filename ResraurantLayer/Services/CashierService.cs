using Domain.Entities;
using Infrastructure.Repositories;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Cashier.Requests;
using RestaurantLayer.Dtos.Cashier.Responses;
using RestaurantLayer.Exceptions;

namespace RestaurantLayer.Services
{
    public class CashierService(ICashierRepository cashierRepository) : ICashierService
    {
        private readonly ICashierRepository _cashierRepository = cashierRepository;
        public async Task<CreateCahierResponseModel> CreateAsync(CreateCashierRequestModel request)
        {
            var addCashier = new Cashier
            {
                ContactId = request.ContactId
            };

            var rows = await _cashierRepository.CreateAsync(addCashier);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedExceptionForIntType(rows);
            }

            return new CreateCahierResponseModel(addCashier.ContactId);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _cashierRepository.DeleteAsync(id);
        }

        public async Task<List<GetCashierResponseModel>> GetAllAsync()
        {
            var getCashiers = await _cashierRepository.GetAllAsync();

            return getCashiers.Select(getCashiers => new GetCashierResponseModel(
                getCashiers.Id,
                getCashiers.ContactId)).ToList();
        }

        public async Task<GetCashierResponseModel?> GetAsync(int id)
        {
            var getCashier = await _cashierRepository.GetAsync(id);

            if (getCashier == null)
            {
                return null;
            }
            return new GetCashierResponseModel(id, getCashier.ContactId);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateCashierRequestModel request)
        {
            var updatecashier = await _cashierRepository.UpdateAsync(id, request.ContactId);

            return new UpdateResponseModel(updatecashier);
        }
    }
}
