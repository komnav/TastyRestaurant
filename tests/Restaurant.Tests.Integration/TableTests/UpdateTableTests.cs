using System.Net.Http.Json;
using Application.Dtos.Table.Requests;
using Domain.Enums;

namespace Restaurant.Tests.Integration.TableTests;

public class UpdateTableTests : BaseTest
{
    [Test]
    public async Task UpdateTableEndpointTest()
    {
        //Arrange
        var request = new UpdateTableRequestModel(151, 213, TableType.Table);
        //Act
        var response = await HttpClient.PutAsJsonAsync("/Table/32", request);

        //Assert
        response.EnsureSuccessStatusCode();
    }
}