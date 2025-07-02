using Application.Dtos.Reservation.Requests;
using Application.Dtos.Reservation.Responses;
using Domain.Entities;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Reservation;
using RestaurantLayer.Dtos.Reservation.Requests;
using RestaurantLayer.Exceptions;
using RestaurantLayer.Repositories;

namespace RestaurantLayer.Services
{
    public class ReservationService(
        IReservationRepository reservationRepository)
        : IReservationService
    {
        private readonly IReservationRepository _reservationRepository = reservationRepository;

        public async Task<CreateReservationResponseModel> CreateAsync(CreateReservationRequestModel request)
        {
            NormalizeDates(request);

            var existingReservations =
                await _reservationRepository.GetExistingReservations(request.TableId, request.From, request.To);
            if (existingReservations.Count > 0)
            {
                throw new ResourceAlreadyExistException(nameof(Reservation), request.TableId, request.From, request.To);
            }

            var addReservation = new Reservation
            {
                UserId = request.CustomerId,
                TableId = request.TableId,
                From = request.From,
                To = request.To,
                Notes = request.Notes
            };

            var rows = await _reservationRepository.CreateAsync(addReservation);

            if (rows <= 0)
                throw new ResourceWasNotCreatedException(nameof(addReservation));

            return new CreateReservationResponseModel(
                addReservation.Id,
                addReservation.TableId,
                addReservation.UserId,
                addReservation.From,
                addReservation.To,
                addReservation.Notes,
                addReservation.Status);
        }

        private void NormalizeDates<T>(T request) where T :
            class, IDateTimeDto
        {
            request.From = new DateTimeOffset
            (
                request.From.Year,
                request.From.Month,
                request.From.Day,
                request.From.Hour,
                request.From.Minute,
                0,
                request.From.Offset
            );
            request.To = new DateTimeOffset
            (
                request.To.Year,
                request.To.Month,
                request.To.Day,
                request.To.Hour,
                request.To.Minute,
                0,
                request.To.Offset
            );
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _reservationRepository.DeleteAsync(id);
        }

        public async Task<List<GetReservationResponseModel>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var getReservations = await _reservationRepository.GetAllAsync(page, pageSize);

            return getReservations.Select(reservations => new GetReservationResponseModel(
                    reservations.Id,
                    reservations.TableId,
                    reservations.UserId,
                    reservations.From,
                    reservations.To,
                    reservations.Notes,
                    reservations.Status
                ))
                .ToList();
        }

        public async Task<int> CancelReservationAsync(int reservationId)
        {
            return await _reservationRepository.CancelReservation(reservationId);
        }

        public async Task<GetReservationResponseModel?> GetAsync(int id)
        {
            var getReservation = await _reservationRepository.GetAsync(id);

            if (getReservation == null)
                return null;

            return new GetReservationResponseModel(
                getReservation.Id,
                getReservation.TableId,
                getReservation.UserId,
                getReservation.From,
                getReservation.To,
                getReservation.Notes,
                getReservation.Status
            );
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateReservationRequestModel request)
        {
            NormalizeDates(request);

            var existingReservations =
                await _reservationRepository.GetExistingReservations(request.TableId, request.From, request.To);

            if (existingReservations.Count > 0)
            {
                throw new ResourceAlreadyExistException(nameof(Reservation),
                    request.TableId, request.From, request.To);
            }

            var updateReservation = await _reservationRepository
                .UpdateAsync(
                    id,
                    request.TableId,
                    request.CustomerId,
                    request.From,
                    request.To,
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