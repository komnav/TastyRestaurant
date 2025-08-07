using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Tests.Integration;

namespace Restaurant.Tests.Unit;

public class OrderServiceTest() : BaseTest
{
    private IOrderRepository _orderRepository;

    [Test]
    public async Task Add_New_Order_And_Check_It_For_Existing()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _orderRepository = new OrderRepository(dbContext);
        
        //Arrange
        var order = new Order()
        {
            UserId = 1,
            DateTime = DateTimeOffset.Now,
            Status = OrdersStatus.Accepted
        };

        //Act
        await _orderRepository.CreateAsync(order);

        //Assert
        var checkOrder = await _orderRepository.GetAsync(order.Id);
        checkOrder.Should().Be(order);
    }
    
    [Test]
    public void OrderDetails_TotalPriceTest()
    {
        //Arrange
        var orderDetails = new List<OrderDetail>
        {
            new()
            {
                Price = 10,
                Quantity = 3
            },
            new()
            {
                Price = 30,
                Quantity = 4
            }
        };

        //Act
        var totalPrice = _orderService.GetTotalPrice(orderDetails);

        //Assert
        totalPrice.Should().Be(165);
    }
}