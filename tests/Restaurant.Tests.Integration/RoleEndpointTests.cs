using System.Net.Http.Json;
using FluentAssertions;
using RestaurantLayer.Dtos.Role.Requests;

namespace Restaurant.Tests.Integration;

public class RoleEndpointTests : BaseTest
{
    [Test]
    public async Task CreateRoleEndpointTest()
    {
        //Arrange
        string roleRequest = "Cooker";

        //Act
        var response = await HttpClient.PostAsJsonAsync("/Role", roleRequest);

        //Assert
        response.EnsureSuccessStatusCode();
        var checkRoleToAdd = await GetRole(roleRequest);
        checkRoleToAdd.Should().Be(roleRequest);
    }

    [Test]
    public async Task UpdateRoleEndpointTest()
    {
        //Arrange
        const string userName = "Admin1243@gmail.com";
        const string password = "Admin1243#";
        const string role = "Cooker";

        await RegisterAsync(userName, password);
        await HttpClient.PostAsJsonAsync("/Role", role);
        var request = new UpdateRolesRequestModel(userName, role);

        //Act
        var response = await HttpClient.PutAsJsonAsync("/Role", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var checkRoleToUpdate = await GetRole(role);
        checkRoleToUpdate.Should().Be(role);
    }

    [Test]
    public async Task DeleteRoleEndpointTest()
    {
        //Arrange
        const string userName = "Admin1243@gmail.com";
        const string password = "Admin1243#";
        const string role = "Cooker";

        await RegisterAsync(userName, password);
        await HttpClient.PostAsJsonAsync("/Role", role);

        //Act
        var response = await HttpClient.DeleteAsync($"/Role{userName}/{role}");

        //Assert
        response.EnsureSuccessStatusCode();
        var checkRoleToAdd = await GetRole(role);
        checkRoleToAdd.Should().BeNull(role);
    }
}