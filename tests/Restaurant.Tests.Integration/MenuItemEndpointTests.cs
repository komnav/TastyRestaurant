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
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var createMenuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };

        //Act
        var response = await HttpClient.PostAsJsonAsync("MenuItem", createMenuItem);

        //Assert
        response.EnsureSuccessStatusCode();
        var menuItem = await GetEntity<MenuItem>(m =>
            m.CategoryId == createMenuItem.CategoryId
            && m.Price == createMenuItem.Price
            && m.Name == createMenuItem.Name);

        menuItem.Should().NotBeNull();
    }

    [Test]
    public async Task CreateDuplicateMenuItemEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };
        await CreateEntity(menuItem);

        var request = new CreateMenuItemRequestModel(menuCategory.Id, 50, "Burger5");

        //Act
        var response = await HttpClient.PostAsJsonAsync("/MenuItem", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task DeleteMenuItemEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.DeleteAsync($"/MenuItem/{menuItem.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task DeleteEmptyMenuItemEndpointTest()
    {
        //Act
        var response = await HttpClient.DeleteAsync("/MenuItem/123");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task UpdateMenuItemEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };
        await CreateEntity(menuItem);
        var updateMenuItem = new UpdateMenuItemRequestModel(1, 34, "pizza", MenuItemStatus.NotAvailable);

        //Act
        var responseForUpdate = await HttpClient.PutAsJsonAsync($"/MenuItem/{menuItem.Id}", updateMenuItem);

        //Assert
        responseForUpdate.EnsureSuccessStatusCode();
        var getMenuItem = await GetEntity<MenuItem>(t =>
            t.Id == menuItem.Id &&
            t.CategoryId == updateMenuItem.CategoryId &&
            t.Price == updateMenuItem.Price &&
            t.Name == updateMenuItem.Name &&
            t.Status == updateMenuItem.Status);

        getMenuItem.Should().NotBeNull();
    }

    [Test]
    public async Task UpdateDuplicateMenuItemEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };
        await CreateEntity(menuItem);

        var menuItem2 = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger6"
        };
        await CreateEntity(menuItem2);

        var updateMenuItem = new UpdateMenuItemRequestModel(1, 34, "Burger6", MenuItemStatus.NotAvailable);

        //Act
        var responseForUpdate2 = await HttpClient.PutAsJsonAsync($"/MenuItem/{menuItem.Id}", updateMenuItem);

        //Assert
        responseForUpdate2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetMenuItemEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuItem>>("/MenuItem");

        //Assert
        response.Should().NotBeNull();
    }

    [Test]
    public async Task GetMenuItemByIdEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5",
            Status = MenuItemStatus.NotAvailable
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.GetFromJsonAsync<MenuItem>($"/MenuItem/{menuItem.Id}");

        //Assert
        response.CategoryId.Should().Be(1);
        response.Price.Should().Be(132);
        response.Name.Should().Be("Burger5");
        response.Status.Should().Be(MenuItemStatus.NotAvailable);
    }

    [Test]
    public async Task GetMenuItemByCategoryIdEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "Shula",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger5"
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuItem>>($"/MenuItem/GetByCategory/{menuItem.Id}");

        //Assert
        response.Should().NotBeNull();
    }
}