using Domain.Entities;
using Domain.Enums;

namespace RestaurantLayer.Repositories
{
    public interface IReservationRepository
    {
        Task<int> CreateAsync(Reservation reservation);

        Task<int> UpdateAsync(int id, int tableId, int customerId, DateTimeOffset from, DateTimeOffset to, string? notes,
            ReservationStatus status);

        Task<int> DeleteAsync(int id);
        Task<Reservation?> GetAsync(int id);
        Task<List<Reservation>> GetAllAsync();
        Task<int> CancelReservation(int reservationId);
        Task<List<Reservation>> GetExistingReservations(int tableId, DateTimeOffset from, DateTimeOffset to);
    }
}