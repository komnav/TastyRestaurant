using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Reservation.Requests;
using Application.Dtos.Reservation.Responses;
using Application.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Reservation")]
    public class ReservationController(IReservationService reservationService) : Controller
    {
        private readonly IReservationService _reservationService = reservationService;


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateReservationResponseModel> Create(CreateReservationRequestModel request)
        {
            return await _reservationService.CreateAsync(request);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _reservationService.DeletAsync(id);
        }

        [HttpGet]
        public async Task<List<GetReservationResponseModel>> GetAll()
        {
            return await _reservationService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetReservationResponseModel?> Get(int id)
        {
            return await _reservationService.GetAsync(id);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<UpdateResponseModel> Update(int id, UpdateReservationRequestModel request)
        {
            return await _reservationService.UpdateAsync(id, request);
        }
    }
}
