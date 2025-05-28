using Application.Dtos;
using Application.Dtos.Table.Requests;
using Application.Dtos.Table.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using ResrautantLayer.Exceptions;

namespace Application.Services
{
    public class TableService(ITableRepository tableRepository) : ITableService
    {
        private readonly ITableRepository _tableRepository = tableRepository;

        public async Task<CreateTableResponseModel> CreateAsync(CreateTableRequestModel request)
        {
            var table = new Table
            {
                Number = request.Number,
                Capacity = request.Capacity,
                Type = request.Type
            };
            var rows = await _tableRepository.CreateAsync(table);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(table));
            }

            return new CreateTableResponseModel(table.Id, table.Number, table.Capacity, table.Type);
        }

        public async Task<int> Delete(int id)
        {
            return await _tableRepository.DeleteAsync(id);
        }

        public async Task<List<GetTableResponseModel>> GetAllAsync()
        {
            var table = await _tableRepository.GetAllAsync();

            return table.Select(table => new GetTableResponseModel(
                table.Id,
                table.Number,
                table.Capacity,
                table.Type
            )).ToList();
        }

        public async Task<GetTableResponseModel?> GetAsync(int id)
        {
            var table = await _tableRepository.GetAsync(id);

            if (table == null)
            {
                return null;
            }

            return new GetTableResponseModel(id, table.Number, table.Capacity, table.Type);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateTableRequestModel request)
        {
            var table = await _tableRepository.UpdateAsync(id, request.Number, request.Capacity, request.Type);
            return new UpdateResponseModel(table);
        }
    }
}