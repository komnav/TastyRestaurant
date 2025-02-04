using Microsoft.AspNetCore.Mvc;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Table.Requests;
using RestaurantLayer.Dtos.Table.Responses;
using RestaurantLayer.Services;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("Table")]
    public class TableController(ITableService tableService) : Controller
    {
        private readonly ITableService _tableService = tableService;

        [HttpPost]
        public async Task<CreateTableResponseModel> Create([FromBody] CreateTableRequestModel request)
        {
            return await _tableService.CreateAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _tableService.Delete(id);
            return Ok();
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

        [HttpPut("{id}")]
        public async Task<UpdateResponseModel> Update(int id, UpdateTableRequestModel request)
        {
            var updateTable = await _tableService.UpdateAsync(id, request);
            return updateTable;
        }
    }
}
