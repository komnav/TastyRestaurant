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
        var requestForCreated = new CreateMenuItemRequestModel(1, 50, "Burgdsfdser5");

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

    [Test]
    public async Task DeleteMenuItemEndpointTest()
    {
        //Act
        var response = await HttpClient.DeleteAsync($"/MenuItem/15");

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

    [Test]
    public async Task UpdateMenuItemEndpointTest()
    {
        //Assert
        var updateMenuItem = new UpdateMenuItemRequestModel(1, 34, "pizza", MenuItemStatus.NotAvailable);

        //Act
        var responseForUpdate = await HttpClient.PutAsJsonAsync("/MenuItem/17", updateMenuItem);

        //Assert
        responseForUpdate.EnsureSuccessStatusCode();
        var menuItem = await GetEntity<MenuItem>(t =>
            t.CategoryId == updateMenuItem.CategoryId &&
            t.Price == updateMenuItem.Price &&
            t.Name == updateMenuItem.Name &&
            t.Status == updateMenuItem.Status);

        menuItem.Should().NotBeNull();
    }

    [Test]
    public async Task UpdateDuplicateMenuItemEndpointTest()
    {
        //Assert
        var updateMenuItem = new UpdateMenuItemRequestModel(1, 34, "Shurbo", MenuItemStatus.NotAvailable);

        //Act
        var responseForUpdate = await HttpClient.PutAsJsonAsync("/MenuItem/12", updateMenuItem);
        var responseForUpdate2 = await HttpClient.PutAsJsonAsync("/MenuItem/17", updateMenuItem);

        //Assert
        responseForUpdate2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetMenuItemEndpointTest()
    {
        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuItem>>("/MenuItem");

        //Assert
        response.Should().NotBeNull();
    }

    [Test]
    public async Task GetMenuItemByIdEndpointTest()
    {
        //Act
        var response = await HttpClient.GetFromJsonAsync<MenuItem>("/MenuItem/12");

        //Assert
        response.CategoryId.Should().Be(1);
        response.Price.Should().Be(34);
        response.Name.Should().Be("Shurbo");
        response.Status.Should().Be(MenuItemStatus.NotAvailable);
    }

    [Test]
    public async Task GetMenuItemByCategoryIdEndpointTest()
    {
        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuItem>>("/MenuItem/GetByCategory/1");

        //Assert
        response.Should().NotBeNull();
    }
}