using System.Net;
using System.Net.Http.Json;
using Application.Dtos.MenuItem.Requests;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class MenuItemEndpointTests : BaseTest
{
    [Ignore("Success")]
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

    [Ignore("Success")]
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

    [Ignore("Success")]
    [Test]
    public async Task DeleteMenuItemEndpointTest()
    {
        //Arrange
        var menuItem = new MenuItem
        {
            CategoryId = 1,
            Price = 100,
            Name = "Shurbo"
        };

        //Act
        var create = await HttpClient.PostAsJsonAsync("/MenuItem", menuItem);
        var response = await HttpClient.DeleteAsync($"/MenuItem/{menuItem.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task DeleteEmptyMenuItemEndpointTest()
    {
        //Act
        var response = await HttpClient.DeleteAsync($"/MenuItem/123");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}