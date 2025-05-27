using System.Net.Http.Json;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;


namespace Restaurant.Tests.Integration;

public class GetTableTests : BaseTest
{
    [Test]
    public async Task GetTableEndpointTest()
    {
        //Arrange
        //Act
        var tables = await
            HttpClient.GetFromJsonAsync<List<Table>>("/Table");
        //Assert
        tables.Should().NotBeEmpty();
    }
}