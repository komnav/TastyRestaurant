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
        var request = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");

        //Act
        var response = await HttpClient.PostAsJsonAsync("MenuCategory", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var menuCategory = await GetEntity<MenuCategory>(c =>
            c.Name == request.Name);

        menuCategory.Should().NotBeNull();
    }

    [Test]
    public async Task CreateDuplicateMenuCategoryEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");
        await CreateEntity(menuCategory);

        //Act
        var response = await HttpClient.PostAsJsonAsync("/MenuCategory", menuCategory);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task DeleteMenuCategoryEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");
        await CreateEntity(menuCategory);

        //Act
        var response = await HttpClient.DeleteAsync($"/MenuCategory/{menuCategory.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        var tryToFindMenuCategory = await GetEntity<MenuCategory>(x => x.Id == menuCategory.Id);
        tryToFindMenuCategory.Should().BeNull();
    }

    [Test]
    public async Task DeleteEmptyMenuCategoryEndpointTest()
    {
        //Arrange
        await LoginAsync("superadmin", "Admin1234$");

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
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");
        await CreateEntity(createMenuCategory);

        var updateMenuCategory = new UpdateMenuCategoryRequestModel("2-um", 1);

        //Act
        var response =
            await HttpClient.PutAsJsonAsync($"/MenuCategory/{createMenuCategory.Id}", updateMenuCategory);

        //Assert
        response.EnsureSuccessStatusCode();
        var menuCategory = await GetEntity<MenuCategory>(t =>
            t.Id == createMenuCategory.Id &&
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
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");
        await CreateEntity(createMenuCategory);

        var createSecondMenuCategory = new MenuCategory
        {
            Name = "2-um",
            ParentId = 1
        };
        await CreateEntity(createSecondMenuCategory);
        var updateMenuCategory = new UpdateMenuCategoryRequestModel(createMenuCategory.Name, 1);

        //Act
        var response =
            await HttpClient.PutAsJsonAsync($"/MenuCategory/{createSecondMenuCategory.Id}", updateMenuCategory);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetMenuCategoryEndpointTest()
    {
        //Assert
        var createMenuCategory = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");
        await CreateEntity(createMenuCategory);

        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuCategory>>("/MenuCategory");

        //Assert
        response.Should().NotBeNull();
        response.Count.Should().Be(1);
    }

    [Test]
    public async Task GetMenuCategoryByIdEndpointTest()
    {
        //Assert
        var createMenuCategory = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await LoginAsync("superadmin", "Admin1234$");
        await CreateEntity(createMenuCategory);
        //Act
        var response = await HttpClient.GetFromJsonAsync<MenuCategory>($"/MenuCategory/{createMenuCategory.Id}");

        //Assert
        response.Should().NotBeNull();
        response.ParentId.Should().Be(createMenuCategory.ParentId);
        response.Name.Should().Be(createMenuCategory.Name);
    }
}