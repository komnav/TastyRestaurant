using System.Net.Http.Json;
using Application.Dtos.Table.Requests;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class CreateTableTests : BaseTest
{
    [Test]
    public async Task CreateTableEndpointTest()
    {
        //Arrange
        var request = new CreateTableRequestModel(1, 10, TableType.Cabin);

        //Act
        var response = await _httpClient.PostAsJsonAsync("/Table", request);

        //Assert
        response.EnsureSuccessStatusCode();

        var table = await GetEntity<Table>(t =>
            t.Number == request.Number 
            && t.Capacity == request.Capacity
            && t.Type == request.Type);
        
        table.Should().NotBeNull();
    }
}