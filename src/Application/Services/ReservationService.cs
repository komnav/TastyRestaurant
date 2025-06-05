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
                From = request.From.AddSeconds(-request.From.Second),
                To = request.To.AddSeconds(-request.To.Second),
                Notes = request.Notes
            };

            var checkForDuplicate = await GetExistingReservations(request.TableId, request.From, request.To);
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
                addReservation.From.AddSeconds(-addReservation.From.Second),
                addReservation.To.AddSeconds(-addReservation.To.Second),
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
                getReservations.From.AddSeconds(-getReservations.From.Second),
                getReservations.To.AddSeconds(-getReservations.To.Second),
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
                getReservation.UserId,
                getReservation.From.AddSeconds(-getReservation.From.Second),
                getReservation.To.AddSeconds(-getReservation.To.Second),
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
                    request.From.AddSeconds(-request.From.Second),
                    request.To.AddSeconds(-request.To.Second), request.Notes,
                    request.Status);
            if (updateReservation <= 0)
            {
                throw new ResourceWasNotUpdatedException(nameof(updateReservation));
            }

            return new UpdateResponseModel(updateReservation);
        }
    }
}