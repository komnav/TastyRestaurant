using Microsoft.Extensions.DependencyInjection;
using Application.Services;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceLayer(this IServiceCollection service)
    {
        service.AddScoped<IMenuCategoryService, MenuCategoryService>();
        service.AddScoped<IMenuItemService, MenuItemService>();
        service.AddScoped<ITableService, TableService>();
        service.AddScoped<IReservationService, ReservationService>();
        service.AddScoped<IOrderDetailService, OrderDetailService>();
        service.AddScoped<IOrderService, OrderService>();
        service.AddScoped<IPaymentCalculationService, PaymentCalculationService>();
    }
}