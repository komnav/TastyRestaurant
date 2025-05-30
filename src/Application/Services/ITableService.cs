﻿using Application.Dtos;
using Application.Dtos.Table.Requests;
using Application.Dtos.Table.Responses;

namespace Application.Services
{
    public interface ITableService
    {
        Task<CreateTableResponseModel> CreateAsync(CreateTableRequestModel request);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateTableRequestModel request);
        Task<GetTableResponseModel?> GetAsync(int id);
        Task<List<GetTableResponseModel>> GetAllAsync();
        Task<int> Delete(int id);
    }
}