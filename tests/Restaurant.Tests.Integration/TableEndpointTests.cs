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
    public async Task CreateTableEndpointTest()
    {
        //Arrange
        var createTable = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await LoginAsync("superadmin", "Admin1234$");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/Table", createTable);

        //Assert
        response.EnsureSuccessStatusCode();
        var table = await GetEntity<Table>(t =>
            t.Number == createTable.Number
            && t.Capacity == createTable.Capacity
            && t.Type == createTable.Type);

        table.Should().NotBeNull();
    }

    [Test]
    public async Task CreateDuplicateTableEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await CreateEntity(table);
        await LoginAsync("superadmin", "Admin1234$");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/Table", table);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
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
        await CreateEntity(table);
        await LoginAsync("superadmin", "Admin1234$");

        //Act
        var response = await HttpClient.DeleteAsync($"/Table/{table.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task DeleteEmptyTableEndpointTest()
    {
        //Arrange
        await LoginAsync("superadmin", "Admin1234$");
        
        //Act
        var response = await HttpClient.DeleteAsync("/Table/524");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetTableByIdEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await CreateEntity(table);

        //Act
        var response = await
            HttpClient.GetFromJsonAsync<Table>($"/Table/{table.Id}");

        //Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(table.Id);
        response.Number.Should().Be(table.Number);
        response.Capacity.Should().Be(table.Capacity);
        response.Type.Should().Be(table.Type);
    }

    [Test]
    public async Task GetTableEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await CreateEntity(table);

        //Act
        var tables = await
            HttpClient.GetFromJsonAsync<List<Table>>("/Table");

        //Assert
        tables.Should().NotBeEmpty();
    }
    
    [Test]
    public async Task UpdateTableEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await CreateEntity(table);
        await LoginAsync("superadmin", "Admin1234$");
        var request = new UpdateTableRequestModel(4, 123, TableType.Table);

        //Act
        var response = await HttpClient.PutAsJsonAsync($"/Table/{table.Id}", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var resultTable = await GetEntity<Table>(t =>
            t.Id == table.Id &&
            t.Number == request.Number &&
            t.Capacity == request.Capacity &&
            t.Type == request.Type);
        resultTable.Should().NotBeNull();
    }
    
    [Test]
    public async Task UpdateDuplicateTableEndpointTest()
    {
        //Arrange
        var firstTable = new Table
        {
            Number = 4,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await CreateEntity(firstTable);
        await LoginAsync("superadmin", "Admin1234$");

        var secondTable = new Table
        {
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        await CreateEntity(secondTable);
        var request = new UpdateTableRequestModel(74, 123, TableType.Table);

        //Act
        var response = await HttpClient.PutAsJsonAsync($"/Table/{firstTable.Id}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}