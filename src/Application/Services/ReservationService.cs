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

            var checkForDuplicate =
                await GetExistingReservations(addReservation.TableId, addReservation.From, addReservation.To);
            if (!checkForDuplicate)
            {
                throw new ResourceAlreadyExist(nameof(addReservation));
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

        public async Task<int> DeletAsync(int id)
        {
            return await _reservationRepository.DeleteAsync(id);
        }

        public async Task<List<GetReservationResponseModel>> GetAllAsync()
        {
            var getReservations = await _reservationRepository.GetAllAsync();

            return getReservations.Select(getReservations => new GetReservationResponseModel(
                getReservations.Id,
                getReservations.TableId,
                getReservations.UserId,
                new DateTimeOffset
                (
                    getReservations.From.Year,
                    getReservations.From.Month,
                    getReservations.From.Day,
                    getReservations.From.Hour,
                    getReservations.From.Minute,
                    0,
                    getReservations.From.Offset
                ),
                new DateTimeOffset
                (
                    getReservations.To.Year,
                    getReservations.To.Month,
                    getReservations.To.Day,
                    getReservations.To.Hour,
                    getReservations.To.Minute,
                    0,
                    getReservations.To.Offset
                ),
                getReservations.Notes,
                getReservations.Status
            )).ToList();
        }

        public async Task<int> CancelReservationAsync(int reservationId)
        {
            return await _reservationRepository.CancelReservation(reservationId);
        }

        public async Task<bool> GetExistingReservations(int tableId, DateTimeOffset from,
            DateTimeOffset to)
        {
            var checkExistingReservations = await _reservationRepository.GetExistingReservations(tableId, from, to);
            return checkExistingReservations.Count == 0;
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
            var checkForDuplicate = await GetExistingReservations(request.TableId, request.From, request.To);
            if (!checkForDuplicate)
            {
                throw new ResourceAlreadyExist(nameof(request));
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