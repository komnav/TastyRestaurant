using Domain.Entities;
using Infrastructure.Repositories;
using ResrautantLayer.Exceptions;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Contact.Requests;
using RestaurantLayer.Dtos.Contact.Responses;

namespace RestaurantLayer.Services
{
    public class ContactService(IContactRepository contactRepository) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        public async Task<CreateContactResponseModel> CreateAsync(CreateContactRequestModel request)
        {
            var addContact = new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PassportSeries = request.PassportSeries,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
            };

            var rows = await _contactRepository.CreateAsync(addContact);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(addContact));
            }

            return new CreateContactResponseModel(
                addContact.Id,
                addContact.FirstName,
                addContact.LastName,
                addContact.Email,
                addContact.PassportSeries,
                addContact.Address,
                addContact.PhoneNumber);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _contactRepository.DeleteAsync(id);
        }

        public async Task<List<GetContactResponseModel>> GetAllAsync()
        {
            var getContacts = await _contactRepository.GetAllAsync();

            return getContacts.Select(getContacts => new GetContactResponseModel(
                getContacts.Id,
                getContacts.FirstName,
                getContacts.LastName,
                getContacts.Email,
                getContacts.PassportSeries,
                getContacts.Address,
                getContacts.PhoneNumber)).ToList();
        }

        public async Task<GetContactResponseModel?> GetAsync(int id)
        {
            var getContact = await _contactRepository.GetAsync(id);

            if (getContact == null)
            {
                return null;
            }

            return new GetContactResponseModel(
                getContact.Id,
                getContact.FirstName,
                getContact.LastName,
                getContact.Email,
                getContact.PassportSeries,
                getContact.Address,
                getContact.PhoneNumber);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateContactRequestModel request)
        {
            var updateContact = await _contactRepository.UpdateAsync(id, request.FirstName, request.LastName, request.Email, request.PassportSeries, request.Address, request.PhoneNumber);

            return new UpdateResponseModel(updateContact);
        }
    }
}
