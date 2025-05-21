using Application.Dtos;
using Application.Dtos.Reservation.Requests;
using Application.Dtos.Reservation.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using ResrautantLayer.Exceptions;

namespace Application.Services
{
    public class ReservationService(IReservationRepository reservationRepository) : IReservationService
    {
        private readonly IReservationRepository _reservationRepository = reservationRepository;
        public async Task<CreateReservationResponseModel> CreateAsync(CreateReservationRequestModel request)
        {
            var addReservation = new Reservation
            {
                UserId = request.CustomerId,
                TableId = request.TableId,
                From = request.From,
                To = request.To,
                Notes = request.Notes,
                Status = request.Status
            };

            var rows = await _reservationRepository.CreateAsync(addReservation);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(addReservation));
            }

            return new CreateReservationResponseModel(
                addReservation.Id,
                addReservation.TableId,
                addReservation.UserId,
                addReservation.From,
                addReservation.To,
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
                getReservations.From,
                getReservations.To,
                getReservations.Notes,
                getReservations.Status
                )).ToList();
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
                getReservation.From,
                getReservation.To,
                getReservation.Notes,
                getReservation.Status
                );
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateReservationRequestModel request)
        {
            var updateReservation = await _reservationRepository
                .UpdateAsync(id, request.TableId, request.CustomerId, request.From, request.To, request.Notes, request.Status);

            return new UpdateResponseModel(updateReservation);
        }
    }
}
