using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Reservation.Requests;
using RestaurantLayer.Dtos.Reservation.Responses;

namespace RestaurantLayer.Services
{
    public interface IReservationService
    {
        Task<CreateReservationResponseModel> CreateAsync(CreateReservationRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateReservationRequestModel request);
        Task<int> DeletAsync(int id);
        Task<GetReservationResponseModel?> GetAsync(int id);
        Task<List<GetReservationResponseModel>> GetAllAsync();
    }
}
