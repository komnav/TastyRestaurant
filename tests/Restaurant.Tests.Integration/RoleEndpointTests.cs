using System.Net;
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
        await CreateRole(role);

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
        await CreateRole(role);

        //Act
        var response = await HttpClient.DeleteAsync($"/Role/{role}");

        //Assert
        response.EnsureSuccessStatusCode();
        var checkRoleToDeleted = await GetRole(role);
        checkRoleToDeleted.Should().BeNull(role);
    }

    [Test]
    public async Task DeleteEmptyRoleEndpointTest()
    {
        //Arrange
        await LoginAsync("SuperAdmin", "Admin1234$");

        //Act
        var response = await HttpClient.DeleteAsync("/Role/Cooker");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var checkRoleToDeleted = await GetRole("Cooker");
        checkRoleToDeleted.Should().BeNull();
    }

    [Test]
    public async Task GetRoleEndpointTest()
    {
        //Arrange
        string role1 = "Cooker";
        string role2 = "Waiter";
        string role3 = "Cashier";

        await LoginAsync("SuperAdmin", "Admin1234$");
        await CreateRole(role1);
        await CreateRole(role2);
        await CreateRole(role3);

        //Act
        var response = await HttpClient.GetAsync("/Role");

        //Assert
        response.EnsureSuccessStatusCode();
        var getRoles = await GetAllRole();
        getRoles.Should().HaveCount(5);
    }
}