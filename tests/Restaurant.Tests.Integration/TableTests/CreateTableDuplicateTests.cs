using System.Net;
using System.Net.Http.Json;
using Application.Dtos.Table.Requests;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration.TableTests;

public class CreateTableDuplicateTests : BaseTest
{

    [Test]
    public async Task CreateTableEndpointTest()
    {
        //Arrange
        var request = new CreateTableRequestModel(1, 10, TableType.Cabin);
        //Act
        var response = await HttpClient.PostAsJsonAsync("/Table", request);
        var response2 = await HttpClient.PostAsJsonAsync("/Table", request);
        //Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}