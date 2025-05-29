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
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var createMenuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
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
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
        };
        await CreateEntity(menuItem);

        var request = new CreateMenuItemRequestModel(menuCategory.Id, 50, "Burger");

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
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.DeleteAsync($"/MenuItem/{menuItem.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        var findMenuItem = await GetEntity<MenuItem>(x => x.Id == menuItem.Id);
        findMenuItem.Should().NotBeNull();
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
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
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
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var firstMenuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
        };
        await CreateEntity(firstMenuItem);

        var secondMenuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "KFC"
        };
        await CreateEntity(secondMenuItem);
        var updateMenuItem = new UpdateMenuItemRequestModel(1, 34, "KFC", MenuItemStatus.NotAvailable);

        //Act
        var responseForUpdate2 = await HttpClient.PutAsJsonAsync($"/MenuItem/{firstMenuItem.Id}", updateMenuItem);

        //Assert
        responseForUpdate2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetMenuItemEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
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
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger",
            Status = MenuItemStatus.NotAvailable
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.GetFromJsonAsync<MenuItem>($"/MenuItem/{menuItem.Id}");

        //Assert
        response.CategoryId.Should().Be(menuItem.CategoryId);
        response.Price.Should().Be(menuItem.Price);
        response.Name.Should().Be(menuItem.Name);
        response.Status.Should().Be(menuItem.Status);
    }

    [Test]
    public async Task GetMenuItemByCategoryIdEndpointTest()
    {
        //Arrange
        var menuCategory = new MenuCategory
        {
            Name = "1-um",
            ParentId = 1
        };
        await CreateEntity(menuCategory);

        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 132,
            Name = "Burger"
        };
        await CreateEntity(menuItem);

        //Act
        var response = await HttpClient.GetFromJsonAsync<List<MenuItem>>($"/MenuItem/GetByCategory/{menuItem.Id}");

        //Assert
        response.Should().NotBeNull();
    }
}