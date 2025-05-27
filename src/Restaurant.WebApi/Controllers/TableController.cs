using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Table.Requests;
using Application.Dtos.Table.Responses;
using Application.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Table")]
    public class TableController(ITableService tableService) : Controller
    {
        private readonly ITableService _tableService = tableService;

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<CreateTableResponseModel> Create([FromBody] CreateTableRequestModel request)
        {
            return await _tableService.CreateAsync(request);
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var affectedCount = await _tableService.Delete(id);
            if (affectedCount > 0)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<GetTableResponseModel?> Get([FromRoute] int id)
        {
            return await _tableService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<GetTableResponseModel>> GetAll()
        {
            return await _tableService.GetAllAsync();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<UpdateResponseModel> Update(int id, UpdateTableRequestModel request)
        {
            return await _tableService.UpdateAsync(id, request);
        }
    }
}