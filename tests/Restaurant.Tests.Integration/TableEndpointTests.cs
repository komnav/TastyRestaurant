using System.Net;
using System.Net.Http.Json;
using Application.Dtos.Table.Requests;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class TableEndpointTests : BaseTest
{
    [Test]
    public async Task CreateDuplicateTableEndpointTest()
    {
        //Arrange
        var request = new CreateTableRequestModel(1, 10, TableType.Cabin);

        //Act
        var response = await HttpClient.PostAsJsonAsync("/Table", request);
        var response2 = await HttpClient.PostAsJsonAsync("/Table", request);

        //Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task CreateTableEndpointTest()
    {
        //Arrange
        var request = new CreateTableRequestModel(1234, 10, TableType.Cabin);

        //Act
        var response = await HttpClient.PostAsJsonAsync("/Table", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var table = await GetEntity<Table>(t =>
            t.Number == request.Number
            && t.Capacity == request.Capacity
            && t.Type == request.Type);

        table.Should().NotBeNull();
    }

    [Test]
    public async Task DeleteEmptyTableEndpointTest()
    {
        //Act
        var response = await HttpClient.DeleteAsync("/Table/524");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task DeleteTableEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };

        //Act
        var create = await HttpClient.PostAsJsonAsync("/Table", table);
        var response = await HttpClient.DeleteAsync("/Table/36");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task GetTableByIdEndpointTest()
    {
        //Act
        var table = await
            HttpClient.GetFromJsonAsync<Table>("/Table/32");

        //Assert
        table.Number.Should().Be(1);
        table.Capacity.Should().Be(10);
        table.Type.Should().Be(TableType.Table);
    }

    [Test]
    public async Task GetTableEndpointTest()
    {
        //Act
        var tables = await
            HttpClient.GetFromJsonAsync<List<Table>>("/Table");

        //Assert
        tables.Should().NotBeEmpty();
    }

    [Test]
    public async Task UpdateDuplicateTableEndpointTest()
    {
        //Arrange
        var request = new UpdateTableRequestModel(121, 213, TableType.Table);

        //Act
        var response = await HttpClient.PutAsJsonAsync("/Table/32", request);
        var response2 = await HttpClient.PutAsJsonAsync("/Table/33", request);

        //Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task UpdateTableEndpointTest()
    {
        //Arrange
        var request = new UpdateTableRequestModel(121, 213, TableType.Table);

        //Act
        var response2 = await HttpClient.PutAsJsonAsync("/Table/33", request);

        //Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}