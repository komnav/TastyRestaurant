using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

namespace Infrastructure.Repositories
{
    public class ReservationRepository(ApplicationDbContext dbContext) : IReservationRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<int> CreateAsync(Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Reservations
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Reservation>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _dbContext.Reservations
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Reservation?> GetAsync(int id)
        {
            return await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync
        (int id, int tableId, int customerId, DateTimeOffset from, DateTimeOffset to, string? notes,
            ReservationStatus status)
        {
            return await _dbContext.Reservations
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.TableId, tableId)
                    .SetProperty(x => x.UserId, customerId)
                    .SetProperty(x => x.From, from)
                    .SetProperty(x => x.To, to)
                    .SetProperty(x => x.Notes, notes)
                    .SetProperty(x => x.Status, status));
        }

        public async Task<int> CancelReservation(int reservationId)
        {
            return await _dbContext.Reservations
                .Where(x => x.Id == reservationId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.Status, ReservationStatus.Cancelled));
        }

        public async Task<List<Reservation>> GetExistingReservations(
            int? tableId,
            DateTimeOffset from,
            DateTimeOffset to)
        {
            return await _dbContext.Reservations
                .Where(x => x.TableId == tableId &&
                            x.From <= to && x.To >= from)
                .ToListAsync();
        }
    }
}