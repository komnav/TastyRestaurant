using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Restaurant.Tests.Integration;

namespace Restaurant.Tests.Unit;

public abstract class BaseTestInit : BaseTest
{
    protected OrderService OrderService;
    protected PaymentCalculationService _paymentCalculationService;

    [SetUp]
    public new void SetUp()
    {
        var mockUserManager = new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<User>>().Object,
            Array.Empty<IUserValidator<User>>(),
            Array.Empty<IPasswordValidator<User>>(),
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<User>>>().Object);

        OrderService = new OrderService(
            Mock.Of<IOrderRepository>(),
            mockUserManager.Object);

        _paymentCalculationService = new PaymentCalculationService(
            Mock.Of<IOrderDetailRepository>());
    }
}