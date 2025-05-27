using System.Net;
using System.Net.Http.Json;
using Application.Dtos.Table.Requests;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration.TableTests;

public class UpdateTableDuplicateTests : BaseTest
{
    [Test]
    public async Task UpdateTableEndpointTest()
    {
        //Arrange
        var request = new UpdateTableRequestModel(121, 213, TableType.Table);
        //Act
        var response = await HttpClient.PutAsJsonAsync("/Table/32", request);
        var response2 = await HttpClient.PutAsJsonAsync("/Table/32", request);

        //Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}