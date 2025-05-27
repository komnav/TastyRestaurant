using System.Net;
using FluentAssertions;

namespace Restaurant.Tests.Integration.TableTests;

public class DeleteEmptyTableTests : BaseTest
{
    [Test]
    public async Task DeleteEmptyTableEndpointTest()
    {
        //Arrange //Act
        var response = await HttpClient.DeleteAsync("/Table/524");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}