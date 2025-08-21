using System.Net;
using System.Net.Http.Json;
using Application.Dtos.Role.Requests;
using Domain.Entities;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class UserRolesEndpointTests : BaseTest
{
    const string SuperAdmin = "SuperAdmin";
    const string Password = "Admin1234$";
    
    [Test]
    public async Task LinkRoleToUserEndpointTest()
    {
        //Arrange
        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        string role = "Cooker";
        await CreateRole(role);

        var request = new LinkRoleToUserRequestModel(user.Id, role);
        await LoginAsync("SuperAdmin", "Admin1234$");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/UserRoles", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var getUserRole = await GetUserRoles(user.Id);
        getUserRole.Should().HaveCount(1);
    }

    [Test]
    public async Task UnLinkRoleToUserEndpointTest()
    {
        //Arrange
        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        string role = "Cooker";
        await CreateRole(role);

        await LoginAsync(SuperAdmin, Password);

        var request = new LinkRoleToUserRequestModel(user.Id, role);
        await HttpClient.PostAsJsonAsync("/UserRoles", request);

        //Act
        var response = await HttpClient.DeleteAsync($"/UserRoles/{user.Id}/{role}");

        //Assert
        response.EnsureSuccessStatusCode();
        var getUserRole = await GetUserRoles(user.Id);
        getUserRole.Should().BeEmpty();
    }

    [Test]
    public async Task UnLinkEmptyRoleToUserEndpointTest()
    {
        //Arrange
        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        await LoginAsync(SuperAdmin, Password);

        //Act
        var response = await HttpClient.DeleteAsync($"/UserRoles/{user.Id}/Cooker");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var getUserRole = await GetUserRoles(user.Id);
        getUserRole.Should().BeEmpty();
    }

    [Test]
    public async Task GetUserRolesEndpointTest()
    {
        //Arrange
        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        string role = "Cooker";
        await CreateRole(role);

        await LoginAsync(SuperAdmin, Password);

        var request = new LinkRoleToUserRequestModel(user.Id, role);
        await HttpClient.PostAsJsonAsync("/UserRoles", request);

        //Act
        var response = await HttpClient.GetAsync($"/UserRoles/{user.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        var getUserRole = await GetUserRoles(user.Id);
        getUserRole.Should().HaveCount(1);
    }
}