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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var effectedRows = await _reservationService.DeleteAsync(id);
            if (effectedRows > 0)
                return Ok();
            return NotFound();
        }

        [HttpGet]
        public async Task<List<GetReservationResponseModel>> GetAll(int page = 1, int pageSize = 10)
        {
            return await _reservationService.GetAllAsync(page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<GetReservationResponseModel?> Get(int id)
        {
            return await _reservationService.GetAsync(id);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<UpdateResponseModel> Update(int id, UpdateReservationRequestModel request)
        {
            return await _reservationService.UpdateAsync(id, request);
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var effectedRows = await _reservationService.CancelReservationAsync(id);
            if (effectedRows > 0)
                return Ok();
            return NotFound();
        }
    }
}