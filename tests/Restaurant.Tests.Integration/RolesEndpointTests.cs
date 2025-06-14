using System.Net.Http.Json;
using Application.Dtos.Account.Requests;
using Application.Dtos.Role.Requests;
using Domain.Entities;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class RolesEndpointTests : BaseTest
{
    [Test]
    public async Task RegisterUserEndpointTest()
    {
        //Arrange 
        var request = new RegisterUserRequest
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "TestUser",
            Password = "TestPassword"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("Account/Register", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var getUser = await GetEntity<User>(x =>
            x.UserName == request.UserName &&
            x.Contact!.FirstName == request.FirstName &&
            x.Contact.LastName == request.LastName
        );
        getUser.Should().NotBeNull();
    }

    [Test]
    public async Task UpdateRoleUserEndpointTest()
    {
        //Arrange
        var user = new RegisterUserRequest
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "TestUser",
            Password = "TestPassword"
        };
        await HttpClient.PostAsJsonAsync("Account/Register", user);

        var request = new UpdateRolesRequestModel(
            user.UserName,
            "Admin"
        );

        //Act
        var response = await HttpClient.PutAsJsonAsync("api/roles", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var getUser = await GetEntity<User>(x =>
            x.UserName == request.UserName &&
            x.Role == request.Role
        );
        getUser.Should().NotBeNull();
    }
}