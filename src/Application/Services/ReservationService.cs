using Application.Dtos;
using Application.Dtos.Reservation.Requests;
using Application.Dtos.Reservation.Responses;
using Application.Exceptions;
using Domain.Entities;
using RestaurantLayer.Exceptions;
using RestaurantLayer.Repositories;

namespace Application.Services
{
    public class ReservationService(
        IReservationRepository reservationRepository)
        : IReservationService
    {
        private readonly IReservationRepository _reservationRepository = reservationRepository;

        public async Task<CreateReservationResponseModel> CreateAsync(CreateReservationRequestModel request)
        {
            var addReservation = new Reservation
            {
                UserId = request.CustomerId,
                TableId = request.TableId,
                From = new DateTimeOffset
                (
                    request.From.Year,
                    request.From.Month,
                    request.From.Day,
                    request.From.Hour,
                    request.From.Minute,
                    0,
                    request.From.Offset
                ),
                To = new DateTimeOffset
                (
                    request.To.Year,
                    request.To.Month,
                    request.To.Day,
                    request.To.Hour,
                    request.To.Minute,
                    0,
                    request.To.Offset
                ),
                Notes = request.Notes
            };

            var getExistingReservationsList =
                await GetExistingReservations(addReservation.TableId, addReservation.From, addReservation.To);

            var checkForDuplicate = getExistingReservationsList.Where(x =>
                x.From < request.To && x.To > request.From);
            if (checkForDuplicate is not null)
            {
                throw new ResourceAlreadyExistException(nameof(addReservation));
            }

            var rows = await _reservationRepository.CreateAsync(addReservation);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(addReservation));
            }

            return new CreateReservationResponseModel(
                addReservation.Id,
                addReservation.TableId,
                addReservation.UserId,
                new DateTimeOffset
                (
                    addReservation.From.Year,
                    addReservation.From.Month,
                    addReservation.From.Day,
                    addReservation.From.Hour,
                    addReservation.From.Minute,
                    0,
                    request.From.Offset
                ),
                new DateTimeOffset
                (
                    addReservation.To.Year,
                    addReservation.To.Month,
                    addReservation.To.Day,
                    addReservation.To.Hour,
                    addReservation.To.Minute,
                    0,
                    request.To.Offset
                ),
                addReservation.Notes,
                addReservation.Status);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _reservationRepository.DeleteAsync(id);
        }

        public async Task<List<GetReservationResponseModel>> GetAllAsync()
        {
            var getReservations = await _reservationRepository.GetAllAsync();

            return getReservations.Select(reservations => new GetReservationResponseModel(
                reservations.Id,
                reservations.TableId,
                reservations.UserId,
                new DateTimeOffset
                (
                    reservations.From.Year,
                    reservations.From.Month,
                    reservations.From.Day,
                    reservations.From.Hour,
                    reservations.From.Minute,
                    0,
                    reservations.From.Offset
                ),
                new DateTimeOffset
                (
                    reservations.To.Year,
                    reservations.To.Month,
                    reservations.To.Day,
                    reservations.To.Hour,
                    reservations.To.Minute,
                    0,
                    reservations.To.Offset
                ),
                reservations.Notes,
                reservations.Status
            )).ToList();
        }

        public async Task<int> CancelReservationAsync(int reservationId)
        {
            return await _reservationRepository.CancelReservation(reservationId);
        }

        public async Task<List<GetExistingReservations>> GetExistingReservations(int tableId, DateTimeOffset from,
            DateTimeOffset to)
        {
            var getExistingReservations = await _reservationRepository.GetExistingReservations(tableId, from, to);
            return getExistingReservations.Select(reservations => new GetExistingReservations(
                reservations.TableId,
                new DateTimeOffset
                (
                    reservations.From.Year,
                    reservations.From.Month,
                    reservations.From.Day,
                    reservations.From.Hour,
                    reservations.From.Minute,
                    0,
                    reservations.From.Offset
                ),
                new DateTimeOffset
                (
                    reservations.To.Year,
                    reservations.To.Month,
                    reservations.To.Day,
                    reservations.To.Hour,
                    reservations.To.Minute,
                    0,
                    reservations.To.Offset
                ))).ToList();
        }

        public async Task<GetReservationResponseModel?> GetAsync(int id)
        {
            var getReservation = await _reservationRepository.GetAsync(id);

            if (getReservation == null)
            {
                return null;
            }

            return new GetReservationResponseModel(
                getReservation.Id,
                getReservation.TableId,
                getReservation.UserId, new DateTimeOffset
                (
                    getReservation.From.Year,
                    getReservation.From.Month,
                    getReservation.From.Day,
                    getReservation.From.Hour,
                    getReservation.From.Minute,
                    0,
                    getReservation.From.Offset
                ),
                new DateTimeOffset
                (
                    getReservation.To.Year,
                    getReservation.To.Month,
                    getReservation.To.Day,
                    getReservation.To.Hour,
                    getReservation.To.Minute,
                    0,
                    getReservation.To.Offset
                ),
                getReservation.Notes,
                getReservation.Status
            );
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateReservationRequestModel request)
        {
            var getExistingReservationsList =
                await GetExistingReservations(request.TableId, request.From, request.To);

            var checkForDuplicate = getExistingReservationsList.Where(x =>
                x.From < request.To && x.To > request.From);
            if (checkForDuplicate is not null)
            {
                throw new ResourceAlreadyExistException(nameof(request));
            }

            var updateReservation = await _reservationRepository
                .UpdateAsync(
                    id,
                    request.TableId,
                    request.CustomerId,
                    new DateTimeOffset
                    (
                        request.From.Year,
                        request.From.Month,
                        request.From.Day,
                        request.From.Hour,
                        request.From.Minute,
                        0,
                        request.From.Offset
                    ),
                    new DateTimeOffset(
                        request.To.Year,
                        request.To.Month,
                        request.To.Day,
                        request.To.Hour,
                        request.To.Minute,
                        0,
                        request.To.Offset
                    ),
                    request.Notes,
                    request.Status);
            if (updateReservation <= 0)
            {
                throw new ResourceWasNotUpdatedException(nameof(updateReservation));
            }

            return new UpdateResponseModel(updateReservation);
        }
    }
}