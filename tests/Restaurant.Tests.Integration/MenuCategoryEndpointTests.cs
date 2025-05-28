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
        await HttpClient.PostAsJsonAsync("/MenuCategory", requestForCreated);
        var response2 = await HttpClient.PostAsJsonAsync("/MenuCategory", requestForCreated);

        //Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task DeleteMenuCategoryEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        //Act
        var response = await HttpClient.DeleteAsync($"/MenuCategory/{menuCategory.Id}");

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
        var createMenuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(createMenuCategory);
        var updateMenuCategory = new UpdateMenuCategoryRequestModel("10-um", 1);

        //Act
        var responseForUpdate =
            await HttpClient.PutAsJsonAsync($"/MenuCategory/{createMenuCategory.Id}", updateMenuCategory);

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
        var createMenuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(createMenuCategory);
        var createMenuCategory2 = new MenuCategory
        {
            Name = "Shula2",
            ParentId = 1
        };
        await CreateEntity(createMenuCategory2);
        var updateMenuCategory = new UpdateMenuCategoryRequestModel("11-um", 1);

        //Act
        var responseForUpdate =
            await HttpClient.PutAsJsonAsync($"/MenuCategory/{createMenuCategory.Id}", updateMenuCategory);
        var responseForUpdate2 =
            await HttpClient.PutAsJsonAsync($"/MenuCategory/{createMenuCategory2.Id}", updateMenuCategory);


        //Assert
        responseForUpdate2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetMenuCategoryEndpointTest()
    {
        //Assert
        var createMenuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(createMenuCategory);
        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuCategory>>("/MenuCategory");

        //Assert
        response.Should().NotBeNull();
        
    }

    [Test]
    public async Task GetMenuItemByIdEndpointTest()
    {
        //Assert
        var createMenuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(createMenuCategory);
        //Act
        var response = await HttpClient.GetFromJsonAsync<MenuCategory>($"/MenuCategory/{createMenuCategory.Id}");

        //Assert
        response.ParentId.Should().Be(1);
        response.Name.Should().Be("Shula");
    }
}