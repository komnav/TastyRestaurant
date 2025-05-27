using System.Net;
using System.Net.Http.Json;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;


namespace Restaurant.Tests.Integration;

public class DeleteTableTests : BaseTest
{
    [Test]
    public async Task DeleteTableEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Id = 64,
            Number = 74,
            Capacity = 213,
            Type = TableType.Cabin
        };
        //Act
        var create = await HttpClient.PostAsJsonAsync("/Table", table);
        var response = await HttpClient.DeleteAsync("/Table/134");
        //Assert
        response.EnsureSuccessStatusCode();
    }
}