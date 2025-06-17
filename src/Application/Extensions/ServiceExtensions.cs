using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using ResraurantLayer.Services;

namespace RestaurantLayer.Extensions;

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
    }
}