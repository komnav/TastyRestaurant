using System.Net.Http.Json;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration.TableTests;

public class GetTableByIdTests : BaseTest
{
    [Test]
    public async Task GetTableByIdEndpointTest()
    {
        //Arrange
        //Act
        var table = await
            HttpClient.GetFromJsonAsync<Table>("/Table/32");
        //Assert
        table.Number.Should().Be(1);
        table.Capacity.Should().Be(10);
        table.Type.Should().Be(TableType.Table);
    }
}