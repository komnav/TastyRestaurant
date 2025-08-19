using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Moq;
using Restaurant.Tests.Integration;

namespace Restaurant.Tests.Unit;

[Ignore("Has some issue")]
public class PaymentCalculationServiceTest : BaseTest
{
    private PaymentCalculationService _paymentCalculationService;

    [SetUp]
    public void Setup()
    {
        _paymentCalculationService = new PaymentCalculationService(
            Mock.Of<IOrderDetailRepository>());
    }

    [Ignore("Has some issue")]
    [Test]
    public async Task OrderDetails_TotalPriceTest()
    {
        //Arrange
        var addOrder = new Order
        {
            UserId = 1,
            DateTime = DateTimeOffset.Now,
            Status = OrdersStatus.Accepted,
        };
        await CreateEntity(addOrder);
        
        var menuCategory = new MenuCategory
        {
            Name = "name"
        };
        await CreateEntity(menuCategory);
        
        var menuItem = new MenuItem
        {
            CategoryId = menuCategory.Id,
            Price = 30,
            Name = "Name"
        };
        await CreateEntity(menuItem);
        
        var addOrderDetail = new OrderDetail
        {
            OrderId = addOrder.Id,
            MenuItemId = menuItem.Id,
            Price = 30,
            Quantity = 40,
            Status = OrderDetailStatus.Received
        };
        await CreateEntity(addOrderDetail);
        var addOrderDetail2 = new OrderDetail
        {
            OrderId = addOrder.Id,
            MenuItemId = menuItem.Id,
            Price = 30,
            Quantity = 50,
            Status = OrderDetailStatus.Received
        };
        await CreateEntity(addOrderDetail2);
        
        //Act
        var totalPrice = await _paymentCalculationService.PaymentCalculation(addOrder.Id);

        //Assert
        totalPrice.TotalPrice.Should().Be(165);
    }
}