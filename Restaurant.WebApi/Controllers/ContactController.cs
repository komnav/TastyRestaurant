using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Contact.Requests;
using RestaurantLayer.Dtos.Contact.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Contact")]
    public class ContactController(IContactService contactService) : Controller
    {
        private readonly IContactService _contactService = contactService;

        [HttpPost]
        public async Task<CreateContactResponseModel> Create(CreateContactRequestModel request)
        {
            return await _contactService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _contactService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<GetContactResponseModel>> GetAll()
        {
            return await _contactService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetContactResponseModel?> Get(int id)
        {
            return await _contactService.GetAsync(id);
        }

        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateContactRequestModel request)
        {
            return await _contactService.UpdateAsync(id, request);
        }
    }
}
