using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Restaurant.Tests.Unit;

public class OrderServiceTest() : BaseTestInit
{
    [Test]
    public async Task Add_New_Order_And_Check_It_From_Db_For_Existing()
    {
        //Arrange
        var order = new Order()
        {
            UserId = 1,
            DateTime = DateTimeOffset.Now,
            Status = OrdersStatus.Accepted
        };

        //Act
        await CreateEntity(order);

        //Assert
        var getEntity = await GetEntity<Order>(x => x.Id == order.Id);
        getEntity.Should().NotBeNull();
        getEntity.UserId.Should().Be(order.UserId);
        getEntity.Status.Should().Be(order.Status);
    }
}