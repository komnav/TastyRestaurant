using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public interface IReservationRepository
    {
        Task<int> CreateAsync(Reservation reservation);
        Task<int> UpdateAsync(int id, int tableId, int customerId, DateTime from, DateTime to, string notes, ReservationStatus status);
        Task<int> DeleteAsync(int id);
        Task<Reservation?> GetAsync(int id);
        Task<List<Reservation>> GetAllAsync();
    }
}
