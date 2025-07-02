using Application.Dtos.Reservation.Requests;
using Application.Dtos.Reservation.Responses;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Reservation.Requests;

namespace RestaurantLayer.Services
{
    public interface IReservationService
    {
        Task<CreateReservationResponseModel> CreateAsync(CreateReservationRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateReservationRequestModel request);
        Task<int> DeleteAsync(int id);
        Task<GetReservationResponseModel?> GetAsync(int id);
        Task<List<GetReservationResponseModel>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<int> CancelReservationAsync(int reservationId);
    }
}