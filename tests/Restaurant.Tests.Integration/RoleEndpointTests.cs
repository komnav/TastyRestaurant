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
        await LoginAsync("SuperAdmin", "Admin1234$");

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
        string role = "Cooker";
        string newName = "Waiter";

        await LoginAsync("SuperAdmin", "Admin1234$");
        await HttpClient.PostAsJsonAsync("/Role", role);

        var request = new UpdateRolesRequestModel(role, newName);

        //Act
        var response = await HttpClient.PutAsJsonAsync("/Role", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var checkRoleToUpdate = await GetRole(newName);
        checkRoleToUpdate.Should().Be(newName);
    }

    [Test]
    public async Task DeleteRoleEndpointTest()
    {
        //Arrange
        string role = "Cooker";

        await LoginAsync("SuperAdmin", "Admin1234$");
        await HttpClient.PostAsJsonAsync("/Role", role);

        //Act
        var response = await HttpClient.DeleteAsync($"/Role/{role}");

        //Assert
        response.EnsureSuccessStatusCode();
        var checkRoleToAdd = await GetRole(role);
        checkRoleToAdd.Should().BeNull(role);
    }
}