using System.Net;
using System.Net.Http.Json;
using Application.Dtos.MenuItem.Requests;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class MenuItemEndpointTests : BaseTest
{
    [Test]
    public async Task CreateMenuItemEndpointTest()
    {
        //Arrange
        var requestForCreated = new CreateMenuItemRequestModel(1, 50, "Burger5");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/MenuItem", requestForCreated);

        //Assert
        response.EnsureSuccessStatusCode();
        var menuItem = await GetEntity<MenuItem>(m =>
            m.CategoryId == requestForCreated.CategoryId
            && m.Price == requestForCreated.Price
            && m.Name == requestForCreated.Name);

        menuItem.Should().NotBeNull();
    }

    [Test]
    public async Task CreateDuplicateMenuItemEndpointTest()
    {
        //Arrange
        var requestForCreated = new CreateMenuItemRequestModel(1, 50, "Burger5");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/MenuItem", requestForCreated);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}