using Domain.Entities;
using Infrastructure.Repositories;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Customer.Requests;
using RestaurantLayer.Dtos.Customer.Responses;
using RestaurantLayer.Exceptions;

namespace RestaurantLayer.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<CreateCustomerResponseModel> CreateAsync(CreateCustomerRequestModel request)
        {
            var addCustomer = new Customer
            {
                ContactId = request.ContactId
            };

            var rows = await _customerRepository.CreateAsync(addCustomer);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedExceptionForIntType(rows);
            }

            return new CreateCustomerResponseModel(addCustomer.ContactId);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<List<GetCustomerResponseModel>> GetAllAsync()
        {
            var getCustomers = await _customerRepository.GetAllAsync();

            return getCustomers.Select(getCustomers => new GetCustomerResponseModel(
                getCustomers.Id, getCustomers.ContactId))
                .ToList();
        }

        public async Task<GetCustomerResponseModel?> GetAsync(int id)
        {
            var getCustomer = await _customerRepository.GetAsync(id);

            if (getCustomer == null)
            {
                return null;
            }
            return new GetCustomerResponseModel(id, getCustomer.ContactId);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateCustomerRequestModel request)
        {
            var updateCustomer = await _customerRepository.UpdateAsync(id, request.ContactId);

            return new UpdateResponseModel(updateCustomer);
        }
    }
}
