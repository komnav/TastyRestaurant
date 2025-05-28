using System.Net;
using System.Net.Http.Json;
using Application.Dtos.MenuCategory.Requests;
using Domain.Entities;
using FluentAssertions;

namespace Restaurant.Tests.Integration;

public class MenuCategoryEndpointTests : BaseTest
{
    [Test]
    public async Task CreateMenuCategoryEndpointTest()
    {
        //Arrange
        var request = new CreateMenuCategoryRequestModel("18067-ium");

        //Act
        var response = await HttpClient.PostAsJsonAsync("MenuCategory", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var menuCategory = await GetEntity<MenuCategory>(c => c.Name == request.Name);
        menuCategory.Should().NotBeNull();
    }

    [Test]
    public async Task CreateDuplicateMenuCategoryEndpointTest()
    {
        //Arrange
        var requestForCreated = new CreateMenuCategoryRequestModel("18067-ium");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/MenuCategory", requestForCreated);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task DeleteMenuCategoryEndpointTest()
    {
        //Act
        var response = await HttpClient.DeleteAsync("/MenuCategory/7");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task DeleteEmptyMenuCategoryEndpointTest()
    {
        //Act
        var response = await HttpClient.DeleteAsync($"/MenuCategory/123");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task UpdateMenuCategoryEndpointTest()
    {
        //Assert
        var updateMenuCategory = new UpdateMenuCategoryRequestModel("10-um", 1);

        //Act
        var responseForUpdate = await HttpClient.PutAsJsonAsync("/MenuCategory/1", updateMenuCategory);

        //Assert
        responseForUpdate.EnsureSuccessStatusCode();
        var menuCategory = await GetEntity<MenuCategory>(t =>
            t.Name == updateMenuCategory.Name &&
            t.ParentId == updateMenuCategory.ParentId);

        menuCategory.Should().NotBeNull();
    }

    [Test]
    public async Task UpdateDuplicateMenuCategoryEndpointTest()
    {
        //Assert
        var updateMenuCategory = new UpdateMenuCategoryRequestModel("10-um", 1);

        //Act
        var responseForUpdate = await HttpClient.PutAsJsonAsync("/MenuCategory/2", updateMenuCategory);

        //Assert
        responseForUpdate.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetMenuCategoryEndpointTest()
    {
        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuCategory>>("/MenuCategory");

        //Assert
        response.Should().NotBeNull();
    }

    [Test]
    public async Task GetMenuItemByIdEndpointTest()
    {
        //Act
        var response = await HttpClient.GetFromJsonAsync<MenuCategory>("/MenuCategory/2");

        //Assert
        response.ParentId.Should().Be(null);
        response.Name.Should().Be("Test123");
    }
}